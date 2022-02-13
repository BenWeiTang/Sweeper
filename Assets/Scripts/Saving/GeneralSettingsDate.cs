namespace Minesweeper.Saving
{
    public class GeneralSettingsDate
    {
        public int Difficulty;
        public bool EasyClear;
        public bool ShowPostGameAnimation;

        public GeneralSettingsDate()
        {
            Difficulty = 0;
            EasyClear = false;
            ShowPostGameAnimation = true;
        }
    }
}