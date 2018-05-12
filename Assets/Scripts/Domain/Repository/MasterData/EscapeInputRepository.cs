using System.Collections.Generic;

public class EscapeInputRepository : BaseMasterDataRepository<EscapeInput>
{
    public static EscapeInput FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }
}
