using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StorySceneViewController : MonoBehaviour
{
    TextComponentHelper textComponentHelper;

    [SerializeField] GameObject emptyObject;
    [SerializeField] Text nameText;
    [SerializeField] public Text contentText;
    [SerializeField] GameObject[] selectionButtons = new GameObject[3];
    [SerializeField] Text[] selectionTexts = new Text[3];
    [SerializeField] Transform layerTarget;
    [SerializeField] GameObject namePanel;
    [SerializeField] GameObject contentPanel;
    [SerializeField] Image escapeBackground;
    [SerializeField] GameObject escapeButtonRight;
    [SerializeField] GameObject escapeButtonDown;
    [SerializeField] GameObject escapeButtonLeft;

    // item
    [SerializeField] GameObject itemList;
    [SerializeField] GameObject itemListContent;
    [SerializeField] GameObject itemListDetail;
    [SerializeField] Image[] itemListImages;
    [SerializeField] Image itemDetailImage;
    [SerializeField] Text itemDetailText;

    public int NumOfSelectionButtons { get { return selectionButtons.Length; } }
    public bool IsCompleteDisplayText
    {
        get { return textComponentHelper.IsCompleteDisplayText; }
    }

    public void Init()
    {
        enabled = false;
        textComponentHelper = new TextComponentHelper(contentText);
        OnItemListCloseButtonDown();
    }

    void Update()
    {
        textComponentHelper.Update();
    }

    public void ToggleNamePanelIsActive(bool flg)
    {
        namePanel.SetActive(flg);
        nameText.text = "";
    }

    public void ToggleContentPanelIsActive(bool flg)
    {
        contentPanel.SetActive(flg);
        contentText.text = "";
    }

    public void UpdateNameText(string name)
    {
        nameText.text = name;
    }

    public void UpdateContentText(string content, float speed)
    {
        textComponentHelper.SetNextLine(TextHelper.ReplaceTextTags(content), speed);
    }

    public void ToggleSelectionButtonIsActive(int index, bool flg)
    {
        selectionButtons[index].SetActive(flg);
    }

    public void UpdateSelectionText(int index, string text)
    {
        selectionTexts[index].text = text;
    }

    public void OnSelectionButtonDown(int index)
    {
        GameObject.FindObjectOfType<ScenarioManager>().OnSelectionSelected(index);
    }

    public void UpdateLayerImage(Layer layer, Sprite sprite)
    {
        Image image = FindLayerImage(layer);
        if (sprite == null)
        {
            image.sprite = null;
            image.enabled = false;
        }
        else
        {
            image.sprite = sprite;
            image.enabled = true;
        }
    }

    public void UpdateEscapeBackground(Sprite sprite)
    {
        if (sprite == null)
        {
            escapeBackground.sprite = null;
            escapeBackground.enabled = false;
        }
        else
        {
            escapeBackground.sprite = sprite;
            escapeBackground.enabled = true;
        }
    }

    public void UpdateLayerPivot(Layer layer, TextAnchor textAnchor)
    {
        GameObject layerObj = FindLayer(layer);
        layerObj.GetComponent<VerticalLayoutGroup>().childAlignment = textAnchor;
    }

    public void UpdatePadding(Layer layer, Character character)
    {
        GameObject layerObj = FindLayer(layer);
        VerticalLayoutGroup group = layerObj.GetComponent<VerticalLayoutGroup>();
        group.padding.left = character.PosX;
        group.padding.right = -character.PosX;
        group.padding.bottom = character.PosY;
        group.padding.top = -character.PosY;
    }

    public void UpdateLayerImageSize(Layer layer, Vector2 size)
    {
        Image image = FindLayerImage(layer);
        image.GetComponent<RectTransform>().sizeDelta = size;
    }

    public void CreateLayers(List<Layer> layerList)
    {
        foreach (Layer layer in layerList)
        {
            //Layerオブジェクトを生成し、VerticalAlignGroupコンポーネントを付与
            GameObject newLayer = Instantiate(emptyObject, layerTarget);
            newLayer.name = layer.Name + "_" + layer.LayerType;
            RectTransform rectTransform = newLayer.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(layer.Width, layer.Height);
            rectTransform.localPosition = new Vector3(
                layer.Width / 2 - Const.ScreenSize.width / 2 + layer.PosX,
                -layer.Height / 2 + Const.ScreenSize.height / 2 - layer.PosY,
                0
            );
            if (layer.FlipX == true) rectTransform.localEulerAngles = new Vector3(rectTransform.eulerAngles.x,
                                                                                  180,
                                                                                  rectTransform.eulerAngles.z);
            if (layer.FlipY == true) rectTransform.localEulerAngles = new Vector3(180,
                                                                                  rectTransform.eulerAngles.y,
                                                                                  rectTransform.eulerAngles.z);

            VerticalLayoutGroup alignGroup = newLayer.AddComponent<VerticalLayoutGroup>();
            alignGroup.childAlignment = TextAnchor.MiddleCenter;
            alignGroup.childControlHeight = false;
            //Layerの子オブジェクトを生成し、Imageコンポーネントを付与
            GameObject newLayerChild = Instantiate(emptyObject, newLayer.transform);
            newLayerChild.name = layer.Name + "_" + layer.LayerType + "_child";
            newLayerChild.AddComponent<RectTransform>().sizeDelta = new Vector2(layer.Width, layer.Height);
            Image newImage = newLayerChild.AddComponent<Image>();
            newImage.enabled = false;
            newImage.preserveAspect = true;
        }
    }

    public Image FindLayerImage(Layer layer)
    {
        return GameObject.Find(layer.Name + "_" + layer.LayerType + "_child").GetComponent<Image>();
    }

    public GameObject FindLayer(Layer layer)
    {
        return GameObject.Find(layer.Name + "_" + layer.LayerType);
    }

    public void ToggleEscapeButtonIsActive(EscapeButtonType type, bool flg)
    {
        switch (type)
        {
            case EscapeButtonType.RIGHT:
                escapeButtonRight.SetActive(flg);
                break;
            case EscapeButtonType.DOWN:
                escapeButtonDown.SetActive(flg);
                break;
            case EscapeButtonType.LEFT:
                escapeButtonLeft.SetActive(flg);
                break;
        }
    }

    public void OnEscapeButtonDown(string buttonType)
    {
        switch (buttonType)
        {
            case "RIGHT":
                GameObject.FindObjectOfType<ScenarioManager>().OnEscapeButtonDown(EscapeButtonType.RIGHT);
                break;
            case "LEFT":
                GameObject.FindObjectOfType<ScenarioManager>().OnEscapeButtonDown(EscapeButtonType.LEFT);
                break;
            case "DOWN":
                GameObject.FindObjectOfType<ScenarioManager>().OnEscapeButtonDown(EscapeButtonType.DOWN);
                break;
        }
    }

    public void ShowSelections(List<Scenario> selectionList)
    {
        for (int i = 0; i < selectionList.Count; i++)
        {
            ToggleSelectionButtonIsActive(i, true);
            UpdateSelectionText(i, selectionList[i].Text);
        }
    }

    public void CompleteDisplayText()
    {
        textComponentHelper.CompleteDisplayText();
    }

    public void OnClickButtonBottonDown()
    {
        GameObject.FindObjectOfType<ScenarioManager>().OnClick(TouchInput.position);
    }

    public void OnItemListButtonDown()
    {
        // Open Item List
        itemList.SetActive(true);
        itemListContent.SetActive(true);
        itemListDetail.SetActive(false);
        UpdateItemListContent(ItemListManager.Instance.GetOwnedItemSprites());
    }

    public void OnItemListCloseButtonDown()
    {
        itemList.SetActive(false);
    }

    public void OnItemListContentButtonDown(int i)
    {
        // Show Item Detail
        itemListContent.SetActive(false);
        itemListDetail.SetActive(true);
        UpdateItemDetail(
            Resources.Load<Sprite>(ItemListManager.Instance.GetSelectedItem(i).ImagePath),
            ItemListManager.Instance.GetSelectedItem(i).Description
        );
    }

    public void OnItemDetailCloseButtonDown()
    {
        itemListContent.SetActive(true);
        itemListDetail.SetActive(false);
    }

    void UpdateItemListContent(List<Sprite> spriteList)
    {
        for (int i = 0; i < itemListImages.Length; i++)
        {
            if (i < spriteList.Count)
            {
                itemListImages[i].sprite = spriteList[i];
            }
            else
            {
                itemListImages[i].sprite = Resources.Load<Sprite>("UI/no_item");
            }
        }
    }

    void UpdateItemDetail(Sprite sprite, string text)
    {
        itemDetailImage.sprite = sprite;
        itemDetailText.text = text;
    }
}

public enum EscapeButtonType
{
    RIGHT,
    DOWN,
    LEFT
}
