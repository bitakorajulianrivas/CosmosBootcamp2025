namespace Katas.Battleship.Tests;

public class Jugador
{
    private const string YaExisteBarcoEnLaPosiciónEnviada = "Ya existe barco en la posición enviada.";
    private const string SoloSePuedeAsignarBarcosDeTipo = "Solo se puede asignar {0} barcos de tipo {1}.";
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }

    private Dictionary<TipoBarco, int> _barcosAsignados = new()
    {
        { TipoBarco.Carrier, 0 },
        { TipoBarco.Destroyer, 0 },
        { TipoBarco.Gunship, 0 }
    };

    public Jugador(string apodo)
    {
        Apodo = apodo;
        Tablero = new char[10, 10];
    }


    public void AgregarBarco(Barco barco, int x, int y, bool esVertical = false)
    {
        if (x >= 10 || y >= 10  || x<0 || y<0 )
            throw new ArgumentException("El barco se encuentra fuera del tablero.");
        if (Tablero[x, y] != '\0')
            throw new ArgumentException(YaExisteBarcoEnLaPosiciónEnviada);
        
        ValidarBarcosAsignados(barco);
        AsignarBarco(barco, x, y, esVertical);
    }

    private void ValidarBarcosAsignados(Barco barco)
    {
        if (_barcosAsignados[barco.Tipo] >= barco.CantidadBarcos)
            throw new ArgumentException(string.Format(SoloSePuedeAsignarBarcosDeTipo, barco.CantidadBarcos, barco.Tipo));
    }

    private void AsignarBarco(Barco barco, int x, int y, bool esVertical)
    {
        for (int indice = 0; indice < barco.Tamanio; indice++)
        {
            if (esVertical)
                Tablero[x, y + indice] = barco.Letra;
            else
                Tablero[x + indice, y] = barco.Letra;
        }

        _barcosAsignados[barco.Tipo]++;
    }
}