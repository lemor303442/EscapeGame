using System.Collections.Generic;

public class ParameterRepository : BaseMasterDataRepository<Parameter>
{
    public static Parameter FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static Parameter FindByName(string name)
    {
        return DataList.Find(x => x.Name == name);
    }
}
