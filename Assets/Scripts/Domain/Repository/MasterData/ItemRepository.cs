public class ItemRepository : BaseMasterDataRepository<Item>
{
    public static Item FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static Item FindByName(string name)
    {
        return DataList.Find(x => x.Name == name);
    }
}
