using System;

namespace Minesweeper.Saving
{
    [Serializable]
    public class GeneralSettingsData
    {
        public int Difficulty;
        public bool EasyClear;

        public GeneralSettingsData()
        {
            Difficulty = 0;
            EasyClear = false;
        }
    }
}