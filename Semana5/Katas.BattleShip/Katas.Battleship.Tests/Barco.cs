namespace Katas.Battleship.Tests;

public class Barco
{
    public TipoBarco Tipo { get; }
    public int Tamanio { get; }
    public char Letra { get; }
    public int CantidadBarcos { get; }
    
    public static Barco Gunship() => new (TipoBarco.Gunship, 1, 'G', 4);
    public static Barco Destroyer() => new (TipoBarco.Destroyer, 3, 'D', 2);
    public static Barco Carrier() => new (TipoBarco.Carrier, 4, 'C', 1);

    private Barco(TipoBarco tipo, int tamanio, char letra, int cantidadBarcos)
    {
        Tipo = tipo;
        Tamanio = tamanio;
        Letra = letra;
        CantidadBarcos = cantidadBarcos;
    }
}