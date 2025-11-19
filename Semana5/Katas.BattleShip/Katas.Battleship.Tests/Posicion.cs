namespace Katas.Battleship.Tests;

public record Posicion
{
    public static Posicion Horizontal(int ejeX, int ejeY) => new Posicion(ejeX, ejeY, false);
    public static Posicion Vertical(int ejeX, int ejeY) => new Posicion(ejeX, ejeY, true);

    private Posicion(int EjeX, int EjeY, bool EsVertical)
    {
        this.EjeX = EjeX;
        this.EjeY = EjeY;
        this.EsVertical = EsVertical;
    }

    public int EjeX { get; }
    public int EjeY { get; }
    public bool EsVertical { get; }
}