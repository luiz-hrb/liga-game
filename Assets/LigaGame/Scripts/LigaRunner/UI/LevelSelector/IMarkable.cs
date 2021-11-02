namespace LigaGame.UI.UI
{
    public interface IMarkable
    {
        void SetState(MarkableState state);
    }

    public enum MarkableState { Inactived, Unmarked, Marked }
}
