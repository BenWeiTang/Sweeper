using System.IO;
using UnityEngine;

namespace Minesweeper.Saving
{
    public static class SettingsSerializer
    {
        private const string PATH = "Settings.json";
        
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