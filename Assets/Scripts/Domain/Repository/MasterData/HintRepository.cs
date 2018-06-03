using System.Collections.Generic;

public class HintRepository : BaseMasterDataRepository<Hint>
{
    public static Hint FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static List<Hint> GetHints(int hintIndex)
    {
        List<Hint> hintList = new List<Hint>();
        foreach (var data in DataList)
        {
            if (data.HintIndex == hintIndex) hintList.Add(data);
        }
        return hintList;
    }
}
