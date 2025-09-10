# MapleLeaf Pizza Ordering System

A console-based pizza ordering application built with .NET 8. This application provides a simple, menu-driven interface for managing pizza orders.

## Features

- **Menu-driven interface**: Easy-to-use console menu system
- **Order Management**: Create new pizza orders and view existing orders
- **Pizza Catalog**: Pre-defined pizza options with pricing
- **Customer Information**: Track customer names with orders
- **Order Tracking**: Automatic order ID assignment and timestamping
- **Price Calculation**: Automatic total price calculation for orders

## Project Structure

```
MapleLeaf/
├── src/
│   └── MapleLeaf.Console/          # Main console application
│       ├── Program.cs              # Application entry point
│       ├── PizzaOrderingApp.cs     # Main application logic and UI
│       ├── Pizza.cs                # Pizza domain model
│       ├── PizzaOrder.cs           # Order domain model
│       └── OrderManager.cs         # Order management service
│   └── MapleLeaf.Tests/            # Unit tests
│       ├── PizzaTests.cs           # Tests for Pizza class
│       ├── PizzaOrderTests.cs      # Tests for PizzaOrder class
│       └── OrderManagerTests.cs     # Tests for OrderManager class
├── MapleLeaf.sln                    # Solution file
└── README.md                        # This file
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

### Building the Application

1. Clone the repository:
   ```bash
   git clone https://github.com/rob-foulkrod/MapleLeaf.git
   cd MapleLeaf
   ```

2. Restore dependencies and build:
   ```bash
   dotnet build
   ```

### Running the Application

To start the pizza ordering system:

```pwsh
dotnet run --project src/MapleLeaf.Console
```

### Running Tests

To run the unit tests:

```pwsh
dotnet test
```

## How to Use

### Main Menu

When you start the application, you'll see the main menu:

```
MapleLeaf Pizza Ordering System
==============================
1. Create New Order
2. View All Orders
3. Exit
```

### Creating a New Order

1. Select option `1` from the main menu
2. Enter the customer name when prompted
3. Choose pizzas from the available options:
   - Margherita - $12.99
   - Pepperoni - $14.99
   - Vegetarian - $13.99
4. Add multiple pizzas if desired
5. The system will calculate the total price automatically

### Viewing Orders

Select option `2` from the main menu to view all existing orders. This displays:
- Order ID and customer name
- Order date and time
- List of pizzas in the order
- Total price

## Domain Models

### Pizza
Represents a pizza item with:
- `Name`: The pizza type (e.g., "Margherita")
- `Price`: The cost of the pizza

### PizzaOrder
Represents a customer's pizza order with:
- `Id`: Unique order identifier
- `CustomerName`: Name of the customer
- `OrderDate`: When the order was created
- `Pizzas`: List of pizzas in the order
- `TotalPrice`: Calculated total cost

### OrderManager
Service class that manages pizza orders:
- `AddOrder()`: Add a new order to the system
- `GetAllOrders()`: Retrieve all orders
- `GetOrderById()`: Find a specific order by ID

## Technology Stack

- **Framework**: .NET 8
- **Language**: C#
- **Testing Framework**: xUnit
- **Project Type**: Console Application

## Future Enhancements

This is a basic project structure that could be extended with:

- Pizza customization options (size, toppings)
- Order persistence (database or file storage)
- Order status tracking (pending, preparing, ready, delivered)
- Customer management system
- Payment processing
- Delivery management
- Web-based interface
- API endpoints for order management

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.