public class HintHelper {
    public static bool IsHintVailable(int i){
        foreach (var hint in HintRepository.GetHints(i))
        {
            if (ConditionHelper.IsAllConditionValid(hint.Conditions))
            {
                return true;
            }
        }
        return false;
    }

    public static Hint GetHint(int i){
        foreach (var hint in HintRepository.GetHints(i))
        {
            if (ConditionHelper.IsAllConditionValid(hint.Conditions))
            {
                return hint;
            }
        }
        return null;
    }
}
