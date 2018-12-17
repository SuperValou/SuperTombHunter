namespace Assets.Scripts
{
    public interface ITile
    {
        int Row { get; }

        int Column { get; }
        TileType Type { get; set; }
    }
}