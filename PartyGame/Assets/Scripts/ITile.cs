namespace Assets.Scripts
{
    public interface ITile
    {
        int Row { get; }

        int Column { get; }

        TileType Type { get; }

        TileState State { get; }

        bool CanBeHolded { get; }

        void Hold();

        void Drop();
    }
}