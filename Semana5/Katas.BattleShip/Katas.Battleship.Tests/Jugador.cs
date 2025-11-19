namespace Katas.Battleship.Tests;

public class Jugador
{
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }
    private int NumeroDeBarcosAsginados => _barcosAsignados.Sum(x => x.Value); 
    
    private readonly Dictionary<TipoBarco, int> _barcosAsignados = new()
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
    
    public void ValidarBarcos()
    {
        if ( NumeroDeBarcosAsginados < 7   ) 
            throw new ArgumentException(JugadorMensajes.FaltaBarcosPorAsignar);
    }
 
    public void AgregarBarco(Barco barco, Posicion posicion)
    {
        const int casillaMax = 10;
        const int casillaMin = 0;
        
        if (posicion.EjeX >= casillaMax || posicion.EjeY >= casillaMax  || 
            posicion.EjeX<casillaMin || posicion.EjeY<casillaMin )
            throw new ArgumentException(JugadorMensajes.ElBarcoSeEncuentraFueraDelTablero);
        if (Tablero[posicion.EjeX, posicion.EjeY] != '\0')
            throw new ArgumentException(JugadorMensajes.YaExisteBarcoEnLaPosiciónEnviada);
        
        ValidarBarcosAsignados(barco);
        AsignarBarco(barco, posicion.EjeX, posicion.EjeY, posicion.EsVertical);
    }


    public bool TieneTodosLosBarcosAsginados() => NumeroDeBarcosAsginados == 7;
    
    private void ValidarBarcosAsignados(Barco barco)
    {
        if (_barcosAsignados[barco.Tipo] >= barco.CantidadBarcos)
            throw new ArgumentException(string.Format(JugadorMensajes.SoloSePuedeAsignarBarcosDeTipo, barco.CantidadBarcos, barco.Tipo));
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

    public string ImprimirTablero()
    {
        string tablero = "\n" +
           "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
           "-------------------------------------------| \n";
        
        for (int columna = 0; columna < 10; columna++)
        {
            tablero += $" {columna} |";
            for (int fila = 0; fila <  10; fila++) 
                tablero += $" {ObtenerCasilla(fila, columna) } |";

            tablero += " \n";
        }

        tablero += 
            "-------------------------------------------| \n" +
            "\n";

        return tablero;
    }

    private char ObtenerCasilla(int fila, int columna) => 
        Tablero[fila, columna] == '\0' ? ' ' : Tablero[fila,columna];
}