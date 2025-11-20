namespace Katas.Battleship.Tests;

public class Jugador
{
    private const int CasillaMaxima = 10;
    private const int CantidadMaximaDeBarcos = 7;

    private const char MarcaBarcoHundido = 'X';
    private const char MarcaTiroAcertado = 'x';
    private const char MarcaTiroFallido = 'o';
    
    private EstadoDisparo _disparo;
    private readonly char[,] _tablero;
    private readonly char[,] _tableroDisparos;
    
    private int _cantidadDisparosAcertados;
    private int _cantidaDisparosFallidos;
    private readonly List<Barco> _barcosAsignados;
    private readonly Reporte _reporte;

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
        if (_tablero[posicion.EjeX, posicion.EjeY] != '\0')
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
            _cantidadDisparosAcertados++;

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
    
    public bool TieneTodosLosBarcosDerribados()
    {
        int barcosDerribados = _barcosAsignados
            .Count(barco => barco.EsDerribado());
        
        return barcosDerribados == CantidadMaximaDeBarcos;
    }
    
    private bool ExisteBarcoEn(int x, int y) =>
        _tablero[x, y] != '\0';
    
    public bool TieneDisparo(int x, int y)
    {
        return _tableroDisparos[x, y] != '\0';
    }
    
    public string Imprimir(bool esReporte = false)
    {
        List<Barco> barcosDerribados = ObtenerBarcosDerribados();
        
        var reporte = new Reporte(_tablero, 
            _cantidadDisparosAcertados, 
            _cantidaDisparosFallidos,
            barcosDerribados);

        if (esReporte) 
            return reporte.ImprimirReporte();

        return reporte.ImprimirTablero();
    }

    private List<Barco> ObtenerBarcosDerribados()
    {
        List<Barco> barcosDerribados = _barcosAsignados
            .Where(barco => barco.EsDerribado()).ToList();
        return barcosDerribados;
    }
}