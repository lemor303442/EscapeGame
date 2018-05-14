using UnityEngine;
using System.Collections.Generic;

namespace Const
{
    public static class Path
    {
        public static readonly string ablsolutePath;
        public static class MasterData
        {
            public static readonly string character = "Locals/Csv/MasterData/Characters.csv";
            public static readonly string escapeInput = "Locals/Csv/MasterData/EscapeInputs.csv";
            public static readonly string escapeScene = "Locals/Csv/MasterData/EscapeScenes.csv";
            public static readonly string item = "Locals/Csv/MasterData/Items.csv";
            public static readonly string layer = "Locals/Csv/MasterData/Layers.csv";
            public static readonly string parameter = "Locals/Csv/MasterData/Parameters.csv";
            public static readonly string scenario = "Locals/Csv/MasterData/Scenarios.csv";
        }
        public static class GameData
        {
            public static readonly string userItem = "Locals/Csv/GameData/UserItem.csv";
            public static readonly string userParameter = "Locals/Csv/GameData/UserParameter.csv";
        }

        static Path()
        {
#if UNITY_EDITOR
            ablsolutePath = Application.dataPath + "/Resources/";
#elif UNITY_ANDROID
            ablsolutePath = Application.persistentDataPath + "/";
#elif UNITY_IPHONE
            ablsolutePath = Application.persistentDataPath + "/";
#else
            ablsolutePath = Application.dataPath + "";
#endif
        }
    }
}
