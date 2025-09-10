using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MapleLeaf.App.Coupons;

namespace MapleLeaf.App;

/// <summary>
/// Root interactive application for managing pizza orders. Implements <see cref="IPizzaOrderingApp"/> so it
/// can be resolved through dependency injection. For the initial transition we retain the parameterless
/// constructor to avoid breaking external code paths while introducing a DI-friendly constructor.
/// </summary>
public class PizzaOrderingApp : IPizzaOrderingApp
{
    private readonly IOrderManager _orderManager;
    private readonly IConsoleUI _ui;
    private readonly ILogger<PizzaOrderingApp> _logger;
    private readonly AppSettings _settings;
    private readonly ICouponEngine _couponEngine;

    /// <summary>
    /// Dependency-injection constructor.
    /// </summary>
    public PizzaOrderingApp(IOrderManager orderManager, IConsoleUI ui, ILogger<PizzaOrderingApp> logger, IOptions<AppSettings> options, ICouponEngine? couponEngine = null)
    {
        _orderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));
        _ui = ui ?? throw new ArgumentNullException(nameof(ui));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settings = options?.Value ?? new AppSettings();
        _couponEngine = couponEngine ?? NullCouponEngine.Instance; // fallback to null-object implementation
    }

    public async Task RunAsync()
    {
        bool running = true;
        
        while (running)
        {
            ShowMainMenu();
            var choice = _ui.ReadLine();
            
            switch (choice)
            {
                case "1":
                    await CreateNewOrderAsync();
                    break;
                case "2":
                    ShowAllOrders();
                    break;
                case "3":
                    _ui.WriteLine("Thank you for using MapleLeaf Pizza Ordering System!");
                    running = false;
                    break;
                default:
                    _ui.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            
            if (running)
            {
                _ui.WriteLine("\nPress any key to continue...");
                _ui.ReadKey();
            }
        }
    }

    private void ShowMainMenu()
    {
    _ui.Clear();
    var title = _settings.Application.Title;
    _ui.WriteLine(title);
    _ui.WriteLine(new string('=', title.Length));
    _ui.WriteLine("1. Create New Order");
    _ui.WriteLine("2. View All Orders");
    _ui.WriteLine("3. Exit");
    _ui.WriteLine("\nPlease enter your choice (1-3): ");
    }

    private async Task CreateNewOrderAsync()
    {
    _ui.Clear();
    _ui.WriteLine("Create New Pizza Order");
    _ui.WriteLine("=====================");
        
    _ui.Write("Enter customer name: ");
    var customerName = _ui.ReadLine() ?? "";
        
        if (string.IsNullOrWhiteSpace(customerName))
        {
            _ui.WriteLine("Customer name is required!");
            return;
        }

        var order = new PizzaOrder(customerName);
        
        bool addingPizzas = true;
        while (addingPizzas)
        {
            ShowPizzaMenu();
            var choice = _ui.ReadLine();
            
            switch (choice)
            {
                case "1":
                    order.AddPizza(new Pizza("Margherita", 12.99m));
                    _ui.WriteLine("Margherita pizza added to order!");
                    break;
                case "2":
                    order.AddPizza(new Pizza("Pepperoni", 14.99m));
                    _ui.WriteLine("Pepperoni pizza added to order!");
                    break;
                case "3":
                    order.AddPizza(new Pizza("Vegetarian", 13.99m));
                    _ui.WriteLine("Vegetarian pizza added to order!");
                    break;
                case "4":
                    addingPizzas = false;
                    break;
                default:
                    _ui.WriteLine("Invalid choice. Please try again.");
                    continue;
            }
            
            if (addingPizzas)
            {
                _ui.Write("Add another pizza? (y/n): ");
                var addMore = _ui.ReadLine();
                if (addMore?.ToLower() != "y")
                {
                    addingPizzas = false;
                }
            }
        }
        
        _orderManager.AddOrder(order);

        // Evaluate coupons via engine (specification pattern)
        var evaluation = _couponEngine.Evaluate(order);
        _ui.WriteLine("\nOrder created successfully!");
        _ui.WriteLine($"Subtotal: ${evaluation.Subtotal:F2}");
        if (evaluation.TotalDiscount > 0)
        {
            foreach (var c in evaluation.AppliedCoupons)
            {
                _ui.WriteLine($"Applied {c.Code}: -${c.DiscountAmount:F2} ({c.Description})");
            }
            _ui.WriteLine($"Final Total: ${evaluation.FinalTotal:F2}");
        }
        else
        {
            _ui.WriteLine($"Total: ${evaluation.FinalTotal:F2}");
        }
    }

    private void ShowPizzaMenu()
    {
        _ui.WriteLine("\nAvailable Pizzas:");
        _ui.WriteLine("================");
        _ui.WriteLine("1. Margherita - $12.99");
        _ui.WriteLine("2. Pepperoni - $14.99");
        _ui.WriteLine("3. Vegetarian - $13.99");
        _ui.WriteLine("4. Finish order");
        _ui.Write("\nSelect pizza (1-4): ");
    }

    private void ShowAllOrders()
    {
        _ui.Clear();
        _ui.WriteLine("All Orders");
        _ui.WriteLine("==========");
        
        var orders = _orderManager.GetAllOrders();
        
        if (!orders.Any())
        {
            _ui.WriteLine("No orders found.");
            return;
        }
        
        foreach (var order in orders)
        {
            _ui.WriteLine($"Order #{order.Id} - Customer: {order.CustomerName}");
            _ui.WriteLine($"Order Date: {order.OrderDate:yyyy-MM-dd HH:mm}");
            _ui.WriteLine("Pizzas:");
            foreach (var pizza in order.Pizzas)
            {
                _ui.WriteLine($"  - {pizza.Name} (${pizza.Price:F2})");
            }
            _ui.WriteLine($"Total: ${order.TotalPrice:F2}");
            _ui.WriteLine(new string('-', 40));
        }
    }
}