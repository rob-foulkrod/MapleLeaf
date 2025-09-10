# MapleLeaf Pizza Ordering System

A console-based pizza ordering application built with .NET 8. This application provides a simple, menu-driven interface for managing pizza orders.

## Features

- **Menu-driven interface**: Easy-to-use console menu system
- **Order Management**: Create new pizza orders and view existing orders
- **Pizza Catalog**: Pre-defined pizza options with pricing
- **Customer Information**: Track customer names with orders
- **Order Tracking**: Automatic order ID assignment and timestamping
- **Price Calculation**: Automatic total price calculation for orders

## Quick Start

```powershell
git clone https://github.com/rob-foulkrod/MapleLeaf.git
cd MapleLeaf/src
dotnet build MapleLeaf.sln
dotnet run --project MapleLeaf.Console
```

Run tests:

```powershell
cd MapleLeaf/src
dotnet test MapleLeaf.sln
```

## Project Structure

```
MapleLeaf/
├── README.md
├── LICENSE
└── src/
   ├── MapleLeaf.sln                 # Solution file (kept under src)
   ├── MapleLeaf.Console/            # Main console application
   │   ├── Program.cs                # Application entry point
   │   ├── PizzaOrderingApp.cs       # Main application logic and UI
   │   ├── Pizza.cs                  # Pizza domain model
   │   ├── PizzaOrder.cs             # Order domain model
   │   └── OrderManager.cs           # Order management service
   └── MapleLeaf.Tests/              # Unit tests
      ├── OrderManagerTests.cs      # Tests for OrderManager class
      ├── PizzaOrderTests.cs        # Tests for PizzaOrder class
      └── UnitTest1.cs              # Placeholder / sample test
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

### Building the Application

1. Clone the repository:
   ```powershell
   git clone https://github.com/rob-foulkrod/MapleLeaf.git
   cd MapleLeaf
   ```

2. Restore dependencies and build (run from the repository root):
```powershell
cd src
dotnet restore
dotnet build MapleLeaf.sln
```

### Running the Application

To start the pizza ordering system:

```powershell
# From the repository root
cd src
dotnet run --project MapleLeaf.Console
```

### Running Tests

To run the unit tests:

```powershell
# From the repository root
cd src
dotnet test MapleLeaf.sln

# Faster when you've just built:
dotnet test MapleLeaf.sln --no-build

# Run only OrderManager tests:
dotnet test MapleLeaf.sln --filter FullyQualifiedName~OrderManagerTests
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

## Code Coverage

The test project already references `coverlet.collector`, so you can collect coverage using the built-in DataCollector.

Generate coverage (Cobertura XML) into a custom folder:

```powershell
cd src
dotnet test MapleLeaf.sln --collect:"XPlat Code Coverage" --results-directory ./TestResultsCoverage
```

Faster if you just built:

```powershell
dotnet test MapleLeaf.sln --collect:"XPlat Code Coverage" --results-directory ./TestResultsCoverage --no-build
```

The Cobertura file will be located under a generated test run directory, for example:

```
src/TestResultsCoverage/<run-id>/coverage.cobertura.xml
```

You can convert or view it with tools such as ReportGenerator:

```powershell
dotnet tool install -g dotnet-reportgenerator-globaltool   # once
reportgenerator -reports:src/TestResultsCoverage/**/coverage.cobertura.xml -targetdir:coverage-report
```

Then open `coverage-report/index.htm` in a browser.

### One-Step Script

This repo includes a helper script that runs tests with coverage and generates the HTML report:

```powershell
# From repo root (first time only installs tools if not present)
dotnet tool restore
pwsh ./scripts/generate-coverage.ps1

# Skip the build step if you just built:
pwsh ./scripts/generate-coverage.ps1 -NoBuild
```

Outputs:
- Cobertura XML under `src/TestResultsCoverage/<run-id>/coverage.cobertura.xml`
- HTML report under `coverage-report/index.htm`

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a new Pull Request

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.