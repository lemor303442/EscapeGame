using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListManager : MonoBehaviour
{
    public static ItemListManager Instance { get; private set; }

    void Awake()
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

    public List<Sprite> GetOwnedItemSprites()
    {
        List<Sprite> spriteList = new List<Sprite>();
        foreach (var itemEntity in ItemEntity.OwnedItems)
        {
            Sprite sprite = Resources.Load<Sprite>(itemEntity.Item.ImagePath);
            spriteList.Add(sprite);
        }
        return spriteList;
    }

    public Item GetSelectedItem(int index)
    {
        return ItemEntity.OwnedItems[index].Item;
    }
}
