namespace Katas.Battleship.Core;

public class Barco
{
    private int _golpes = 0;
    
    public TipoBarco Tipo { get; }
    public char Inicial { get; }
    public int Tamanio { get; }
    public int CantidadBarcos { get; }
    public Posicion Posicion { get; }
    public (int x, int y)[] Coordenadas { get; }
    

    public bool EsDerribado() => _golpes == Tamanio;
    
    public static Barco Gunship(Posicion posicion) => new(TipoBarco.Gunship, 1, 4, posicion);
    public static Barco Destroyer(Posicion posicion) => new(TipoBarco.Destroyer, 3, 2, posicion);
    public static Barco Carrier(Posicion posicion) => new(TipoBarco.Carrier, 4, 1, posicion);


    private Barco(TipoBarco tipo, int tamanio, int cantidadBarcos, Posicion posicion)
    {
        Tipo = tipo;
        Tamanio = tamanio;
        Inicial =Tipo.ToString()[0];
        CantidadBarcos = cantidadBarcos;
        Posicion = posicion;
        Coordenadas = ObtenerCoordenadas();
    }

    public void Golpear() => _golpes++;

    public (int x, int y)[] ObtenerCoordenadas()
    {
        (int x, int y)[] coordenadas = new (int x, int y)[Tamanio];

        for (int indice = 0; indice < Tamanio; indice++)
            coordenadas[indice] = Posicion.EsVertical
                ? (Posicion.EjeX, Posicion.EjeY + indice)
                : (Posicion.EjeX + indice, Posicion.EjeY);
        
        return coordenadas;
    }

    public override string ToString()
    {
        return Tipo + $": ({Posicion.EjeX},{Posicion.EjeY}).";
    }
}