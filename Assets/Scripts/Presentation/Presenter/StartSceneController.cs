using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Start manager.
/// Manages things which relate to only start scene. 
/// </summary>
public class StartSceneController : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Story");
    }
}
