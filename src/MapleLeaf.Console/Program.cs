using MapleLeaf.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Banner remains outside the host so it appears immediately when the process starts.
Console.WriteLine("Welcome to MapleLeaf Pizza Ordering System!");
Console.WriteLine("==========================================");

// NOTE: We begin by wiring up a minimal Generic Host. At this stage we still register
// concrete types. Interfaces will be introduced in subsequent steps for improved
// abstraction and testability.
var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		// Service registrations (initial concrete registrations). Next steps will introduce interfaces for OrderManager.
		services.AddSingleton<OrderManager>(); // Singleton for shared state across the app lifetime.
		services.AddTransient<IPizzaOrderingApp, PizzaOrderingApp>(); // Interface-based registration for the app root.
	})
	.Build();

// Resolve the application root and execute via its interface abstraction.
var app = host.Services.GetRequiredService<IPizzaOrderingApp>();
await app.RunAsync();

// For a console app that completes once RunAsync finishes, we optionally dispose the host.
// Using await using pattern ensures graceful disposal of any async resources when added later.
await host.StopAsync();
host.Dispose();
