using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneController : MonoBehaviour
{
    [HideInInspector]
    public StorySceneViewController viewController;
    ScenarioManager scenarioManager;
    EscapeManager escapeManager;

    bool isDataReady = false;

    void Start()
    {
        //Debug.Log("".IndexOf());
        // 初期データのインスタンスがない場合は、データを再ロードする。
        isDataReady = TextFileHelper.IsExist(Const.Path.MasterData.escapeInput) && ScenarioRepository.Count != 0;
        if (!isDataReady && TextFileHelper.IsExist(Const.Path.MasterData.escapeInput))
        {
            GameController.Instance.Init();
            isDataReady = true;
        }
        viewController = GameObject.FindObjectOfType<StorySceneViewController>();
        scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        escapeManager = GameObject.FindObjectOfType<EscapeManager>();
        viewController.Init();
        scenarioManager.Init();

        if (isDataReady) scenarioManager.Next();
    }

    void Update()
    {
        if (isDataReady && viewController.enabled == false) viewController.enabled = true;
    }
}