namespace LigaGame.UI.Score
{
    public interface IScoreItem
    {
        void SetState(ScoreItemState state);
    }

    public enum ScoreItemState
    {
        Inactived,
        Unmarked,
        Marked,
    }
}
