using System.Collections.Generic;

public class ScenarioRepository : BaseMasterDataRepository<Scenario>
{
    public static int Count
    {
        get
        {
            return DataList.Count;
        }
    }

    public static Scenario FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static Scenario FindByCommand(string commnad)
    {
        return DataList.Find(x => x.Command == commnad);
    }

    public static List<Scenario> GetSelections(int startId)
    {
        if (DataList.Find(x => x.Id == startId).Command != "Selection") throw new System.Exception("Scenario.id[" + startId.ToString() + "].command is not \"Selection\".");
        List<Scenario> selectionList = new List<Scenario>();
        for (int i = 0; ;i++)
        {
            Scenario scenario = DataList.Find(x => x.Id == startId + i);
            if (scenario.Command == "Selection") selectionList.Add(scenario);
            else break;
        }
        return selectionList;
    }
}
