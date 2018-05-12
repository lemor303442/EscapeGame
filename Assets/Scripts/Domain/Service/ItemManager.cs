using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public void ToggleItemIsOwned(string name, bool flg)
    {
        ItemEntity itemEntity = ItemEntity.FindByItemName(name);itemEntity.UserItem.ToggleIsOwned(flg);
        GameDataManager.Instance.SaveData();
    }
}
