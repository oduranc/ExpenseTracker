using AngleSharp.Dom;
using ExpenseTracker;
using System;
using System.Xml.Linq;
using static ExpenseTracker.Transaction;


// Programa
InitialData();
bool exit = false;
while (!exit)
{
    Console.Clear();

    Console.WriteLine("============================================================================");
    Console.WriteLine("====================== Welcome to our Expense Tracker ======================");
    Console.WriteLine("============================================================================\n");

    Console.WriteLine("What do you want to do?");
    Console.WriteLine("1. Select account");
    Console.WriteLine("2. Go to Accounts.");
    Console.WriteLine("3. Go to Categories.");
    Console.WriteLine("4. Exit.");

    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("\nChoose one:\n");
            foreach (var account in Account.records)
            {
                Console.WriteLine($"{account.Id}. {account.Name}");
            }
            UserActions(int.Parse(Console.ReadLine()));
            break;
        case "2":
            Account.CRUD();
            break;
        case "3":
            Categoria.CRUD();
            break;
        case "4":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

// Métodos
static void InitialData()
{
    Account oscarAcc = new("Oscar Durán", 1);
    Account ameliaAcc = new("Amelia Cepada", 2);
    Account.records.Add(oscarAcc);
    Account.records.Add(ameliaAcc);

    Categoria ahorro = new("Ahorros", 1);
    Categoria ropa = new("Ropa", 2);
    Categoria comida = new("Comida", 3);
    Categoria.category.Add(ahorro);
    Categoria.category.Add(ropa);
    Categoria.category.Add(comida);

    Transaction.Create(new Transaction(Transaction.Type.Ingreso, oscarAcc, ahorro, 25000, Transaction.Currency.DOP, "Me depositaron", DateTime.Today.AddDays(-1)));
    Transaction.Create(new Transaction(Transaction.Type.Gasto, oscarAcc, comida, 590, Transaction.Currency.DOP, "Me compré un baconator", DateTime.Now));
    Transaction.Create(new Transaction(Transaction.Type.Ingreso, ameliaAcc, ahorro, 100, Transaction.Currency.USD, "Me gané 100 USD", DateTime.Today.AddDays(-1)));
    Transaction.Create(new Transaction(Transaction.Type.Gasto, ameliaAcc, ropa, 1000, Transaction.Currency.DOP, "Jeans", DateTime.Now));

    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.White;
}
static void GetTransactions(Account account, Transaction.Type? type)
{
    var transactions = type == null ? Transaction.Read(account) : Transaction.Read(account, type);
   
    Console.WriteLine("");
    float total = GetTotal(account, type);
    ClearLine();
    foreach (var transaction in transactions)
    {
        Console.ForegroundColor = transaction.type == Transaction.Type.Ingreso ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"{transaction.type} - {transaction.category.Name} - {transaction.amount} {transaction.currency} - {transaction.description} - {transaction.date.ToShortDateString()}");
    }
    Console.WriteLine("");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("Total : ");
    Console.ForegroundColor = total < 0 ? ConsoleColor.Red : ConsoleColor.Green;
    Console.Write($"{total} {Transaction.Currency.DOP}\n");
    Console.ForegroundColor = ConsoleColor.White;
}
static void CreateTransaction(Account account)
{
    var type = GetEnumSelection<Transaction.Type>("Enter the number of the transaction type:");
    var categoria = GetCategorySelection();
    var currency = GetEnumSelection<Transaction.Currency>("Enter the number of the transaction currency:");
    Console.Write("Enter the amount of the transaction: ");
    float amount = float.Parse(Console.ReadLine());
    Console.Write("Enter a description for the transaction: ");
    string description = Console.ReadLine();
    Console.Write("Enter the date of the transaction (dd/mm/aaaa): ");
    DateTime date = DateTime.Parse(Console.ReadLine());

    Transaction transaction = new Transaction(type, account, categoria, amount, currency, description, date);
    Transaction.Create(transaction);
}
static T GetEnumSelection<T>(string message) where T : Enum
{
    Console.WriteLine(message);
    var values = Enum.GetValues(typeof(T));
    for (int i = 0; i < values.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {values.GetValue(i)}");
    }

    string input = Console.ReadLine();
    int selection = int.Parse(input);
    return (T)Enum.ToObject(typeof(T), selection - 1);
}
static T GetEnumOrPrevious<T>(string message, T previous) where T : Enum
{
    Console.WriteLine(message);
    var values = Enum.GetValues(typeof(T));
    for (int i = 0; i < values.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {values.GetValue(i)}");
    }

    string input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        return previous;
    }

    int selection = int.Parse(input);
    return (T)Enum.ToObject(typeof(T), selection - 1);
}
static Categoria GetCategorySelection()
{
    Console.WriteLine("Enter the number of the transaction category:");
    foreach (var category in Categoria.category)
    {
        Console.WriteLine($"{category.Id}. {category.Name}");
    }

    string input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        return null;
    }

    int categoryIndex = int.Parse(input);
    return Categoria.category.Find(x => x.Id == categoryIndex);
}
static void UpdateTransaction(Account account)
{
    Console.WriteLine("Enter the number of the transaction you want to edit:");
    foreach (var item in Transaction.Read(account))
    {
        Console.WriteLine($"{item.id}. {item.type} - {item.category.Name} - {item.description} - {item.amount} {item.currency} - {item.date.ToShortDateString()}");
    }
    int transactionId = int.Parse(Console.ReadLine());
    Transaction transaction = Transaction.transactions.Find(x => x.id == transactionId);
    if (transaction == null)
    {
        Console.WriteLine("Invalid transaction id");
        return;
    }

    Console.WriteLine("Enter the new value for each attribute (if you want to keep the previous one just press Enter):\n");

    Console.WriteLine($"Type: {transaction.type}");

    var type = GetEnumOrPrevious<Transaction.Type>("Enter the number of the transaction type:", transaction.type);

    Console.WriteLine($"Category: {transaction.category.Name}");
    var categoria = GetCategorySelection() ?? transaction.category;

    Console.WriteLine($"Amount: {transaction.amount}");
    Console.WriteLine("Enter the new amount:");
    float amount = float.TryParse(Console.ReadLine(), out float parsedAmount) ? parsedAmount : transaction.amount;

    Console.WriteLine($"Currency: {transaction.currency}");
    var currency = GetEnumOrPrevious<Transaction.Currency>("Enter the number of the transaction currency:", transaction.currency);

    Console.WriteLine($"Description: {transaction.description}");
    Console.WriteLine("Enter the new description:");
    string description = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(description))
    {
        description = transaction.description;
    }

    Console.WriteLine($"Date: {transaction.date.ToShortDateString()}");
    Console.WriteLine("Enter the new date:");
    DateTime date;
    if (!DateTime.TryParse(Console.ReadLine(), out date))
    {
        date = transaction.date;
    }

    Transaction updatedTransaction = new Transaction(
        type != null ? type : transaction.type,
        account,
        categoria != null ? categoria : transaction.category,
        amount != 0 ? amount : transaction.amount,
        currency != null ? currency : transaction.currency,
        description,
        date
    );

    Transaction.Update(transactionId, updatedTransaction);
}
static void DeleteTransaction(Account account)
{
    Console.WriteLine("Enter the number of the transaction you want to edit:");
    foreach (var item in Transaction.Read(account))
    {
        Console.WriteLine($"{item.id}. {item.type} - {item.category.Name} - {item.description} - {item.amount} {item.currency} - {item.date.ToShortDateString()}");
    }
    int transactionId = int.Parse(Console.ReadLine());
    Transaction transaction = Transaction.transactions.Find(x => x.id == transactionId);
    if (transaction == null)
    {
        Console.WriteLine("Invalid transaction id");
        return;
    }
    Transaction.Delete(transactionId);
}
static void UserActions(int accountId)
{
    Account account = Account.records.Find(x => x.Id == accountId);

    while (true)
    {
        Console.Clear();
        Console.WriteLine("What do you want to do?");
        Console.WriteLine("1. See all transactions");
        Console.WriteLine("2. See just incomes.");
        Console.WriteLine("3. See just outcomes.");
        Console.WriteLine("4. Create transaction.");
        Console.WriteLine("5. Edit one transaction.");
        Console.WriteLine("6. Delete one transaction.");
        Console.WriteLine("7. Exit.");

        string choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                GetTransactions(account, null);
                break;
            case "2":
                GetTransactions(account, Transaction.Type.Ingreso);
                break;
            case "3":
                GetTransactions(account, Transaction.Type.Gasto);
                break;
            case "4":
                CreateTransaction(account);
                break;
            case "5":
                UpdateTransaction(account);
                break;
            case "6":
                DeleteTransaction(account);
                break;
            case "7":
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
static float GetTotal(Account account, Transaction.Type? type)
{
    var transactions = type == null ? Transaction.Read(account) : Transaction.Read(account, type);
    float dolar = 0;
    float pesos = 0;
    float total = 0;
    foreach (var transaction in transactions)
    {
        float value = 0;
        switch (transaction.currency)
        {
            case Transaction.Currency.DOP:
                pesos += transaction.amount;
                break;
            case Transaction.Currency.USD:
                dolar += transaction.amount;
                break;
                //Add more cases for other currencies
        }

        float nuevaTasa = Convertidor("Banco Popular", dolar).GetAwaiter().GetResult();
            value = pesos + dolar;

        total = transaction.type == Transaction.Type.Ingreso
            ? total + value
            : total - value;
    }

    return total;
}
static void ClearLine()
{
    Console.SetCursorPosition(0, Console.CursorTop - 1);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, Console.CursorTop - 1);

}
static async Task<float> Convertidor(string banco, float monto)
{
    IBuscadorTasas buscadorTasas = new BuscadorTasas(); // En el caso de prueba este será el stub
    ConvertidorDeMoneda convertidor = new ConvertidorDeMoneda(buscadorTasas); // <-- Dependency Injection

    if (monto == 0)
    {
        return 0;
    }

    string MonedaDominicana = "DOP";

    var cantidad = convertidor.ConvertirMoneda(monto, banco, MonedaDominicana);
    Console.WriteLine("Cargando....");
    float convertirMoneda = await cantidad;
    ClearLine();
    return convertirMoneda;
}