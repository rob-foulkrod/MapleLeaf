using MapleLeaf.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MapleLeaf.App.Coupons;

// Banner remains outside the host so it appears immediately when the process starts.
Console.WriteLine("Welcome to MapleLeaf Pizza Ordering System!");
Console.WriteLine("==========================================");

// NOTE: We begin by wiring up a minimal Generic Host. At this stage we still register
// concrete types. Interfaces will be introduced in subsequent steps for improved
// abstraction and testability.
var host = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration((ctx, config) =>
	{
		// Additional configuration sources can be added here (env-specific json, etc.)
		config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
	})
	.ConfigureServices((ctx, services) =>
	{
		services.Configure<AppSettings>(ctx.Configuration); // Bind root configuration to AppSettings structure.

		// Interface-based service registrations.
		services.AddSingleton<IOrderManager, OrderManager>(); // Shared order state.
		services.AddSingleton<IConsoleUI, ConsoleUI>(); // Console abstraction.
		services.AddTransient<IPizzaOrderingApp, PizzaOrderingApp>(); // App root.
		services.AddCouponEngine(); // Coupon engine (specification + aggregation pattern)
	})
	.ConfigureLogging(logging => { /* default providers already added */ })
	.Build();

// Resolve the application root and execute via its interface abstraction.
var app = host.Services.GetRequiredService<IPizzaOrderingApp>();
await app.RunAsync();

// For a console app that completes once RunAsync finishes, we optionally dispose the host.
// Using await using pattern ensures graceful disposal of any async resources when added later.
await host.StopAsync();
host.Dispose();
