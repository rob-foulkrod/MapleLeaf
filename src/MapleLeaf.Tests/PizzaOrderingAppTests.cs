using MapleLeaf.App;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace MapleLeaf.Tests;

public class PizzaOrderingAppTests
{
    private PizzaOrderingApp CreateApp(Mock<IConsoleUI> uiMock, List<string> written, Queue<string> input)
    {
        uiMock.Setup(u => u.WriteLine(It.IsAny<string?>()))
            .Callback<string?>(s => written.Add(s ?? string.Empty));
        uiMock.Setup(u => u.Write(It.IsAny<string?>()))
            .Callback<string?>(s => written.Add(s ?? string.Empty));
        uiMock.Setup(u => u.ReadLine())
            .Returns(() => input.Count > 0 ? input.Dequeue() : string.Empty);
        uiMock.Setup(u => u.ReadKey(It.IsAny<bool>()))
            .Returns(new ConsoleKeyInfo('\0', ConsoleKey.Enter, false, false, false));

        var logger = Mock.Of<ILogger<PizzaOrderingApp>>();
        var settings = Options.Create(new AppSettings
        {
            Application = new AppSettings.ApplicationSettings { Title = "Test Title" }
        });
        return new PizzaOrderingApp(new OrderManager(), uiMock.Object, logger, settings);
    }

    [Fact]
    public async Task RunAsync_ShouldExit_WhenUserChoosesExit()
    {
        // Arrange
        var outputs = new List<string>();
        var inputs = new Queue<string>(new[] { "3" }); // Immediately choose Exit
        var uiMock = new Mock<IConsoleUI>();
        var app = CreateApp(uiMock, outputs, inputs);

        // Act
        await app.RunAsync();

        // Assert
        Assert.Contains(outputs, s => s.Contains("Thank you for using"));
    }

    [Fact]
    public async Task RunAsync_ShouldUseConfiguredTitle()
    {
        // Arrange
        var outputs = new List<string>();
        var inputs = new Queue<string>(new[] { "3" });
        var uiMock = new Mock<IConsoleUI>();
        var app = CreateApp(uiMock, outputs, inputs);

        // Act
        await app.RunAsync();

        // Assert
        Assert.Contains(outputs, s => s.Contains("Test Title"));
    }
}
