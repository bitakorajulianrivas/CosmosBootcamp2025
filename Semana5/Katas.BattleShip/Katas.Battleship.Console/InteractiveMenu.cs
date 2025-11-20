namespace ContextMenu;

/// <summary>
/// Interactive console menu with arrow key navigation
/// </summary>
public class InteractiveMenu
{
    private readonly List<MenuItem> _menuItems;
    private int _selectedIndex;
    private readonly string _title;

    public InteractiveMenu(string title)
    {
        _title = title;
        _menuItems = new List<MenuItem>();
        _selectedIndex = 0;
    }

    /// <summary>
    /// Add a menu item to the menu
    /// </summary>
    public void AddMenuItem(string title, Action action, bool isExit = false)
    {
        _menuItems.Add(new MenuItem(title, action, isExit));
    }

    /// <summary>
    /// Display and run the interactive menu
    /// </summary>
    public void Display()
    {
        ConsoleKey keyPressed;
        do
        {
            Console.Clear();
            DisplayHeader();
            DisplayMenuItems();
            DisplayFooter();

            var keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                    _selectedIndex = _selectedIndex == 0 ? _menuItems.Count - 1 : _selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _selectedIndex = _selectedIndex == _menuItems.Count - 1 ? 0 : _selectedIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    ExecuteSelectedItem();
                    if (_menuItems[_selectedIndex].IsExit)
                        return;
                    break;
            }
        } while (keyPressed != ConsoleKey.Escape);
    }

    private void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("═".PadRight(60, '═'));
        Console.WriteLine(_title.PadLeft((_title.Length + 60) / 2).PadRight(60));
        Console.WriteLine("═".PadRight(60, '═'));
        Console.ResetColor();
        Console.WriteLine();
    }

    private void DisplayMenuItems()
    {
        for (int i = 0; i < _menuItems.Count; i++)
        {
            if (i == _selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine($"  ► {_menuItems[i].Title}".PadRight(60));
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"    {_menuItems[i].Title}");
            }
        }
    }

    private void DisplayFooter()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("─".PadRight(60, '─'));
        Console.WriteLine("Use ↑/↓ arrows to navigate, Enter to select, ESC to exit");
        Console.ResetColor();
    }

    private void ExecuteSelectedItem()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n► Executing: {_menuItems[_selectedIndex].Title}\n");
        Console.ResetColor();

        _menuItems[_selectedIndex].Action?.Invoke();

        if (!_menuItems[_selectedIndex].IsExit)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to return to menu...");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}
