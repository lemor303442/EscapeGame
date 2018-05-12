using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDataManager : MonoBehaviour
{
    public static MasterDataManager Instance;
    public MasterDataSet MasterDataSet { get; private set; }

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
        MasterDataLoader dataLoader = new MasterDataLoader();
        MasterDataSet = dataLoader.ReadData();
    }

    public void DownloadData()
    {
        MasterDataLoader dataLoader = new MasterDataLoader();
        dataLoader.DownloadData();
    }
}
