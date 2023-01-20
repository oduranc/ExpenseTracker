using AngleSharp.Dom;
using ExpenseTracker;




IBuscadorTasas buscadorTasas = new BuscadorTasas(); // En el caso de prueba este será el stub
ConvertidorDeMoneda convertidor = new ConvertidorDeMoneda(buscadorTasas); // <-- Dependency Injection

Console.WriteLine("Bienvenidos");
Console.Write("Banco: ");
string banco = Console.ReadLine();

Console.Write("\nMonto:  ");
float monto;

try { monto = Convert.ToSingle(Console.ReadLine()); }
catch (FormatException e) { monto = 0.0f; }

if (monto == 0)
{
    Console.WriteLine("Oye ahora...");
    Console.ReadLine();
    return;
}



Console.WriteLine("Conversion");
Console.WriteLine("1. USD");
Console.WriteLine("2. DOP");

int choice = Convert.ToInt16(Console.ReadLine());
string MonedaDominicana = "DOP";
string MonedaEstadounidense = "USD";
string MonedaConvertir = (choice == 1) ? MonedaEstadounidense : MonedaDominicana;



Console.WriteLine($"\nToma tus: ${monto}");
var cantidad =  convertidor.ConvertirMoneda(monto, banco,MonedaConvertir);
Console.Write($" Conversion de {monto}----> ");
var convertirMoneda = await cantidad;
Console.Write("");
Console.Write($"{convertirMoneda}");


Console.ReadKey();
        