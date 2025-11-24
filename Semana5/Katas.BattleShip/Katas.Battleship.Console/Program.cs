// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Katas.Battleship.Core;

string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Title.txt");

Console.Clear();
Console.WriteLine(File.ReadAllText(path));
Console.ReadKey();
Console.Clear();
// BatallaNavalBuilder builder = new BatallaNavalBuilder();
// Jugador jugador1 = CrearJugador(esPrimero: true);
// Console.WriteLine("---------------------------------------------------------------------------");
// CrearJugadorConBarcos(jugador1);
// Console.WriteLine("---------------------------------------------------------------------------");
// Jugador jugador2 = CrearJugador(esPrimero: false);
// Console.WriteLine("---------------------------------------------------------------------------");
// CrearJugadorConBarcos(jugador2);
//
// var batalla = builder
//     .AgregarJugador(jugador1)
//     .AgregarJugador(jugador2)
//     .Construir();

var batalla = new BatallaNavalBuilder()
    .AgregarJugador("Pollo", [
        Barco.Carrier(Posicion.Horizontal(1, 1)),
        Barco.Destroyer(Posicion.Horizontal(2, 2)),
        Barco.Destroyer(Posicion.Horizontal(3, 3)),
        Barco.Gunship(Posicion.Horizontal(4, 4)),
        Barco.Gunship(Posicion.Horizontal(5, 5)),
        Barco.Gunship(Posicion.Horizontal(6, 6)),
        Barco.Gunship(Posicion.Horizontal(7, 7)),
    ])
    .AgregarJugador("Gato", [
        Barco.Carrier(Posicion.Vertical(1, 4)),
        Barco.Destroyer(Posicion.Horizontal(1, 0)),
        Barco.Destroyer(Posicion.Vertical(8, 1)),
        Barco.Gunship(Posicion.Horizontal(0, 2)),
        Barco.Gunship(Posicion.Horizontal(0, 9)),
        Barco.Gunship(Posicion.Horizontal(3, 4)),
        Barco.Gunship(Posicion.Horizontal(6, 7)),
    ])
    .Construir();

batalla.Iniciar();

while (!batalla.JuegoFinalizado)
{
    Console.Clear();
    Console.WriteLine($"Turno {(batalla.EsTurnoPrincipal ? 1 : 2)} - Jugador {batalla.ApodoJugadorActual}:");
    Console.WriteLine(batalla.ImprimirTableroDeDisparos());
    string mensaje = Disparar(batalla);
    Console.Clear();
    Console.WriteLine(batalla.ImprimirTableroDeDisparos());
    Console.WriteLine(mensaje);
    Console.ReadKey();
    batalla.FinalizarTurno();
}

Console.Clear();
Console.WriteLine(batalla.Imprimir());
Console.Read();

void CrearJugadorConBarcos(Jugador jugador)
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
    Console.WriteLine(jugador.Imprimir());
    Console.Read();
}
    
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

Posicion CrearPosicionBarco(TipoBarco tipo, int numeroBarco)
{
    Console.WriteLine();
    Console.WriteLine("---------------------------------------------------------------------------");
    Console.WriteLine($"Barco {numeroBarco} - {tipo}");
    char orientacion = ObtenerOrientacion();
    var x = ObtenerPosicion(esEjeX: true);
    var y = ObtenerPosicion(esEjeX: false);
    Console.WriteLine("---------------------------------------------------------------------------");
    
    return orientacion == 'H' 
        ? Posicion.Horizontal(x, y) 
        : Posicion.Vertical(x, y);
}

char ObtenerOrientacion()
{
    while (true)
    {
        try
        {
            Console.WriteLine("Orientacion (H), (V): ");
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

int ObtenerTeclaPosicion(bool b)
{
    Console.WriteLine();
    Console.Write($"Posición {(b ? "X": "Y")}: ");
    ConsoleKeyInfo keyInfo = Console.ReadKey();
    bool esDigito = Char.IsDigit(keyInfo.KeyChar);
    
    if (!esDigito)
        throw new ArgumentException("El valor ingresado debe ser un numero.");
    
    return int.Parse(keyInfo.KeyChar.ToString());
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

string Disparar(BatallaNaval batallaNaval)
{
    while (true)
    {
        try
        {
            
            var x = ObtenerPosicion(esEjeX: true);
            var y = ObtenerPosicion(esEjeX: false);
            return batalla.Disparar(x, y);
        }
        catch (ArgumentException excepcion)
        {
            Console.WriteLine();
            Console.WriteLine(excepcion.Message);
        }
    }
    int ejeX = 0;
    int exeY = 0;
    batallaNaval.Disparar(ejeX, exeY);
}