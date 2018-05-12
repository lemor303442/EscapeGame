public class ParameterEntity
{
    public Parameter Parameter { get; private set; }
    public UserParameter UserParameter { get; private set; }

    public ParameterEntity(Parameter _parameter, UserParameter _userParameter)
    {
        Parameter = _parameter;
        UserParameter = _userParameter;
    }

    public static ParameterEntity FindByUserParamaterId(int id)
    {
        UserParameter userParameter = UserParameterRepository.FindById(id);
        Parameter parameter = ParameterRepository.FindById(userParameter.Id);
        return new ParameterEntity(parameter, userParameter);
    }

    public static ParameterEntity FindByParameterName(string name)
    {
        Parameter parameter = ParameterRepository.FindByName(name);
        UserParameter userParameter = UserParameterRepository.FindByParameterId(parameter.Id);
        return new ParameterEntity(parameter, userParameter);
    }
}
