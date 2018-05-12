using System.Collections.Generic;

public class EscapeInputRepository : BaseMasterDataRepository<EscapeInput>
{
    public static EscapeInput FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static List<EscapeInput> FindBySceneName(string name)
    {
        List<EscapeInput> list = new List<EscapeInput>();
        foreach(var escapeInput in DataList){
            if (escapeInput.SceneName == name) list.Add(escapeInput);
        }
        return list;
    }
}
