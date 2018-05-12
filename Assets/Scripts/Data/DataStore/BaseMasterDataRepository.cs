using System.Collections.Generic;
using System.Reflection;

public class BaseMasterDataRepository<T>
{
    private static List<T> dataList;
    protected static List<T> DataList
    {
        get
        {
            var dataSet = MasterDataManager.Instance.MasterDataSet;
            var property = typeof(MasterDataSet).GetProperty(typeof(T).Name + "List");
            try
            {
                dataList = property.GetValue(dataSet, null) as List<T>;
            }
            catch (TargetException)
            {
                UnityEngine.Debug.LogWarning(property.Name + " is null.");
                dataList = new List<T>();
            }
            return dataList;
        }
    }
}
