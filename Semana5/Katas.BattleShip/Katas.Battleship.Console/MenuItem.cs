namespace ContextMenu;

/// <summary>
/// Represents a single menu item with a title and action
/// </summary>
public class MenuItem
{
    public string Title { get; set; }
    public Action Action { get; set; }
    public bool IsExit { get; set; }

    public MenuItem(string title, Action action, bool isExit = false)
    {
        Title = title;
        Action = action;
        IsExit = isExit;
    }
}
