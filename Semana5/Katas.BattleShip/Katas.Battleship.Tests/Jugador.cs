namespace Katas.Battleship.Tests;

public class Jugador
{
    private const int CasillaMaxima = 10;

    private const int CantidadMaximaDeBarcos = 7;
    
    private const char CasillaPorDefecto = '\0';
    private const char MarcaCasillaVacia = ' ';
    private const char MarcaBarcoHundido = 'X';
    private const char MarcaTiroAcertado = 'x';
    private const char MarcaTiroFallido = 'o';


    
    private EstadoDisparo _disparo;
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }
    public char[,] TableroDisparos { get; set; }
    
    private readonly List<Barco> _barcosAsignados;
    private int _cantidadDisparosAcerdos;
    private int _cantidaDisparosFallidos;
    private int CantidadDisparosTotales => 
        _cantidadDisparosAcerdos + _cantidaDisparosFallidos;

    public EstadoDisparo ObtenerEstadoDisparo()
    {
        return _disparo;
    }

    private int NumeroDeBarcosAsignados => _barcosAsignados.Count;



    public Jugador(string apodo)
    {
        Apodo = apodo;
        _barcosAsignados = [];
        Tablero = new char[CasillaMaxima, CasillaMaxima];
        TableroDisparos = (char[,])Tablero.Clone();
    }


    public void AgregarBarco(Barco barco)
    {
        ValidarBordesDelTablero(barco.Posicion);
        ValidarSobreposicionDeBarcos(barco.Posicion);
        ValidarCantidadDeBarcosAsignadosPorTipo(barco);

        AsignarBarco(barco);
    }

    public void ValidarQueExistanSieteBarcosAsignadosPorTablero()
    {
        if (NumeroDeBarcosAsignados < CantidadMaximaDeBarcos)
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
            posicion.EjeX < 0 ||
            posicion.EjeY < 0)
            throw new ArgumentException(JugadorMensajes.ElBarcoSeEncuentraFueraDelTablero);
    }


    public bool TieneTodosLosBarcosAsginados() => NumeroDeBarcosAsignados == CantidadMaximaDeBarcos;

    private void ValidarCantidadDeBarcosAsignadosPorTipo(Barco barco)
    {
        if (ObtenerCantidadBarcosPorTipo(barco.Tipo) >= barco.CantidadBarcos)
            throw new ArgumentException(string.Format(JugadorMensajes.SoloSePuedeAsignarBarcosDeTipo,
                barco.CantidadBarcos, barco.Tipo));
    }

    private int ObtenerCantidadBarcosPorTipo(TipoBarco tipo)
    {
        return _barcosAsignados
            .Count(barco => barco.Tipo == tipo);
    }

    private void AsignarBarco(Barco barco)
    {
        for (int indice = 0; indice < barco.Tamanio; indice++)
        {
            if (barco.Posicion.EsVertical)
                Tablero[barco.Posicion.EjeX, barco.Posicion.EjeY + indice] = barco.Inicial;
            else
                Tablero[barco.Posicion.EjeX + indice, barco.Posicion.EjeY] = barco.Inicial;
        }
        
        _barcosAsignados.Add(barco);
    }

    public char RecibirDisparo(int x, int y)
    {
        if (ExisteBarcoEn(x, y))
        {
            _cantidadDisparosAcerdos++;

            Tablero[x, y] = MarcaTiroAcertado;
            Barco? barco = ObtenerBarco(x, y);
            if (barco != null)
                barco.Golpear();
            _disparo = EstadoDisparo.DisparoAcertado;

            if (barco.EsDerribado())
            {
                _disparo = EstadoDisparo.BarcoHundido;

                foreach (var coordenada in barco.Coordenadas)
                    Tablero[coordenada.x, coordenada.y] = MarcaBarcoHundido;
            }
        }
        else
        {
            Tablero[x, y] = MarcaTiroFallido;
            _cantidaDisparosFallidos++;
            _disparo = EstadoDisparo.DisparoFallido;
        }

        return Tablero[x, y];
    }

    public Barco? ObtenerBarco(int x, int y)
    {
        return _barcosAsignados.FirstOrDefault(barco =>
            barco.ObtenerCoordenadas().Contains((x, y)));
    }


    public char RegistrarDisparo(int x, int y, char disparo) =>
        TableroDisparos[x, y] = disparo;

    public string ImprimirTablero()
    {
        string tablero = "\n" +
                         "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                         "-------------------------------------------| \n";

        for (int columna = 0; columna < CasillaMaxima; columna++)
        {
            tablero += $" {columna} |";
            for (int fila = 0; fila < CasillaMaxima; fila++)
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
        return Tablero[fila, columna] == CasillaPorDefecto ? MarcaCasillaVacia : Tablero[fila, columna];
    }

    private bool ExisteBarcoEn(int x, int y) =>
        Tablero[x, y] != CasillaPorDefecto;


    public string ObtenerInforme()
    {
        var resultado = string.Format(
            "Total disparos: {0}.\n" + "Perdidos: {1}.\n" + "Acertados: {2}.\n",
            CantidadDisparosTotales,
            _cantidaDisparosFallidos,
            _cantidadDisparosAcerdos);

        var barcos = _barcosAsignados
            .Where(barco => barco.EsDerribado())
            .Select(barco => string.Format("{0}: ({1},{2}).\n",
                barco.Tipo, barco.Posicion.EjeX, barco.Posicion.EjeY))
            .ToArray();

        var barcosDerribados = barcos.Any();

        string informeBarcosDerribados =
            barcosDerribados
                ? "Barcos derribados: [" + string.Join("", barcos) + "]"
                : string.Empty;

        return resultado + informeBarcosDerribados;
    }

    public bool TieneDisparo(int x, int y)
    {
        return TableroDisparos[x, y] != CasillaPorDefecto;
    }
}