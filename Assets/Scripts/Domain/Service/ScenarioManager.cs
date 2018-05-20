using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


/// <summary>
/// シナリオの進行，イベントに応じて処理を決定する．
/// </summary>
public class ScenarioManager : MonoBehaviour
{
    StorySceneController sceneController;
    AudioManager audioManager;
    ImageManager imageManager;
    ItemManager itemManager;
    ParamManager paramManager;
    EscapeManager escapeManager;
    AnimatorManager animatorManager;
    int scenarioId = 0;

    public void Init()
    {
        sceneController = GameObject.FindObjectOfType<StorySceneController>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        imageManager = GameObject.FindObjectOfType<ImageManager>();
        imageManager.Init();
        itemManager = GameObject.FindObjectOfType<ItemManager>();
        paramManager = GameObject.FindObjectOfType<ParamManager>();
        escapeManager = GameObject.FindObjectOfType<EscapeManager>();
        escapeManager.Init();
        animatorManager = GameObject.FindObjectOfType<AnimatorManager>();
    }

    public void Next()
    {
        int safetyCount = 0;
        scenarioId++;
        while (true)
        {
            if (scenarioId > ScenarioRepository.Count) break;
            Scenario scenario = ScenarioRepository.FindById(scenarioId);

            if (Regex.IsMatch(scenario.Command, @"^\*")
                || Regex.IsMatch(scenario.Command, @"^\/\/"))
            {
                scenarioId++;
                continue;
            }

            if (CommandFunc(scenario)) break;
            safetyCount++;
            if (safetyCount > 1000)
            {
                Debug.LogWarning("INFINIT LOOP");
                break;
            }
        }
    }

    private bool CommandFunc(Scenario scenario)
    {
        bool breakLoop = false;
        switch (scenario.Command)
        {
            case "":
                Debug.Log("Command: [Text]");
                // テキストの表示
                if (string.IsNullOrEmpty(scenario.Arg4))
                    sceneController.ShowNextText(scenario.Arg1, scenario.Text);
                else
                    sceneController.ShowNextText(scenario.Arg1, scenario.Text, float.Parse(scenario.Arg4));
                // ボイスの再生
                if (!string.IsNullOrEmpty(scenario.Arg2))
                {
                    if (string.IsNullOrEmpty(scenario.Arg3))
                        audioManager.PlayVoice(scenario.Arg2);
                    else
                        audioManager.PlayVoice(scenario.Arg2, float.Parse(scenario.Arg3));
                }
                breakLoop = true;
                break;
            case "Jump":
                Debug.Log("Command: [Jump]");
                // ジャンプ先へ移動
                JumpTo(scenario.Arg1);
                break;
            case "Selection":
                Debug.Log("Command: [Selection]");
                List<Scenario> selectionList = ScenarioRepository.GetSelections(scenarioId);
                // 条件を満たしていないselectionを削除
                List<int> removeSelectionId = new List<int>();
                foreach (Scenario selection in selectionList)
                {
                    if (string.IsNullOrEmpty(selection.Arg2)) continue;
                    List<string> conditionList = ConditionHelper.GetConditions(selection.Arg2);
                    if (conditionList == null) continue;
                    foreach (string condition in conditionList)
                    {
                        if (!ConditionHelper.IsConditionValid(condition)) {
                            removeSelectionId.Add(selection.Id);
                            break;
                        }
                    }
                }
                foreach (int i in removeSelectionId)
                {
                    selectionList.RemoveAll(x => x.Id == i);
                }

                // Selectionを表示
                sceneController.ShowSelections(selectionList);
                breakLoop = true;
                Debug.Log("break selection loop");
                break;
            case "Bg":
                Debug.Log("Command: [Bg]");
                imageManager.UpdateBackgroundImage(scenario.Arg1, scenario.Arg2);
                scenarioId++;
                break;
            case "BgOff":
                Debug.Log("Command: [BgOff]");
                imageManager.RemoveLayerImage(scenario.Arg1);
                scenarioId++;
                break;
            case "Character":
                Debug.Log("Command: [Character]");
                imageManager.UpdateCharacterImage(scenario.Arg1, scenario.Arg2, scenario.Arg3);
                scenarioId++;
                break;
            case "CharacterOff":
                Debug.Log("Command: [CharacterOff]");
                imageManager.RemoveLayerImage(scenario.Arg1);
                scenarioId++;
                break;
            case "Bgm":
                Debug.Log("Command: [Bgm]");
                if (string.IsNullOrEmpty(scenario.Arg3))
                {
                    if (string.IsNullOrEmpty(scenario.Arg2)) audioManager.PlayBgm(scenario.Arg1);
                    else audioManager.PlayBgm(scenario.Arg1, float.Parse(scenario.Arg2));
                }
                else
                {
                    if (string.IsNullOrEmpty(scenario.Arg2)) audioManager.PlayBgmWithStartTime(scenario.Arg1, float.Parse(scenario.Arg3));
                    else audioManager.PlayBgmWithStartTime(scenario.Arg1, float.Parse(scenario.Arg3), float.Parse(scenario.Arg2));
                }
                scenarioId++;
                break;
            case "BgmOff":
                Debug.Log("Command: [BgmOff]");
                audioManager.StopBgm();
                scenarioId++;
                break;
            case "Ambience":
                Debug.Log("Command: [Ambience]");
                if (string.IsNullOrEmpty(scenario.Arg2)) audioManager.PlayAmbience(scenario.Arg1);
                else audioManager.PlayAmbience(scenario.Arg1, float.Parse(scenario.Arg2));
                scenarioId++;
                break;
            case "AmbienceOff":
                Debug.Log("Command: [AmbienceOff]");
                audioManager.StopAmbience();
                scenarioId++;
                break;
            case "SoundEffect":
                Debug.Log("Command: [SoundEffect]");
                if (string.IsNullOrEmpty(scenario.Arg2)) audioManager.PlaySoundEffect(scenario.Arg1);
                else audioManager.PlaySoundEffect(scenario.Arg1, float.Parse(scenario.Arg2));
                scenarioId++;
                break;
            case "SoundEffectOff":
                Debug.Log("Command: [SoundEffectOff]");
                audioManager.StopSoundEffect();
                scenarioId++;
                break;
            case "StopAllSound":
                Debug.Log("Command: [StopAllSound]");
                audioManager.StopAllSound();
                scenarioId++;
                break;
            case "ChangeScene":
                Debug.Log("Command: [ChangeScene]");
                SceneManager.LoadScene(scenario.Arg1);
                scenarioId++;
                break;
            case "Item":
                Debug.Log("Command: [Item]");
                itemManager.ToggleItemIsOwned(scenario.Arg1, System.Convert.ToBoolean(scenario.Arg2));
                scenarioId++;
                break;
            case "Param":
                Debug.Log("Command: [Param]");
                paramManager.UpdateParam(scenario.Arg1);
                scenarioId++;
                break;
            case "ToEscape":
                Debug.Log("Command: [ToEspace]");
                sceneController.ChangeToEscapeMode();
                escapeManager.ToEscape(scenario.Arg1);
                breakLoop = true;
                break;
            case "InstantiatePrefab":
                Debug.Log("Command: [InstantiatePrefab]");
                GameObject clone = Instantiate<GameObject>(
                    Resources.Load<GameObject>(scenario.Arg1),
                    new Vector3(float.Parse(scenario.Arg3), float.Parse(scenario.Arg4), float.Parse(scenario.Arg5)),
                    Quaternion.identity
                );
                clone.name = scenario.Arg2;
                scenarioId++;
                break;
            case "AnimatorSetTrigger":
                Debug.Log("Command: [AnimatorSetTrigger]");
                animatorManager.SetTrigger(scenario.Arg1, scenario.Arg2);
                scenarioId++;
                break;
            case "DestoryGameObject":
                Debug.Log("Command: [DestoryGameObject]");
                Destroy(GameObject.Find(scenario.Arg1));
                scenarioId++;
                break;
            default:
                Debug.LogWarning("Unkown command [" + scenario.Command + "]");
                breakLoop = true;
                break;
        }
        return breakLoop;
    }

    public void JumpTo(string dest)
    {
        scenarioId = ScenarioRepository.FindByCommand(dest).Id;
    }

    public void OnSelectionSelected(int index)
    {
        scenarioId += index;
        JumpTo(ScenarioRepository.FindById(scenarioId).Arg1);
        Next();
    }
}
