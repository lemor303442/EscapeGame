[System.Serializable]
public class UserItem
{
    public int Id { get; private set; }
    public int ItemId { get; private set; }
    public bool IsOwned { get; private set; }

    public UserItem(int _id, int _itemId, bool _isOwned)
    {
        Id = _id;
        ItemId = _itemId;
        IsOwned = _isOwned;
    }

    public void ToggleIsOwned(bool flg)
    {
        IsOwned = flg;
    }
}
