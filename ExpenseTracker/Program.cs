using AngleSharp.Dom;
using ExpenseTracker;




IBuscadorTasas buscadorTasas = new BuscadorTasas(); // En el caso de prueba este será el stub
ConvertidorDeMoneda convertidor = new ConvertidorDeMoneda(buscadorTasas); // <-- Dependency Injection

// Esto no es necesario, es sólo para mostrar la lista de tasas:

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
int res = Convert.ToInt16(Console.ReadLine());

Console.WriteLine($"\nToma tus: ${monto}");
var dolares =  convertidor.ComprarDolaresEnElPopular(monto, banco,res);
Console.Write($" Conversion de {monto}----> ");
var d = await dolares;
Console.Write($"{d}");


Console.ReadKey();
        