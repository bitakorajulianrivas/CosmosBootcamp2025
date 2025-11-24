using System.Reflection;
using Katas.Battleship.Core;

MostrarIntro();
var batalla = PrepararPartida();
IniciarPartida(batalla);
return;

void MostrarMensajeIniciandoPartida()
{
    Console.Clear();
    Console.WriteLine(ObtenerTextoDesdeArchivo("Starting"));
    Console.WriteLine("Presione enter para continuar.");
    Console.ReadKey();
}

void MostrarIntro()
{
    Console.Clear();
    Console.WriteLine(ObtenerTextoDesdeArchivo("Title"));
    Console.Write("\t Presione enter para continuar.");
    Console.ReadKey();
    Console.Clear();
}
BatallaNaval PrepararPartida()
{
    BatallaNavalBuilder builder = new BatallaNavalBuilder();
    
    Jugador jugador1 = CrearJugador(esPrimero: true);
    AsignarBarcos(jugador1);
    builder.AgregarJugador(jugador1);

    Jugador jugador2 = CrearJugador(esPrimero: false);
    AsignarBarcos(jugador2);
    builder.AgregarJugador(jugador2);

    return builder.Construir();

    Jugador CrearJugador(bool esPrimero)
    {
        while (true)
        {
            int numeroJugador = esPrimero ? 1 : 2;
            Console.Clear();
            Console.WriteLine($"Jugador {numeroJugador}");  
            Console.Write("Apodo: ");
            string? apodo = Console.ReadLine();

            try
            {
                return new Jugador(apodo);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }

    char ObtenerOrientacion()
    {
        while (true)
        {
            try
            {
                Console.Write("Orientacion horizontal (H), vertical (V): ");
                ConsoleKeyInfo teclaOrientacion = Console.ReadKey();
                if(teclaOrientacion.Key != ConsoleKey.H && teclaOrientacion.Key != ConsoleKey.V)
                {
                    throw new ArgumentException("La orientación debe ser horizontal (H) o vertical (V)");
                }
                return (char) teclaOrientacion.Key;
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
        }
    }

    Posicion CrearPosicionBarco(TipoBarco tipo, int numeroBarco)
    {
        Console.WriteLine($"Barco {numeroBarco} - {tipo}");
        char orientacion = ObtenerOrientacion();
        var x = ObtenerPosicion(esEjeX: true);
        var y = ObtenerPosicion(esEjeX: false);
    
        return orientacion == 'H' 
            ? Posicion.Horizontal(x, y) 
            : Posicion.Vertical(x, y);
    }

    void CrearBarco(Jugador jugador, TipoBarco tipoBarco, int numeroBarco)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(jugador.Imprimir());

                var posicion = CrearPosicionBarco(tipoBarco, numeroBarco);

                Barco barco = tipoBarco switch
                {
                    TipoBarco.Carrier => Barco.Carrier(posicion),
                    TipoBarco.Destroyer => Barco.Destroyer(posicion),
                    TipoBarco.Gunship => Barco.Gunship(posicion),
                    _ => throw new NotImplementedException()
                };

                jugador.AgregarBarco(barco);
                break;
            }
            catch (ArgumentException excepcion)
            {
                Console.WriteLine();
                Console.WriteLine(excepcion.Message);
            }
        }
    }

    void AsignarBarcos(Jugador jugador)
    {
        Console.Clear();
        CrearBarco(jugador, TipoBarco.Carrier, 1);
        Console.Clear();
        CrearBarco(jugador, TipoBarco.Destroyer, 2);
        Console.Clear();
        CrearBarco(jugador, TipoBarco.Destroyer, 3);
        Console.Clear();
        CrearBarco(jugador, TipoBarco.Gunship, 4);
        Console.Clear();
        CrearBarco(jugador, TipoBarco.Gunship, 5);
        Console.Clear();
        CrearBarco(jugador, TipoBarco.Gunship, 6);
        Console.Clear();
        CrearBarco(jugador, TipoBarco.Gunship, 7);
        Console.WriteLine();
        Console.Clear();
        Console.WriteLine(jugador.Imprimir());
        Console.WriteLine("Se han posicionado todos los barcos.");
        Console.WriteLine("\tPresione enter para continuar.");
        Console.Read();
    }
}

void IniciarPartida(BatallaNaval batallaNaval1)
{
    MostrarMensajeIniciandoPartida();
    batallaNaval1.Iniciar();
    while (!batallaNaval1.HaFinalizado)
    {
        Console.Clear();
        Console.WriteLine($"Turno {(batallaNaval1.EsTurnoPrincipal ? 1 : 2)} - Jugador {batallaNaval1.ApodoJugadorActual}:");
        Console.Write(batallaNaval1.ImprimirTableroDeDisparos());
        string mensaje = Disparar();
        Console.Clear();
        Console.WriteLine($"Turno {(batallaNaval1.EsTurnoPrincipal ? 1 : 2)} - Jugador {batallaNaval1.ApodoJugadorActual}:");
        Console.Write(batallaNaval1.ImprimirTableroDeDisparos());
        Console.WriteLine(mensaje);
        Console.ReadKey();
        batallaNaval1.FinalizarTurno();
    }
    
    MostrarGanador(batallaNaval1);
    MostrarEstadisticas(batallaNaval1);
    return;
    
    string Disparar()
    {
        while (true)
        {
            try
            {
            
                var x = ObtenerPosicion(esEjeX: true);
                var y = ObtenerPosicion(esEjeX: false);
                return batallaNaval1.Disparar(x, y);
            }
            catch (ArgumentException excepcion)
            {
                Console.WriteLine();
                Console.WriteLine(excepcion.Message);
            }
        }
    }

    void MostrarGanador(BatallaNaval batallaNaval)
    {
        Console.Clear();
        Console.WriteLine(ObtenerTextoDesdeArchivo("Winner"));
        Console.WriteLine(batallaNaval.MostrarJugadorGanador());
        Console.WriteLine("+============================================+");
        Console.Write("\tPresione enter para ver las estadísticas.");
        Console.ReadKey();
    }

    void MostrarEstadisticas(BatallaNaval batalla1)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(batalla1.Imprimir());
        Console.ReadKey();
    }
}

int ObtenerPosicion(bool esEjeX)
{
    while (true)
    {
        try
        {
            return ObtenerTeclaPosicion(esEjeX);
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine();
            Console.WriteLine(exception.Message);
        }
    }
}
int ObtenerTeclaPosicion(bool esEjeX)
{
    Console.WriteLine();
    Console.Write($"Posición {(esEjeX ? "X": "Y")}: ");
    ConsoleKeyInfo keyInfo = Console.ReadKey();
    bool esDigito = Char.IsDigit(keyInfo.KeyChar);
    
    if (!esDigito)
        throw new ArgumentException("El valor ingresado debe ser un numero.");
    
    return int.Parse(keyInfo.KeyChar.ToString());
}
string ObtenerTextoDesdeArchivo(string archivo)
{
    var ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
    string rutaArchivo = Path.Combine(ruta, "Recursos" , archivo);
    return File.ReadAllText(rutaArchivo);
}