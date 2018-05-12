public class ItemEntity
{
    public Item Item { get; private set; }
    public UserItem UserItem { get; private set; }

    public ItemEntity(Item _item, UserItem _userItem)
    {
        Item = _item;
        UserItem = _userItem;
    }

    public static ItemEntity FindByUserItemId(int id)
    {
        UserItem userItem = UserItemRepository.FindById(id);
        Item item = ItemRepository.FindById(userItem.Id);
        return new ItemEntity(item, userItem);
    }

    public static ItemEntity FindByItemName(string name)
    {
        Item item = ItemRepository.FindByName(name);
        UserItem userItem = UserItemRepository.FindByItemId(item.Id);
        return new ItemEntity(item, userItem);
    }
}
