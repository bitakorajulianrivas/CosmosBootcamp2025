namespace Katas.Battleship.Tests;

public class Jugador
{
    private const int CasillaMaxima = 10;
    private const int CasillaMinima = 0;
    private readonly int _cantidadMaximaDeBarcos = 7;
    private const char CasillaPorDefecto = '\0';
    private const char CasillaVacia = ' ';

    
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }
    public char[,] TableroDisparos { get; set; }
    
    private int NumeroDeBarcosAsginados => _barcosAsignados
        .Sum(x => x.Value);
    
    private readonly Dictionary<TipoBarco, int> _barcosAsignados = new()
    {
        { TipoBarco.Carrier, 0 },
        { TipoBarco.Destroyer, 0 },
        { TipoBarco.Gunship, 0 }
    };
    

    public Jugador(string apodo)
    {
        Apodo = apodo;
        Tablero = new char[CasillaMaxima, CasillaMaxima];
        TableroDisparos = (char[,])Tablero.Clone();
    }



    public void AgregarBarco(Barco barco, Posicion posicion)
    {
        ValidarBordesDelTablero(posicion);
        ValidarSobreposicionDeBarcos(posicion);
        ValidarCantidadDeBarcosAsignadosPorTipo(barco);
        
        AsignarBarco(barco, posicion.EjeX, posicion.EjeY, posicion.EsVertical);
    }

    public void ValidarQueExistanSieteBarcosAsignadosPorTablero()
    {
        if (NumeroDeBarcosAsginados < _cantidadMaximaDeBarcos)
            throw new ArgumentException(JugadorMensajes.FaltaBarcosPorAsignar);
    }
    
    private void ValidarSobreposicionDeBarcos(Posicion posicion)
    {
        if (Tablero[posicion.EjeX, posicion.EjeY] != CasillaPorDefecto)
            throw new ArgumentException(JugadorMensajes.YaExisteBarcoEnLaPosiciónEnviada);
    }

    private static void ValidarBordesDelTablero(Posicion posicion)
    {
        if (posicion.EjeX >= CasillaMaxima || 
            posicion.EjeY >= CasillaMaxima ||
            posicion.EjeX < CasillaMinima || 
            posicion.EjeY < CasillaMinima)
            throw new ArgumentException(JugadorMensajes.ElBarcoSeEncuentraFueraDelTablero);
    }


    public bool TieneTodosLosBarcosAsginados() => NumeroDeBarcosAsginados == _cantidadMaximaDeBarcos;

    private void ValidarCantidadDeBarcosAsignadosPorTipo(Barco barco)
    {
        if (_barcosAsignados[barco.Tipo] >= barco.CantidadBarcos)
            throw new ArgumentException(string.Format(JugadorMensajes.SoloSePuedeAsignarBarcosDeTipo,
                barco.CantidadBarcos, barco.Tipo));
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

    public char RecibirDisparo(int x, int y) => 
        Tablero[x, y] = ExisteBarcoEn(x, y) ? 'x' : 'o';

    public char RegistrarDisparo(int x, int y, char disparo) => 
        TableroDisparos[x, y] = disparo;
    
    public string ImprimirTablero()
    {
        string tablero = "\n" +
                         "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                         "-------------------------------------------| \n";

        for (int columna = 0; columna < 10; columna++)
        {
            tablero += $" {columna} |";
            for (int fila = 0; fila < 10; fila++)
                tablero += $" {ObtenerCasilla(fila, columna)} |";

            tablero += " \n";
        }

        tablero +=
            "-------------------------------------------| \n" +
            "\n";

        return tablero;
    }

    private char ObtenerCasilla(int fila, int columna)
    {
        return Tablero[fila, columna] == CasillaPorDefecto ? CasillaVacia : Tablero[fila, columna];
    }

    private bool ExisteBarcoEn(int x, int y) => 
        Tablero[x, y] != CasillaPorDefecto;


} 