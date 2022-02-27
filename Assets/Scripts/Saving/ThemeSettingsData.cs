using System;

namespace Minesweeper.Saving
{
    [Serializable]
    public class ThemeSettingsData
    {
        public int ThemeID;

        public ThemeSettingsData()
        {
            ThemeID = 0;
        }
    }
}