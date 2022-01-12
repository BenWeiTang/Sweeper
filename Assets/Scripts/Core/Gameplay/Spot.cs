namespace Minesweeper.Core
{
    [System.Serializable]
    public class Spot
    {
        private bool _isMine;
        public bool IsMine => _isMine;
        public int HintNumber = 0;
        public SpotState State {get; set;} = SpotState.Untouched;
        
        public void SetMine(bool isMine)
        {
            _isMine = isMine;
        }
    }

    [System.Serializable]
    public enum SpotState
    {
        Untouched,
        Dug,
        Marked,
        Detonated
    }
}
