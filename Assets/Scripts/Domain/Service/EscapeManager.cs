using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    EscapeScene currentEscapeScene;
    StorySceneController sceneController;
    List<EscapeInput> availableInputList;


    public void Init()
    {
        sceneController = GameObject.FindObjectOfType<StorySceneController>();
    }

    public void ToEscape(string escapeSceneName)
    {
        currentEscapeScene = EscapeSceneRepository.FindByName(escapeSceneName);
        UpdateArrowButtons(currentEscapeScene);
        Sprite sprite = Resources.Load<Sprite>(currentEscapeScene.ImagePath);
        if (sprite == null) Debug.LogWarning("EscapeScene Error: [" + currentEscapeScene.ImagePath + "] not found");
        sceneController.viewController.UpdateEscapeBackground(sprite);
        availableInputList = GetAvailableInputList(currentEscapeScene);
    }

    public void UpdateArrowButtons(EscapeScene scene)
    {
        sceneController.viewController.ToggleEscapeButtonIsActive(EscapeButtonType.RIGHT, !string.IsNullOrEmpty(scene.Right));
        sceneController.viewController.ToggleEscapeButtonIsActive(EscapeButtonType.LEFT, !string.IsNullOrEmpty(scene.Left));
        sceneController.viewController.ToggleEscapeButtonIsActive(EscapeButtonType.DOWN, !string.IsNullOrEmpty(scene.Down));
    }

    public void OnEscapeButtonDown(EscapeButtonType type)
    {
        switch(type){
            case EscapeButtonType.RIGHT:
                ToEscape(currentEscapeScene.Right);
                break;
            case EscapeButtonType.LEFT:
                ToEscape(currentEscapeScene.Left);
                break;
            case EscapeButtonType.DOWN:
                ToEscape(currentEscapeScene.Down);
                break;
        }
    }

    public List<EscapeInput> GetAvailableInputList(EscapeScene scene)
    {
        List<EscapeInput> list = EscapeInputRepository.FindBySceneName(scene.Name);
        List<int> removeIds = new List<int>();
        foreach (EscapeInput escapeInput in list)
        {
            if (!ConditionHelper.IsAllConditionValid(escapeInput.Conditions))
            {
                removeIds.Add(escapeInput.Id);
            }
        }
        foreach (int i in removeIds)
        {
            list.RemoveAll(x => x.Id == i);
        }
        return list;
    }

    public void OnClick(Vector2 clickPos)
    {
        foreach (EscapeInput escapeInput in availableInputList)
        {
            if (IsInsideOfTarget(escapeInput, clickPos))
            {
                if (Regex.IsMatch(escapeInput.JumpTo, @"^\*"))
                {
                    //sceneController.ChangeToScenarioMode(escapeInput.JumpTo);
                }
                else
                {
                    ToEscape(escapeInput.JumpTo);
                }
                break;
            }
        }
    }

    bool IsInsideOfTarget(EscapeInput escapeInput, Vector2 pos)
    {
        bool isInX = pos.x >= escapeInput.PosX && pos.x <= escapeInput.PosX + escapeInput.Width;
        bool isInY = pos.y >= escapeInput.PosY && pos.y <= escapeInput.PosY + escapeInput.Height;
        return isInX && isInY;
    }
}
