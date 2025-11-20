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
    private readonly char[,] _tablero;
    private readonly char[,] _tableroDisparos;
    
    private int _cantidadDisparosAcerdos;
    private int _cantidaDisparosFallidos;
    private readonly List<Barco> _barcosAsignados;
    
    public string Apodo { get; private set; }
    
    public Jugador(string apodo)
    {
        Apodo = apodo;
        _barcosAsignados = [];
        _tablero = new char[CasillaMaxima, CasillaMaxima];
        _tableroDisparos = (char[,])_tablero.Clone();
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
        if (_barcosAsignados.Count < CantidadMaximaDeBarcos)
            throw new ArgumentException(JugadorMensajes.FaltaBarcosPorAsignar);
    }



    private static void ValidarBordesDelTablero(Posicion posicion)
    {
        if (posicion.EjeX >= CasillaMaxima ||
            posicion.EjeY >= CasillaMaxima ||
            posicion.EjeX < 0 ||
            posicion.EjeY < 0)
            throw new ArgumentException(JugadorMensajes.ElBarcoSeEncuentraFueraDelTablero);
    }

    public EstadoDisparo ObtenerEstadoDisparo() => _disparo;
    
    public bool TieneTodosLosBarcosAsginados() => _barcosAsignados.Count == CantidadMaximaDeBarcos;

    private void ValidarSobreposicionDeBarcos(Posicion posicion)
    {
        if (_tablero[posicion.EjeX, posicion.EjeY] != CasillaPorDefecto)
            throw new ArgumentException(JugadorMensajes.YaExisteBarcoEnLaPosiciónEnviada);
    }
    
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
                _tablero[barco.Posicion.EjeX, barco.Posicion.EjeY + indice] = barco.Inicial;
            else
                _tablero[barco.Posicion.EjeX + indice, barco.Posicion.EjeY] = barco.Inicial;
        }
        
        _barcosAsignados.Add(barco);
    }

    public char RecibirDisparo(int x, int y)
    {
        if (ExisteBarcoEn(x, y))
        {
            _cantidadDisparosAcerdos++;

            _tablero[x, y] = MarcaTiroAcertado;
            Barco? barco = ObtenerBarco(x, y);
            barco?.Golpear();
            _disparo = EstadoDisparo.DisparoAcertado;

            if (barco!.EsDerribado())
            {
                _disparo = EstadoDisparo.BarcoHundido;

                foreach (var coordenada in barco.Coordenadas)
                    _tablero[coordenada.x, coordenada.y] = MarcaBarcoHundido;
            }
        }
        else
        {
            _tablero[x, y] = MarcaTiroFallido;
            _cantidaDisparosFallidos++;
            _disparo = EstadoDisparo.DisparoFallido;
        }

        return _tablero[x, y];
    }

    public Barco? ObtenerBarco(int x, int y)
    {
        return _barcosAsignados.FirstOrDefault(barco =>
            barco.ObtenerCoordenadas().Contains((x, y)));
    }


    public char RegistrarDisparo(int x, int y, char disparo) =>
        _tableroDisparos[x, y] = disparo;
    
    private bool ExisteBarcoEn(int x, int y) =>
        _tablero[x, y] != CasillaPorDefecto;
    
    public bool TieneDisparo(int x, int y)
    {
        return _tableroDisparos[x, y] != CasillaPorDefecto;
    }
    
    public string Imprimir(bool esReporte = false)
    {
        if (esReporte)
            return ImprimirReporte();
        return ImprimirTablero();
    }

    private string ImprimirTablero()
    {
        int columnas = CasillaMaxima;
        int filas = CasillaMaxima;
        
        string tablero = "\n" +
                         "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                         "-------------------------------------------| \n";

        for (int columna = 0; columna < columnas; columna++)
        {
            
            tablero += $" {columna} |";
            
            for (int fila = 0; fila < filas; fila++)
            {
                var casilla = _tablero[fila, columna] == CasillaPorDefecto 
                    ? MarcaCasillaVacia 
                    : _tablero[fila, columna];
                
                tablero += $" {casilla} |";
            }

            tablero += " \n";
        }

        tablero +=
            "-------------------------------------------| \n" +
            "\n";

        return tablero;
    }
    
    private string ImprimirReporte()
    {
        int disparosTotales = _cantidadDisparosAcerdos + _cantidaDisparosFallidos;
        
        var resultado = string.Format(
            "Total disparos: {0}.\n" + "Perdidos: {1}.\n" + "Acertados: {2}.\n",
            disparosTotales,
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


}