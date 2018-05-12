 public class UserParameter
{
    public int Id { get; private set; }
    public int ParameterId { get; private set; }
    public string Value { get; private set; }

    public UserParameter(int _id, int _parameterId, string _value)
    {
        Id = _id;
        ParameterId = _parameterId;
        Value = _value;
    }

    public void UpdateValue(string _value){
        Value = _value;
    }
}
