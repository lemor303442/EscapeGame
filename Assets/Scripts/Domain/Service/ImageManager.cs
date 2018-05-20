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
        Layer layer = LayerRepository.FindByName(layerName);
        Character character = CharacterRepository.FindByPattern(characterName, characterPattern);
        if (character == null) Debug.LogWarning("Cannot find Character with name = [" + characterName + "], pattern = [" + characterPattern + "]");
        // Pivotを調整
        switch (character.Pivot)
        {
            case "Bottom":
                sceneController.viewController.UpdateLayerPivot(layer, TextAnchor.LowerCenter);
                break;
            case "Center":
                sceneController.viewController.UpdateLayerPivot(layer, TextAnchor.MiddleCenter);
                break;
            case "Top":
                sceneController.viewController.UpdateLayerPivot(layer, TextAnchor.UpperCenter);
                break;
            default:
                Debug.LogWarning("Unkown Character.Pivot [" + character.Pivot + "].");
                break;
        }
        //paddingを調整
        sceneController.viewController.UpdatePadding(layer, character);
        //Spriteを表示
        Sprite image = Resources.Load<Sprite>(character.FilePath);
        sceneController.viewController.UpdateLayerImage(layer, image);
        // Imageのサイズを調整
        Debug.Log(image);
        float imageRatio = image.rect.height / image.rect.width;
        Vector2 size = new Vector2(layer.Width, layer.Width * imageRatio);
        if (size.y > layer.Height) size = new Vector2(size.x, layer.Height);
        sceneController.viewController.UpdateLayerImageSize(layer, size);
    }
}
