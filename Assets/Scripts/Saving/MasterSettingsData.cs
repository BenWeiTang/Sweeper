using System;

namespace Minesweeper.Saving
{
    [Serializable]
    public class MasterSettingsData
    {
        public GeneralSettingsData GeneralSettingsData = new GeneralSettingsData();
        public AudioSettingsData AudioSettingsData = new AudioSettingsData();
        public ThemeSettingsData ThemeSettingsData = new ThemeSettingsData();
    }
}
