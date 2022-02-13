using UnityEngine;

namespace Minesweeper.Saving
{
    public class MasterSettingsData
    {
        public GeneralSettingsDate GeneralSettingsDate = new GeneralSettingsDate();
        public AudioSettingsData AudioSettingsData = new AudioSettingsData();
        public CustomizeSettingsData CustomizeSettingsData = new CustomizeSettingsData();
    }
}
