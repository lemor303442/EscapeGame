using System.Text.RegularExpressions;

public class TextHelper
{
    public static string ReplaceTextTags(string text)
    {
        string newText = text.Replace("<br>", "\n");
        Regex regex = new Regex(@"\<param=[^\>]+\>");
        Match match = regex.Match(newText);
        while (match.Success)
        {
            string paramName = match.Value.Replace("<param=", "").Replace(">", "");
            UnityEngine.Debug.Log(paramName);
            newText = newText.Replace(match.Value, ParameterEntity.FindByParameterName(paramName).UserParameter.Value);
            match = match.NextMatch();
        }
        return newText;
    }
}
