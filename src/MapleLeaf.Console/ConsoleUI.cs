namespace MapleLeaf.App;

/// <summary>
/// Concrete console implementation of <see cref="IConsoleUI"/>.
/// </summary>
public class ConsoleUI : IConsoleUI
{
    public void WriteLine(string? value = null) => Console.WriteLine(value);
    public void Write(string? value = null) => Console.Write(value);
    public string? ReadLine() => Console.ReadLine();
    public ConsoleKeyInfo ReadKey(bool intercept = false) => Console.ReadKey(intercept);
    public void Clear() => Console.Clear();
}
