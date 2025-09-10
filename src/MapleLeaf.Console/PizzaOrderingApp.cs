namespace MapleLeaf.App;

public class PizzaOrderingApp
{
    private readonly OrderManager _orderManager;

    public PizzaOrderingApp()
    {
        _orderManager = new OrderManager();
    }

    public async Task RunAsync()
    {
        bool running = true;
        
        while (running)
        {
            ShowMainMenu();
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    await CreateNewOrderAsync();
                    break;
                case "2":
                    ShowAllOrders();
                    break;
                case "3":
                    Console.WriteLine("Thank you for using MapleLeaf Pizza Ordering System!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            
            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    private void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("MapleLeaf Pizza Ordering System");
        Console.WriteLine("==============================");
        Console.WriteLine("1. Create New Order");
        Console.WriteLine("2. View All Orders");
        Console.WriteLine("3. Exit");
        Console.WriteLine("\nPlease enter your choice (1-3): ");
    }

    private async Task CreateNewOrderAsync()
    {
        Console.Clear();
        Console.WriteLine("Create New Pizza Order");
        Console.WriteLine("=====================");
        
        Console.Write("Enter customer name: ");
        var customerName = Console.ReadLine() ?? "";
        
        if (string.IsNullOrWhiteSpace(customerName))
        {
            Console.WriteLine("Customer name is required!");
            return;
        }

        var order = new PizzaOrder(customerName);
        
        bool addingPizzas = true;
        while (addingPizzas)
        {
            ShowPizzaMenu();
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    order.AddPizza(new Pizza("Margherita", 12.99m));
                    Console.WriteLine("Margherita pizza added to order!");
                    break;
                case "2":
                    order.AddPizza(new Pizza("Pepperoni", 14.99m));
                    Console.WriteLine("Pepperoni pizza added to order!");
                    break;
                case "3":
                    order.AddPizza(new Pizza("Vegetarian", 13.99m));
                    Console.WriteLine("Vegetarian pizza added to order!");
                    break;
                case "4":
                    addingPizzas = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }
            
            if (addingPizzas)
            {
                Console.Write("Add another pizza? (y/n): ");
                var addMore = Console.ReadLine();
                if (addMore?.ToLower() != "y")
                {
                    addingPizzas = false;
                }
            }
        }
        
        _orderManager.AddOrder(order);
        Console.WriteLine($"\nOrder created successfully! Total: ${order.TotalPrice:F2}");
    }

    private void ShowPizzaMenu()
    {
        Console.WriteLine("\nAvailable Pizzas:");
        Console.WriteLine("================");
        Console.WriteLine("1. Margherita - $12.99");
        Console.WriteLine("2. Pepperoni - $14.99");
        Console.WriteLine("3. Vegetarian - $13.99");
        Console.WriteLine("4. Finish order");
        Console.Write("\nSelect pizza (1-4): ");
    }

    private void ShowAllOrders()
    {
        Console.Clear();
        Console.WriteLine("All Orders");
        Console.WriteLine("==========");
        
        var orders = _orderManager.GetAllOrders();
        
        if (!orders.Any())
        {
            Console.WriteLine("No orders found.");
            return;
        }
        
        foreach (var order in orders)
        {
            Console.WriteLine($"Order #{order.Id} - Customer: {order.CustomerName}");
            Console.WriteLine($"Order Date: {order.OrderDate:yyyy-MM-dd HH:mm}");
            Console.WriteLine("Pizzas:");
            foreach (var pizza in order.Pizzas)
            {
                Console.WriteLine($"  - {pizza.Name} (${pizza.Price:F2})");
            }
            Console.WriteLine($"Total: ${order.TotalPrice:F2}");
            Console.WriteLine(new string('-', 40));
        }
    }
}