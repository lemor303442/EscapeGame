public class HintRepository : BaseMasterDataRepository<Hint>
{
    public static Hint FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }
}
