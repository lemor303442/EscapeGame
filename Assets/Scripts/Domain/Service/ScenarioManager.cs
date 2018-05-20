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

    bool isClickable = false;
    bool isEscapeMode = false;
    int scenarioId = 0;

    TextCommandHandler textCommandHandler;
    JumpCommandHandler jumpCommandHandler;
    BgCommandHandler bgCommandHandler;
    BgOffCommandHandler bgOffCommandHandler;
    SelectionCommandHandler selectionCommandHandler;
    SpriteCommandHandler spriteCommandHandler;
    SpriteOffCommandHandler spriteOffCommandHandler;
    CharacterCommandHandler characterCommandHandler;
    CharacterOffCommandHandler characterOffCommandHandler;
    BgmCommandHandler bgmCommandHandler;

    public StorySceneViewController ScenarioView
    {
        get { return sceneController.viewController; }
    }

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

        isClickable = true;
        textCommandHandler = new TextCommandHandler(this);
        bgCommandHandler = new BgCommandHandler(this);
        bgOffCommandHandler = new BgOffCommandHandler(this);
        jumpCommandHandler = new JumpCommandHandler(this);
        selectionCommandHandler = new SelectionCommandHandler(this);
        spriteCommandHandler = new SpriteCommandHandler(this);
        spriteOffCommandHandler = new SpriteOffCommandHandler(this);
        characterCommandHandler = new CharacterCommandHandler(this);
        characterOffCommandHandler = new CharacterOffCommandHandler(this);
        bgmCommandHandler = new BgmCommandHandler(this);
    }

    public void OnClick(Vector2 touchPos)
    {
        if (isEscapeMode)
        {
            escapeManager.OnClick(touchPos);
            return;
        }
        if (isClickable)
        {
            if (ScenarioView.textComponentHelper.IsCompleteDisplayText)
            {
                Next();
            }
            else
            {
                ScenarioView.textComponentHelper.CompleteDisplayText();
            }
        }
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
        ShowCommandLog(scenario);
        switch (scenario.Command)
        {
            case "":
                {
                    var options = TextCommandHandler.Options.Create(scenario);
                    textCommandHandler.OnCommand(options);
                    breakLoop = true;
                    break;
                }
            case "Jump":
                {
                    var options = JumpCommandHandler.Options.Create(scenario);
                    jumpCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "Selection":
                {
                    var options = SelectionCommandHandler.Options.Create(scenario);
                    selectionCommandHandler.OnCommand(options);
                    isClickable = false;
                    breakLoop = true;
                    break;
                }
            case "Bg":
                {
                    var options = BgCommandHandler.Options.Create(scenario);
                    bgCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "BgOff":
                {
                    var options = BgOffCommandHandler.Options.Create(scenario);
                    bgOffCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "Sprite":
                {
                    var options = SpriteCommandHandler.Options.Create(scenario);
                    spriteCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "SpriteOff":
                {
                    var options = SpriteOffCommandHandler.Options.Create(scenario);
                    spriteOffCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "Character":
                {
                    var options = CharacterCommandHandler.Options.Create(scenario);
                    characterCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "CharacterOff":
                {
                    var options = CharacterOffCommandHandler.Options.Create(scenario);
                    characterOffCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "Bgm":
                {
                    var options = BgmCommandHandler.Options.Create(scenario);
                    bgmCommandHandler.OnCommand(options);
                    scenarioId++;
                    break;
                }
            case "BgmOff":
                {
                    audioManager.StopBgm();
                    scenarioId++;
                    break;
                }
            case "Ambience":
                Debug.Log("Command: [Ambience]");
                if (string.IsNullOrEmpty(scenario.Arg2)) audioManager.PlayAmbience(scenario.Arg1);
                else audioManager.PlayAmbience(scenario.Arg1, float.Parse(scenario.Arg2));
                scenarioId++;
                break;
            case "AmbienceOff":
                {
                    audioManager.StopAmbience();
                    scenarioId++;
                    break;
                }
            case "SoundEffect":
                Debug.Log("Command: [SoundEffect]");
                if (string.IsNullOrEmpty(scenario.Arg2)) audioManager.PlaySoundEffect(scenario.Arg1);
                else audioManager.PlaySoundEffect(scenario.Arg1, float.Parse(scenario.Arg2));
                scenarioId++;
                break;
            case "SoundEffectOff":
                {
                    audioManager.StopSoundEffect();
                    scenarioId++;
                    break;
                }
            case "StopAllSound":
                {
                    audioManager.StopAllSound();
                    scenarioId++;
                    break;
                }
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
                ChangeToEscapeMode();
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

    public void ShowCommandLog(Scenario scenario)
    {
        Debug.Log("(Senario.id:" + scenario.Id + ") => Command: [" + scenario.Command + "]");
    }

    public void OnSelectionSelected(int index)
    {
        scenarioId += index;

        // Selectionを非表示にする
        for (int i = 0; i < ScenarioView.NumOfSelectionButtons; i++)
        {
            ScenarioView.ToggleSelectionButtonIsActive(i, false);
        }
        // Jumpさせる
        JumpTo(ScenarioRepository.FindById(scenarioId).Arg1);

        isClickable = true;
        Next();
    }

    public void ChangeToEscapeMode()
    {
        isEscapeMode = true;
        ScenarioView.ToggleNamePanelIsActive(false);
        ScenarioView.ToggleContentPanelIsActive(false);
    }

    public void ChangeToScenarioMode(string dest)
    {
        isEscapeMode = false;
        ScenarioView.ToggleNamePanelIsActive(true);
        ScenarioView.ToggleContentPanelIsActive(true);
        ScenarioView.UpdateEscapeBackground(null);
        ScenarioView.ToggleEscapeButtonIsActive(EscapeButtonType.RIGHT, false);
        ScenarioView.ToggleEscapeButtonIsActive(EscapeButtonType.LEFT, false);
        ScenarioView.ToggleEscapeButtonIsActive(EscapeButtonType.DOWN, false);
        JumpTo(dest);
        Next();
    }

    public void OnEscapeButtonDown(EscapeButtonType type)
    {
        escapeManager.OnEscapeButtonDown(type);
    }
}
