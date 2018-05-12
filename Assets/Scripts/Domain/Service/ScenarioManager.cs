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
    ConditionManager conditionManager;
    int scenarioId = 0;

    public void Init()
    {
        sceneController = GameObject.FindObjectOfType<StorySceneController>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        imageManager = GameObject.FindObjectOfType<ImageManager>();
        imageManager.Init();
        itemManager = GameObject.FindObjectOfType<ItemManager>();
        paramManager = GameObject.FindObjectOfType<ParamManager>();
        conditionManager = GameObject.FindObjectOfType<ConditionManager>();
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
                foreach (Scenario selection in selectionList)
                {
                    string[] conditions = selection.Arg2.Split('&');
                    foreach (string condition in conditions)
                    {

                    }
                }

                // Selectionを表示
                sceneController.ShowSelections(selectionList);
                breakLoop = true;
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
            default:
                Debug.LogWarning("Unkown command [" + scenario.Command + "]");
                breakLoop = true;
                break;
        }
        return breakLoop;
    }

    void JumpTo(string dest)
    {
        scenarioId = ScenarioRepository.FindByCommand(dest).Id;
    }

    //void Update ()
    //{
    //	if (!scenario.isDownloadComplete) {
    //		return;
    //	}
    //	textHelper.Update ();
    //	if ((Input.GetMouseButtonDown (0) && isClickable) || (Input.GetKeyDown ("space") && isClickable)) {
    //		if (textHelper.IsCompleteDisplayText) {
    //			voice.Stop ();
    //			NextCommand ();
    //		} else {
    //			voice.Stop ();
    //			textHelper.CompleteDisplayText ();
    //		}
    //	}
    //}

    //void NextCommand ()
    //{
    //	while (true) {
    //		if (currentCommandId > scenario.commands.Count)
    //			break;
    //		if (string.IsNullOrEmpty (scenario.commands [currentCommandId])) {
    //			Debug.Log ("テキスト");
    //			//テキストを表示
    //			nameText.text = scenario.arg1s [currentCommandId];
    //			if (Random.Range (0, 2) == 0) {
    //				textHelper.SetNextLine (scenario.texts [currentCommandId]);
    //			} else {
    //				textHelper.SetNextLine (scenario.texts [currentCommandId], 0.3f);
    //			}

    //			//音声ファイルを取得
    //			if (!string.IsNullOrEmpty (scenario.arg2s [currentCommandId])) {
    //				AudioClip voiceAudioClip = Resources.Load<AudioClip> (scenario.arg2s [currentCommandId]);
    //				if (voiceAudioClip == null) {
    //					Debug.LogWarning ("Voice Error: [" + scenario.arg2s [currentCommandId] + "] not found");
    //					break;
    //				}
    //				//再生
    //				voice.clip = voiceAudioClip;
    //				if (string.IsNullOrEmpty (scenario.arg3s [currentCommandId])) {
    //					voice.volume = 1;
    //				} else {
    //					voice.volume = Mathf.Clamp (float.Parse (scenario.arg3s [currentCommandId]), 0, 1);
    //				}
    //				voice.Play ();
    //			}
    //			currentCommandId++;
    //			break;
    //		}
    //		Debug.Log (scenario.commands [currentCommandId]);
    //		if (scenario.commands [currentCommandId].Substring (0, 1) != "*") {
    //			bool breakLoop = Command (scenario.commands [currentCommandId]);
    //			if (breakLoop) {
    //				break;
    //			}
    //		}
    //		currentCommandId++;
    //	}
    //}

    //public void BackButton ()
    //{
    //	SceneManager.LoadScene ("Start");
    //}

    //public void SelectionButton (int i)
    //{
    //	isClickable = true;
    //	for (int j = 0; j < selectionButtons.Length; j++) {
    //		selectionButtons [j].SetActive (false);
    //	}
    //	currentCommandId += i;
    //	Command ("Jump");
    //	NextCommand ();
    //}

    //bool Command (string command)
    //{
    //	bool breakLoop = false;
    //	switch (command) {
    //	case "Jump":
    //		for (int i = 0; i < scenario.commands.Count; i++) {
    //			if (scenario.commands [i] == scenario.arg1s [currentCommandId]) {
    //				Debug.Log ("Found Jump Point:" + i + ":" + scenario.commands [i]);
    //				currentCommandId = i;
    //				break;
    //			}
    //		}
    //		break;
    //	case "Selection":
    //		breakLoop = true;
    //		isClickable = false;

    //		int selectionCount = 0;
    //		for (int i = 0; i < 3; i++) {
    //			if (scenario.commands [currentCommandId + i] == "Selection") {
    //				selectionCount++;
    //			}
    //		}
    //		for (int i = 0; i < selectionCount; i++) {
    //			selectionButtons [i].SetActive (true);
    //			selectionButtons [i].GetComponentInChildren<Text> ().text = scenario.texts [currentCommandId + i];
    //		}
    //		break;
    //	case "Bg":
    //		Texture bgTexture = Resources.Load (scenario.arg1s [currentCommandId]) as Texture;
    //		if (bgTexture == null) {
    //			Debug.LogWarning ("Background Error: [" + scenario.arg1s [currentCommandId] + "] not found");
    //			break;
    //		}
    //		backgroundImage.texture = bgTexture;
    //		break;
    //	case "BgOff":
    //		backgroundImage.texture = null;
    //		break;
    //	case "Character":
    //		//画像を取得
    //		Sprite characterSprite = Resources.Load<Sprite> (scenario.arg1s [currentCommandId]);
    //		if (characterSprite == null) {
    //			Debug.LogWarning ("Character Error: [" + scenario.arg1s [currentCommandId] + "] not found");
    //			break;
    //		}
    //		//ゲームオブジェクトを取得
    //		Image targetImage = GameObject.Find ("CharacterImage_" + scenario.arg2s [currentCommandId]).GetComponent<Image> ();
    //		if (targetImage == null) {
    //			Debug.LogWarning ("Character Error: [" + scenario.arg2s [currentCommandId] + "] gameobject not found");
    //			break;
    //		}
    //		//サイズを調整
    //		float width = characterSprite.rect.width;
    //		float height = characterSprite.rect.height;
    //		float aspectRatio = width / height;
    //		Vector2 targetImageSize = targetImage.GetComponent<RectTransform> ().sizeDelta;
    //		targetImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (targetImageSize.y * aspectRatio, targetImageSize.y);
    //		//画像を表示
    //		targetImage.sprite = characterSprite;
    //		targetImage.color = new Color (1, 1, 1, 1);
    //		//反転？
    //		if (string.IsNullOrEmpty (scenario.arg3s [currentCommandId])) {
    //			targetImage.transform.localEulerAngles = new Vector3 (0, 0, 0);
    //		} else {
    //			targetImage.transform.localEulerAngles = new Vector3 (0, 180, 0);
    //		}
    //		break;
    //	case "CharacterOff":
    //		if (string.IsNullOrEmpty (scenario.arg1s [currentCommandId])) {
    //			Image[] offTargetImages = GameObject.Find ("CharacterImages").GetComponentsInChildren<Image> ();
    //			for (int i = 0; i < offTargetImages.Length; i++) {
    //				offTargetImages [i].sprite = null;
    //				offTargetImages [i].color = new Color (1, 1, 1, 0);
    //			}
    //		} else {
    //			//ゲームオブジェクトを取得
    //			Image offTargetImage = GameObject.Find ("CharacterImage_" + scenario.arg1s [currentCommandId]).GetComponent<Image> ();
    //			if (offTargetImage == null) {
    //				Debug.LogWarning ("CharacterOff Error: [" + scenario.arg2s [currentCommandId] + "] gameobject not found");
    //				break;
    //			}
    //			//画像を消す
    //			offTargetImage.sprite = null;
    //			offTargetImage.color = new Color (1, 1, 1, 0);
    //		}
    //		break;
    //	case "Bgm":
    //		//音を取得
    //		AudioClip bgmAudioClip = Resources.Load<AudioClip> (scenario.arg1s [currentCommandId]);
    //		if (bgmAudioClip == null) {
    //			Debug.LogWarning ("Bgm Error: [" + scenario.arg1s [currentCommandId] + "] not found");
    //			break;
    //		}
    //		//再生
    //		bgm.clip = bgmAudioClip;
    //		if (string.IsNullOrEmpty (scenario.arg2s [currentCommandId])) {
    //			bgm.volume = 1;
    //		} else {
    //			bgm.volume = Mathf.Clamp (float.Parse (scenario.arg2s [currentCommandId]), 0, 1);
    //		}
    //		if (string.IsNullOrEmpty (scenario.arg3s [currentCommandId])) {
    //			bgm.time = 0;
    //		} else {
    //			bgm.time = float.Parse (scenario.arg3s [currentCommandId]);
    //		}
    //		bgm.Play ();
    //		break;
    //	case "BgmOff":
    //		bgm.clip = null;
    //		bgm.Stop ();
    //		break;
    //	case "Ambience":
    //		//音を取得
    //		AudioClip ambienceAudioClip = Resources.Load<AudioClip> (scenario.arg1s [currentCommandId]);
    //		if (ambienceAudioClip == null) {
    //			Debug.LogWarning ("Ambience Error: [" + scenario.arg1s [currentCommandId] + "] not found");
    //			break;
    //		}
    //		//再生
    //		ambience.clip = ambienceAudioClip;
    //		if (string.IsNullOrEmpty (scenario.arg2s [currentCommandId])) {
    //			ambience.volume = 1;
    //		} else {
    //			ambience.volume = Mathf.Clamp (float.Parse (scenario.arg2s [currentCommandId]), 0, 1);
    //		}
    //		ambience.Play ();
    //		break;
    //	case "AmbienceOff":
    //		ambience.clip = null;
    //		ambience.Stop ();
    //		break;
    //	case "SoundEffect":
    //		//音を取得
    //		AudioClip seAudioClip = Resources.Load<AudioClip> (scenario.arg1s [currentCommandId]);
    //		if (seAudioClip == null) {
    //			Debug.LogWarning ("SoundEffect Error: [" + scenario.arg1s [currentCommandId] + "] not found");
    //			break;
    //		}
    //		//再生
    //		soundEffect.clip = seAudioClip;
    //		if (string.IsNullOrEmpty (scenario.arg2s [currentCommandId])) {
    //			soundEffect.volume = 1;
    //		} else {
    //			soundEffect.volume = Mathf.Clamp (float.Parse (scenario.arg2s [currentCommandId]), 0, 1);
    //		}
    //		soundEffect.Play ();
    //		break;
    //	case "SoundEffectOff":
    //		soundEffect.clip = null;
    //		soundEffect.Stop ();
    //		break;
    //	case "StopSound":
    //		bgm.clip = null;
    //		bgm.Stop ();
    //		ambience.clip = null;
    //		ambience.Stop ();
    //		soundEffect.clip = null;
    //		soundEffect.Stop ();
    //		break;
    //	case "End":
    //		SceneManager.LoadScene ("Start");
    //		break;
    //	default:
    //		Debug.LogWarning ("コマンドエラー:unknown command - " + command);
    //		break;
    //	}
    //	return breakLoop;
    //}

    //void SetUpCharacterLayers ()
    //{
    //	GameObject imagePrefab = Resources.Load ("Prefabs/CharacterImage") as GameObject;
    //	GameObject targetObj = GameObject.Find ("CharacterImages");
    //	for (int i = 0; i < scenario.characterLayerList.Count; i++) {
    //		GameObject imageClone = Instantiate (imagePrefab, targetObj.transform) as GameObject;
    //		imageClone.name = "CharacterImage_" + scenario.characterLayerList [i] [0];
    //		imageClone.transform.localPosition = new Vector3 (int.Parse (scenario.characterLayerList [i] [2]), int.Parse (scenario.characterLayerList [i] [3]), 0);
    //	}
    //}
    public void OnSelectionSelected(int index)
    {
        scenarioId += index;
        Debug.Log(scenarioId);
        JumpTo(ScenarioRepository.FindById(scenarioId).Arg1);
        Debug.Log(scenarioId);
        Next();
    }
}
