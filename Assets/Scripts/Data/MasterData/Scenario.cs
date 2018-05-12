using System.Text.RegularExpressions;
using UnityEngine;

public class Scenario
{
    public int Id { get; private set; }
    public string Command { get; private set; }
    public string Arg1 { get; private set; }
    public string Arg2 { get; private set; }
    public string Arg3 { get; private set; }
    public string Arg4 { get; private set; }
    public string Arg5 { get; private set; }
    public string Arg6 { get; private set; }
    public string Text { get; private set; }

    public Scenario(
        int _id,
        string _command,
        string _arg1,
        string _arg2,
        string _arg3,
        string _arg4,
        string _arg5,
        string _arg6,
        string _text
    )
    {
        Validation(
            _id,
            _command,
            _arg1,
            _arg2,
            _arg3,
            _arg4,
            _arg5,
            _arg6,
            _text
        );
        Id = _id;
        Command = _command;
        Arg1 = _arg1;
        Arg2 = _arg2;
        Arg3 = _arg3;
        Arg4 = _arg4;
        Arg5 = _arg5;
        Arg6 = _arg6;
        Text = _text;
    }

    private void Validation(
        int _id,
        string _command,
        string _arg1,
        string _arg2,
        string _arg3,
        string _arg4,
        string _arg5,
        string _arg6,
        string _text
    )
    {
        switch (_command)
        {
            case "":
                if (!string.IsNullOrEmpty(_arg3) && !Regex.IsMatch(_arg3, @"^([1-9]\d*|0)(\.\d+)?$"))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg3]\n" +
                                   "Must be a float number between 0.0 ~ 1.0");
                if (!string.IsNullOrEmpty(_arg4) && !Regex.IsMatch(_arg4, @"^([1-9]\d*|0)(\.\d+)?$"))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg4]\n" +
                                   "Must be a float number between 0.0 ~ 1.0");
                break;
            case "Jump":
                if (string.IsNullOrEmpty(_arg1))
                {
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg1]\n" +
                                   "Jump.Arg1 must not be null.");
                }
                else if (_arg1.Remove(1) != "*")
                {
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg1]\n" +
                                   "Jump.Arg1 must start with \"*\".");
                }
                break;
            case "Selection":
                if (string.IsNullOrEmpty(_arg1))
                {
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg1]\n" +
                                   "Selection.Arg1 must not be null.");
                }
                else if (_arg1.Remove(1) != "*")
                {
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg1]\n" +
                                   "Selection.Arg1 must start with \"*\".");
                }
                break;
            case "Bg":
                if (string.IsNullOrEmpty(_arg1))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg1]\n" +
                                   "Bg.Arg1 must not be null.");
                if (string.IsNullOrEmpty(_arg2))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg2]\n" +
                                   "Bg.Arg2 must not be null.");
                break;
            case "BgOff":
                if (string.IsNullOrEmpty(_arg1))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg1]\n" +
                                   "BgOff.Arg1 must not be null.");
                break;
            case "Character":
                if (string.IsNullOrEmpty(_arg1))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg1]\n" +
                                   "Character.Arg1 must not be null.");
                if (string.IsNullOrEmpty(_arg2))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg2]\n" +
                                   "Character.Arg2 must not be null.");
                if (string.IsNullOrEmpty(_arg3))
                    Debug.LogError("Scenario Validation Error at [" + _id.ToString() + ".Arg3]\n" +
                                   "Character.Arg3 must not be null.");
                break;
        }
    }
}
