# MapleLeaf Pizza Ordering System

MapleLeaf is a .NET 8 console application that provides a menu-driven pizza ordering system. The application manages pizza orders, customer information, and provides a simple text-based user interface.

**ALWAYS follow these instructions first. Only fallback to additional search and context gathering if the information here is incomplete or found to be in error.**

## Working Effectively

### Prerequisites and Setup
- Ensure .NET 8 SDK is installed
- PowerShell (pwsh) is required for coverage scripts
- All commands should be run from the repository root unless specified

### Bootstrap and Build Process
Run these commands in order for a fresh setup:

```bash
# Navigate to source directory  
cd src

# Restore dependencies - takes ~15 seconds on first run
dotnet restore

# Restore dotnet tools for coverage generation
cd ..
dotnet tool restore

# Build solution - takes ~7 seconds, NEVER CANCEL, set timeout to 60+ seconds
cd src  
dotnet build MapleLeaf.sln
```

**NEVER CANCEL BUILDS**: The build takes approximately 7 seconds but can take longer on slower systems. Always set timeouts to 60+ seconds minimum.

### Running Tests
```bash
# From src directory
# Full test run - takes ~5 seconds, NEVER CANCEL, set timeout to 30+ seconds  
dotnet test MapleLeaf.sln

# Faster when you just built (recommended for iterative development)
dotnet test MapleLeaf.sln --no-build

# Run specific test class
dotnet test MapleLeaf.sln --filter FullyQualifiedName~OrderManagerTests --no-build

# Generate code coverage - takes ~8 seconds, NEVER CANCEL, set timeout to 60+ seconds
dotnet test MapleLeaf.sln --collect:"XPlat Code Coverage" --results-directory ./TestResultsCoverage --no-build
```

**NEVER CANCEL TEST RUNS**: Test execution takes 4-5 seconds normally, but set timeouts to 30+ seconds minimum to account for system variations.

### Running the Application
```bash
# From src directory
dotnet run --project MapleLeaf.Console
```

The application starts immediately after build completion.

### Code Coverage Generation
```bash
# From repository root - takes ~8 seconds, NEVER CANCEL, set timeout to 60+ seconds
pwsh ./scripts/generate-coverage.ps1

# Skip build if you just built  
pwsh ./scripts/generate-coverage.ps1 -NoBuild
```

Coverage report will be generated at `coverage-report/index.htm`.

## Validation Requirements

### CRITICAL: Manual Validation After Changes
After making any code changes, **ALWAYS** run through this complete validation scenario:

1. **Build and Test Validation**:
   ```bash
   cd src
   dotnet build MapleLeaf.sln
   dotnet test MapleLeaf.sln --no-build
   ```

2. **End-to-End Application Testing**:
   ```bash
   dotnet run --project MapleLeaf.Console
   ```
   
   Then test this complete workflow:
   - Select option `1` (Create New Order)
   - Enter customer name: `Test Customer`
   - Select option `1` (Margherita pizza)
   - Choose `n` (no more pizzas)
   - Verify order creation message shows correct total ($12.99)
   - Press any key to continue
   - Select option `2` (View All Orders) 
   - Verify the order appears with correct customer name, date, pizza, and total
   - Press any key to continue
   - Select option `3` (Exit)
   - Verify clean exit message

3. **Coverage Validation**:
   ```bash
   # From repository root
   pwsh ./scripts/generate-coverage.ps1 -NoBuild
   ```

**Do not skip validation steps**. Simply starting and stopping the application is NOT sufficient - you must test the complete user workflow.

## Project Structure and Navigation

### Key Directories
```
MapleLeaf/
├── .github/
│   ├── instructions/          # C# and SQL coding guidelines
│   └── copilot-instructions.md # This file
├── .config/
│   └── dotnet-tools.json      # Tool configuration (reportgenerator)
├── scripts/
│   └── generate-coverage.ps1  # Coverage generation script
└── src/                       # Main source code
    ├── MapleLeaf.sln          # Solution file
    ├── MapleLeaf.Console/     # Main console application
    │   ├── Program.cs         # Application entry point  
    │   ├── PizzaOrderingApp.cs # Main UI and application logic
    │   ├── OrderManager.cs    # Order management service
    │   ├── Pizza.cs           # Pizza domain model
    │   └── PizzaOrder.cs      # Order domain model
    └── MapleLeaf.Tests/       # Unit tests
        ├── OrderManagerTests.cs # OrderManager service tests
        ├── PizzaOrderTests.cs  # PizzaOrder model tests  
        └── UnitTest1.cs       # Sample/placeholder test
```

### Key Components
- **Program.cs**: Simple entry point that creates and runs PizzaOrderingApp
- **PizzaOrderingApp.cs**: Contains the main menu loop and UI logic
- **OrderManager.cs**: Service for managing pizza orders (add, retrieve, etc.)
- **Pizza.cs**: Domain model representing a pizza with name and price
- **PizzaOrder.cs**: Domain model representing a customer order with pizzas

### Code Patterns
- Uses file-scoped namespaces (`namespace MapleLeaf.App;`)
- Follows C# 13 conventions with nullable reference types enabled
- Simple console I/O with menu-driven interface
- In-memory storage (no database or persistence layer)

## Coding Guidelines

### Reference Existing Instructions
**ALWAYS** consult these files before making code changes:
- `.github/instructions/csharp.instructions.md` - Comprehensive C# coding standards
- `.github/instructions/sql.instructions.md` - SQL guidelines (if adding database features)

### Common Tasks and Time Expectations

| Command | Expected Time | Timeout Setting | Notes |
|---------|---------------|-----------------|-------|
| `dotnet restore` | 15 seconds (first run) | 60+ seconds | Much faster on subsequent runs |
| `dotnet build` | 7 seconds | 60+ seconds | Produces 3 harmless nullable warnings |
| `dotnet test` | 5 seconds | 30+ seconds | All 18 tests should pass |
| `dotnet test --no-build` | 2 seconds | 30+ seconds | Use after building |
| Coverage generation | 8 seconds | 60+ seconds | Via PowerShell script |
| Application startup | Immediate | N/A | Starts right after build |

### Build Notes
- Build succeeds with 3 nullable reference warnings (CS1998, CS8625) - these are harmless
- No CI/CD workflows are configured  
- Solution file is located in `src/` directory, not repository root
- Tests use xUnit framework with coverlet for coverage collection

### Development Workflow
1. Make code changes
2. Build: `cd src && dotnet build MapleLeaf.sln` 
3. Test: `dotnet test MapleLeaf.sln --no-build`
4. Run manual validation (see Validation Requirements above)
5. Generate coverage if needed: `cd .. && pwsh ./scripts/generate-coverage.ps1 -NoBuild`

### Warning: Timeout Settings
- **NEVER CANCEL** long-running operations
- Always set explicit timeouts with generous buffers:
  - Build commands: 60+ seconds minimum  
  - Test commands: 30+ seconds minimum
  - Coverage generation: 60+ seconds minimum
- Commands may take longer on different systems - these are minimum safe timeouts

## Common Outputs Reference

### Successful Build Output
```
MSBuild version 17.8.32+74df0b3f5 for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  MapleLeaf.Console -> /path/to/MapleLeaf.Console/bin/Debug/net8.0/MapleLeaf.Console.dll
  MapleLeaf.Tests -> /path/to/MapleLeaf.Tests/bin/Debug/net8.0/MapleLeaf.Tests.dll

Build succeeded.
    3 Warning(s)
    0 Error(s)
```

### Successful Test Output
```
Passed!  - Failed:     0, Passed:    18, Skipped:     0, Total:    18, Duration: 42 ms
```

### Application Menu
```
Welcome to MapleLeaf Pizza Ordering System!
==========================================
MapleLeaf Pizza Ordering System
==============================
1. Create New Order
2. View All Orders  
3. Exit

Please enter your choice (1-3):
```

Use these reference outputs to verify commands are working correctly rather than running exploratory bash commands.