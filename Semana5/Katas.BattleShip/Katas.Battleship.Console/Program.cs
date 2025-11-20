using Katas.Battleship.Core;

namespace ContextMenu;

class Program
{
    static void Main(string[] args)
    {
        InstanciarMenu();
    }

    private void InstanciarMenu()
    {
        BatallaNaval batalla = new BatallaNaval();
        
        var menu = new InteractiveMenu(@"
           ___       __  __  __    ______   _        
          / _ )___ _/ /_/ /_/ /__ / __/ /  (_)__  ___
         / _  / _ `/ __/ __/ / -_)\ \/ _ \/ / _ \(_-<
        /____/\_,_/\__/\__/_/\__/___/_//_/_/ .__/___/
                                          /_/        ");

        // Add menu items
        menu.AddMenuItem("Agregar jugador", Opcion1);
        menu.AddMenuItem("Iniciar", CalculateSum);
        menu.AddMenuItem("Disparar", ShowSystemInfo);
        menu.AddMenuItem("Salir", () => Console.WriteLine("Goodbye!"), isExit: true);

        // Display the menu
        menu.Display();
    }

    Jugador Opcion1(BatallaNaval batalla)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Nickname: ");
        string? apodo = Console.ReadLine();
        // _juego.AgregarJugador(new Jugador(apodo!));
        Console.WriteLine("Barco 1 - Carrier: ");
        Console.WriteLine("Orientacion Horizontal (H) , Vertical (V) : ");
        string? orientacion = Console.ReadLine();
        Console.WriteLine("Posicion (XY):");
        string? posicion = Console.ReadLine();
        Barco.Carrier(orientacion == "H"
            ? Posicion.Horizontal(posicion![0], posicion[1])
            : Posicion.Vertical(posicion![0], posicion[1]));
        Console.WriteLine("Barco 2 - Destroyer: ");
        Console.WriteLine("Orientacion Horizontal (H) , Vertical (V) : ");
        string? orientacionD1 = Console.ReadLine();
        Console.WriteLine("Posicion (XY):");
        string? posicionD1 = Console.ReadLine();
        Barco.Carrier(orientacion == "H"
            ? Posicion.Horizontal(posicionD1![0], posicionD1[1])
            : Posicion.Vertical(posicionD1![0], posicionD1[1]));
        Console.ResetColor();
    }

    static void DisplayMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Hello! This is a sample message from the menu system.");
        Console.WriteLine("You can customize this action to do whatever you need!");
        Console.ResetColor();
    }

    static void CalculateSum()
    {
        Console.Write("Enter first number: ");
        if (int.TryParse(Console.ReadLine(), out int num1))
        {
            Console.Write("Enter second number: ");
            if (int.TryParse(Console.ReadLine(), out int num2))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nResult: {num1} + {num2} = {num1 + num2}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number!");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid number!");
            Console.ResetColor();
        }
    }

    static void ShowSystemInfo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("SYSTEM INFORMATION");
        Console.WriteLine("─────────────────────────────────");
        Console.WriteLine($"OS: {Environment.OSVersion}");
        Console.WriteLine($"Machine Name: {Environment.MachineName}");
        Console.WriteLine($"User: {Environment.UserName}");
        Console.WriteLine($"Processors: {Environment.ProcessorCount}");
        Console.WriteLine($"64-bit OS: {Environment.Is64BitOperatingSystem}");
        Console.WriteLine($".NET Version: {Environment.Version}");
        Console.ResetColor();
    }

    static void ListNumbers()
    {
        Console.Write("Enter how many numbers to display (1-20): ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0 && count <= 20)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nNumber List:");
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"  {i}. Item number {i}");
            }
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter a valid number between 1 and 20!");
            Console.ResetColor();
        }
    }

    static void ShowDateTime()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("CURRENT DATE & TIME");
        Console.WriteLine("─────────────────────────────────");
        Console.WriteLine($"Date: {DateTime.Now:dddd, MMMM dd, yyyy}");
        Console.WriteLine($"Time: {DateTime.Now:HH:mm:ss}");
        Console.WriteLine($"UTC: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        Console.ResetColor();
    }
}
