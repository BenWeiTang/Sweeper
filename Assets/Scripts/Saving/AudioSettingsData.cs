namespace Minesweeper.Saving
{
    public class AudioSettingsData
    {
        public float MasterVolume;
        public float BGMVolume;
        public float EffectVolume;
        public bool Mute;
        public bool MuteBGM;
        public bool MuteEffect;

        public AudioSettingsData()
        {
            MasterVolume = 100f;
            BGMVolume = 100f;
            EffectVolume = 100f;
            Mute = false;
            MuteBGM = false;
            MuteEffect = false;
        }
    }
}