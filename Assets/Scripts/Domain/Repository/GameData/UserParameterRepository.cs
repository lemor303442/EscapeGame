public class UserParameterRepository : BaseGameDataRepository<UserParameter>
{
    public static UserParameter FindById(int id)
    {
        return DataList.Find(x => x.Id == id);
    }

    public static UserParameter FindByParameterId(int parameterId)
    {
        return DataList.Find(x => x.ParameterId == parameterId);
    }
}
