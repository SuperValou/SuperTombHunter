namespace Assets.Scripts
{
    public interface ITile
    {
        int Row { get; }

        int Column { get; }

        TileType Type { get; set; }

        TileState State { get; set; }

        bool CanBeHolded { get; set; }
    }
}