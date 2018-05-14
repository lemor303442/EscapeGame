using System.Collections.Generic;

public class EscapeSceneRepository : BaseMasterDataRepository<EscapeScene>
{
    public static EscapeScene FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static EscapeScene FindByName(string name)
    {
        return DataList.Find(x => x.Name == name);
    }
}
