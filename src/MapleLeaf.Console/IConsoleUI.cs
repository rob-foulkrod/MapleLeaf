namespace MapleLeaf.App;

/// <summary>
/// Abstraction over console I/O to enable unit testing of interactive flows.
/// </summary>
public interface IConsoleUI
{
    void WriteLine(string? value = null);
    void Write(string? value = null);
    string? ReadLine();
    ConsoleKeyInfo ReadKey(bool intercept = false);
    void Clear();
}
