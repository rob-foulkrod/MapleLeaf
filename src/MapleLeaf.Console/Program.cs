using MapleLeaf.App;

Console.WriteLine("Welcome to MapleLeaf Pizza Ordering System!");
Console.WriteLine("==========================================");

var app = new PizzaOrderingApp();
await app.RunAsync();
