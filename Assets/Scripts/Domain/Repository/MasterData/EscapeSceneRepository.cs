using System.Collections.Generic;

public class EscapeSceneRepository : BaseMasterDataRepository<EscapeScene>
{
    public static EscapeScene FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }
}
