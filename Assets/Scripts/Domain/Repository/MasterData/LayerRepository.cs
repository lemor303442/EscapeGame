using System.Collections.Generic;

public class LayerRepository : BaseMasterDataRepository<Layer>
{
    public static List<Layer> All
    {
        get
        {
            return DataList;
        }
    }

    public static Layer FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static Layer FindByName(string name)
    {
        return DataList.Find(x => x.Name == name);
    }

    public static void SortByOrder()
    {
        DataList.Sort((Layer layer1, Layer layer2) => {
            return layer1.Order.CompareTo(layer2.Order);
        });
    }
}
