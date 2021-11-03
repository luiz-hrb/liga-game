namespace LigaGame.UI
{
    public interface IScoreItem
    {
        void SetState(ScoreItemState state);
    }

    public enum ScoreItemState { Inactived, Unmarked, Marked }
}
