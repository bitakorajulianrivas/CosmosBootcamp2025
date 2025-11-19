namespace Katas.Battleship.Tests;

public class Jugador
{
    private const int CasillaMaxima = 10;
    private const int CasillaMinima = 0;
    private readonly int _cantidadMaximaDeBarcos = 7;
    private const char CasillaPorDefecto = '\0';
    private const char CasillaVacia = ' ';
    private int cantidadDisparosAcerdos = 0;
    private int cantidaDisparosFallidos = 0;
    bool tieneBarcosDerribados = false;
    private int cantidadDisparosTotales => cantidadDisparosAcerdos + cantidaDisparosFallidos;
    
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }
    public char[,] TableroDisparos { get; set; }
    public List<(Barco, Posicion)> Barcos; 
    
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
        Barcos = [];
        Tablero = new char[CasillaMaxima, CasillaMaxima];
        TableroDisparos = (char[,])Tablero.Clone();
    }



    public void AgregarBarco(Barco barco, Posicion posicion)
    {
        ValidarBordesDelTablero(posicion);
        ValidarSobreposicionDeBarcos(posicion);
        ValidarCantidadDeBarcosAsignadosPorTipo(barco);
        
        AsignarBarco(barco, posicion);
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

    private void AsignarBarco(Barco barco, Posicion posicion)
    {
        for (int indice = 0; indice < barco.Tamanio; indice++)
        {
            if (posicion.EsVertical)
                Tablero[posicion.EjeX, posicion.EjeY + indice] = barco.Letra;
            else
                Tablero[posicion.EjeX + indice, posicion.EjeY] = barco.Letra;
        }

        _barcosAsignados[barco.Tipo]++;
        Barcos.Add((barco, posicion));
    }

    public char RecibirDisparo(int x, int y)
    {
        if(ExisteBarcoEn(x, y))
        {
            Tablero[x, y] = 'x';
            cantidadDisparosAcerdos++;
        }
        else
        {
            Tablero[x, y] = 'o';
            cantidaDisparosFallidos++;
        }

        return Tablero[x, y];
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
        var resultado = string.Format("Total disparos: {0}.\n" + "Perdidos: {1}.\n" + "Acertados: {2}.\n",
            cantidadDisparosTotales,
            cantidaDisparosFallidos,
            cantidadDisparosAcerdos);


        
        string informeBarcosDerribados = 
            tieneBarcosDerribados
                ? "Barcos derribados: [" +
                                         "Gunship: (0,2).\n" +
                                         "Destroyer: (1,0). \n" + 
                                         "]"
                : string.Empty;
        
        return resultado + informeBarcosDerribados;
    }
} 