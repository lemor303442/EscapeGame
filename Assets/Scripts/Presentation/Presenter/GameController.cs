using UnityEngine;

/// <summary>
/// Game manager. A singleton.
/// Manages things which relate to all game. 
/// </summary>
public class GameController : MonoBehaviour
{

    public static GameController Instance;

    void Awake()
    {
        Singleton();
    }

    void Singleton()
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

    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (TextFileHelper.IsExist(Const.Path.MasterData.escapeInput))
        {
            MasterDataManager.Instance.UpdateData();
            Debug.Log("Loaded data from local storage.");
            if (TextFileHelper.IsExist(Const.Path.GameData.userItem))
            {
                GameDataManager.Instance.UpdateData();
                Debug.Log("Loaded gameData from local storage.");
            }
            else
            {
                GameDataManager.Instance.CreateInitialData();
                Debug.Log("Created initial gameData.");
            }
        }
        else
        {
            Debug.Log("Downloading data from spreadsheet. Go to main scene after a moment.");
            MasterDataManager.Instance.DownloadData();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
    }
}
