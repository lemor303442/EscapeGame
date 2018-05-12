using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    StorySceneController sceneController;

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
    }

}
