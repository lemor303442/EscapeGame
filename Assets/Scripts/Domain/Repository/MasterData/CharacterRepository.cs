using System.Collections.Generic;

public class CharacterRepository : BaseMasterDataRepository<Character>
{
    public static Character FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static Character FindByPattern(string name, string pattern)
    {
        return DataList.Find(x => x.Name == name && x.Pattern == pattern);
    }
}
