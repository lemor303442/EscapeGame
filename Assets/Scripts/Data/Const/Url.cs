using System.Collections.Generic;

namespace Const
{
    public static class Url
    {
        public static readonly string character;
        public static readonly string escapeInput;
        public static readonly string escapeScene;
        public static readonly string hint;
        public static readonly string item;
        public static readonly string layer;
        public static readonly string parameter;
        public static readonly string scenario;

        static Url()
        {
            string baseUrl = TextFileHelper.Read("Locals/Configs/BaseUrl.txt");
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("Character", TextFileHelper.Read("Locals/Configs/CharacterKey.txt"));
            keys.Add("EscapeInput", TextFileHelper.Read("Locals/Configs/EscapeInputKey.txt"));
            keys.Add("EscapeScene", TextFileHelper.Read("Locals/Configs/EscapeSceneKey.txt"));
            keys.Add("Hint", TextFileHelper.Read("Locals/Configs/HintKey.txt"));
            keys.Add("Item", TextFileHelper.Read("Locals/Configs/ItemKey.txt"));
            keys.Add("Layer", TextFileHelper.Read("Locals/Configs/LayerKey.txt"));
            keys.Add("Parameter", TextFileHelper.Read("Locals/Configs/ParameterKey.txt"));
            keys.Add("Scenario", TextFileHelper.Read("Locals/Configs/ScenarioKey.txt"));

            character = baseUrl + "/export?format=csv&gid=" + keys["Character"];
            escapeInput = baseUrl + "/export?format=csv&gid=" + keys["EscapeInput"];
            escapeScene = baseUrl + "/export?format=csv&gid=" + keys["EscapeScene"];
            hint = baseUrl + "/export?format=csv&gid=" + keys["Hint"];
            item = baseUrl + "/export?format=csv&gid=" + keys["Item"];
            layer = baseUrl + "/export?format=csv&gid=" + keys["Layer"];
            parameter = baseUrl + "/export?format=csv&gid=" + keys["Parameter"];
            scenario = baseUrl + "/export?format=csv&gid=" + keys["Scenario"];
        }
    }
}
