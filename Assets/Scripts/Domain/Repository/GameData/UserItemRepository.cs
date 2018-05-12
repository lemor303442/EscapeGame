public class UserItemRepository : BaseGameDataRepository<UserItem>
{
    public static UserItem FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }
    public static UserItem FindByItemId(int itemId)
    {
        return DataList.Find(x => x.ItemId == itemId);
    }
}
