namespace Katas.Battleship.Tests;

public class Barco
{
    public TipoBarco Tipo { get; }
    public int Tamanio { get; }
    public char Letra { get; }
    public int CantidadBarcos { get; }
    public Posicion Posicion { get; }


    public static Barco Gunship(Posicion posicion) => new (TipoBarco.Gunship, 1, 'G', 4, posicion);
    public static Barco Destroyer(Posicion posicion) => new (TipoBarco.Destroyer, 3, 'D', 2, posicion);
    public static Barco Carrier(Posicion posicion) => new (TipoBarco.Carrier, 4, 'C', 1, posicion);
    
   
    
    private Barco(TipoBarco tipo, int tamanio, char letra, int cantidadBarcos, Posicion posicion)
    {
        Tipo = tipo;
        Tamanio = tamanio;
        Letra = letra;
        CantidadBarcos = cantidadBarcos;
        Posicion = posicion;
    }


    public (int x, int y)[] ObtenerCoordenadas()
    {
        throw new NotImplementedException();
    }
}
