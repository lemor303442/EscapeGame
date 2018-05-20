using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    StorySceneController sceneController;

    public void Init()
    {
        sceneController = GameObject.FindObjectOfType<StorySceneController>();
        LayerRepository.SortByOrder();
        sceneController.viewController.CreateLayers(LayerRepository.All);
    }

    public void UpdateLayerImage(string layerName, string imgPath)
    {
        Sprite image = Resources.Load<Sprite>(imgPath);
        if (image == null)
        {
            Debug.LogWarning("Sprite Load Error: [" + imgPath + "] not found");
            return;
        }
        sceneController.viewController.UpdateLayerImage(LayerRepository.FindByName(layerName), image);
    }

    public void RemoveLayerImage(string layerName)
    {
        sceneController.viewController.UpdateLayerImage(LayerRepository.FindByName(layerName), null);
    }

    public void UpdateCharacterImage(string layerName, string characterName, string characterPattern)
    {
        
    }
}
