using System;

namespace Minesweeper.Saving
{
    [Serializable]
    public class MasterSettingsData
    {
        public GeneralSettingsData GeneralSettingsData = new GeneralSettingsData();
        public AudioSettingsData AudioSettingsData = new AudioSettingsData();
        public CustomizeSettingsData CustomizeSettingsData = new CustomizeSettingsData();
    }
}
