using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameDataSet GameDataSet { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateData()
    {
        GameDataLoader dataLoader = new GameDataLoader();
        GameDataSet = dataLoader.ReadData();
    }

    public void CreateInitialData()
    {
        GameDataLoader dataLoader = new GameDataLoader();
        GameDataSet = dataLoader.CreateInitialData();
    }

    public void SaveData()
    {
        GameDataLoader.SaveData(GameDataSet);
    }
}
