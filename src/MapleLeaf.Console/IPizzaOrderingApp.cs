namespace MapleLeaf.App;

/// <summary>
/// Contract for the pizza ordering application root. Abstracted to enable
/// dependency injection, testability, and future alternate entry points (e.g. GUI or Web host).
/// </summary>
public interface IPizzaOrderingApp
{
    /// <summary>
    /// Runs the interactive pizza ordering workflow until the user exits.
    /// </summary>
    Task RunAsync();
}
