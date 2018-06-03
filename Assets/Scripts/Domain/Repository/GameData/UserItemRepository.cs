using System.Collections.Generic;

public class UserItemRepository : BaseGameDataRepository<UserItem>
{
    public static List<UserItem> OwnedItems
    {
        get
        {
            List<UserItem> userItemList = new List<UserItem>();
            foreach (var data in DataList)
            {
                if (data.IsOwned) userItemList.Add(data);
            }
            return userItemList;
        }
    }

    public static UserItem FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }
    public static UserItem FindByItemId(int itemId)
    {
        return DataList.Find(x => x.ItemId == itemId);
    }
}
