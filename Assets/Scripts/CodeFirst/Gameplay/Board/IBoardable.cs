namespace CodeFirst.Gameplay
{
    public interface IBoardable
    {
        void Load(int? x, int? y);
        void Clear();
    }
}