using System.IO;
using UnityEngine;

namespace Minesweeper.Saving
{
    public static class SettingsSerializer
    {
        public const string PATH = "/GameSettings.json";
        
        public static void SaveSettings(MasterSettingsData masterSettingsData)
        {
            string masterSettingsDataString = JsonUtility.ToJson(masterSettingsData);
            File.WriteAllText(Application.dataPath + PATH, masterSettingsDataString);
        }

        public static MasterSettingsData LoadSettings()
        {
            string json = File.ReadAllText(Application.dataPath + PATH);
            return JsonUtility.FromJson<MasterSettingsData>(json);
        }
    }
}