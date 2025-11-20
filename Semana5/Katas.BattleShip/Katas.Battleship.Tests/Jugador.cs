namespace Katas.Battleship.Tests;

public class Jugador
{
    private const int CasillaMaxima = 10;
    private const int CasillaMinima = 0;
    private readonly int _cantidadMaximaDeBarcos = 7;
    private const char CasillaPorDefecto = '\0';
    private const char CasillaVacia = ' ';
    private int _cantidadDisparosAcerdos = 0;
    private int _cantidaDisparosFallidos = 0;
    private int CantidadDisparosTotales => _cantidadDisparosAcerdos + _cantidaDisparosFallidos;
    private EstadoDisparo _disparo;
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }
    public char[,] TableroDisparos { get; set; }
    public List<Barco> Barcos;

    public EstadoDisparo ObtenerEstadoDisparo()
    {
        return _disparo;
    }

    private int NumeroDeBarcosAsginados => _barcosAsignados
        .Sum(x => x.Value);
    
    private readonly Dictionary<TipoBarco, int> _barcosAsignados = new()
    {
        { TipoBarco.Carrier, 0 },
        { TipoBarco.Destroyer, 0 },
        { TipoBarco.Gunship, 0 }
    };

    private char MarcaBarcoDerribado ='x';


    public Jugador(string apodo)
    {
        Apodo = apodo;
        Barcos = [];
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

    private void AsignarBarco(Barco barco)
    {
        for (int indice = 0; indice < barco.Tamanio; indice++)
        {
            if (barco.Posicion.EsVertical)
                Tablero[barco.Posicion.EjeX, barco.Posicion.EjeY + indice] = barco.Inicial;
            else
                Tablero[barco.Posicion.EjeX + indice, barco.Posicion.EjeY] = barco.Inicial;
        }

        _barcosAsignados[barco.Tipo]++;
        Barcos.Add(barco);
    }

    public char RecibirDisparo(int x, int y)
    {
        if(ExisteBarcoEn(x, y))
        {
            _cantidadDisparosAcerdos++;
           
            Tablero[x, y] = MarcaBarcoDerribado;
            var barco = ObtenerBarco(x, y);
            if(barco != null)
                barco.Golpear();
            _disparo = EstadoDisparo.DisparoAcertado;
            if (barco.EsDerribado())
            {
                _disparo = EstadoDisparo.BarcoHundido;
            }
        }
        else
        {
            Tablero[x, y] = 'o';
            _cantidaDisparosFallidos++;
            _disparo = EstadoDisparo.DisparoFallido;
        }

        return Tablero[x, y];
    }

    private Barco? ObtenerBarco(int x, int y)
    {
        return Barcos.FirstOrDefault(barco =>
            barco.ObtenerCoordenadas().Contains((x, y)));
    }


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


    public string ObtenerInforme()
    {
        var resultado = string.Format(
            "Total disparos: {0}.\n" + "Perdidos: {1}.\n" + "Acertados: {2}.\n",
            CantidadDisparosTotales,
            _cantidaDisparosFallidos,
            _cantidadDisparosAcerdos);

        var barcos = Barcos
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
} 