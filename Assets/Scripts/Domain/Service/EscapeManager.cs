using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    StorySceneController sceneController;
    List<EscapeInput> availableInputList;


    public void Init()
    {
        sceneController = GameObject.FindObjectOfType<StorySceneController>();
    }

    public void ToEscape(string escapeSceneName)
    {
        EscapeScene escapeScene = EscapeSceneRepository.FindByName(escapeSceneName);
        Sprite sprite = Resources.Load<Sprite>(escapeScene.ImagePath);
        if (sprite == null) Debug.LogWarning("EscapeScene Error: [" + escapeScene.ImagePath + "] not found");
        sceneController.viewController.UpdateEscapeBackground(sprite);
        availableInputList = GetAvailableInputList(escapeScene);
    }

    public List<EscapeInput> GetAvailableInputList(EscapeScene scene)
    {
        List<EscapeInput> list = EscapeInputRepository.FindBySceneName(scene.Name);
        List<int> removeIds = new List<int>();
        foreach (EscapeInput escapeInput in list)
        {
            List<string> conditionList = ConditionHelper.GetConditions(escapeInput.Conditions);
            if (conditionList == null) continue;
            foreach (string condition in conditionList)
            {
                if (!ConditionHelper.IsConditionValid(condition))
                {
                    removeIds.Add(escapeInput.Id);
                    break;
                }
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
            if(IsInsideOfTarget(escapeInput, clickPos)){
                if(Regex.IsMatch(escapeInput.JumpTo, @"^\*")){
                    sceneController.ChangeToScenarioMode(escapeInput.JumpTo);
                }else{
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
