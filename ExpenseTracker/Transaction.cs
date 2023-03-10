using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Transaction
    {
        public static List<Transaction> transactions = new List<Transaction>();

        public static int autoIncrement = 0;

        // Enums
        public enum Type { Ingreso, Gasto }
        public enum Currency { DOP, USD }

        // Properties
        public int id { get; set; }
        public Type type { get; set; }
        public Account account { get; set; }
        public Categoria category { get; set; }
        public float amount { get; set; }
        public Currency currency { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        // Constructor
        public Transaction(Type type, Account account, Categoria category, float amount, Currency currency, string description, DateTime date)
        {
            autoIncrement++;
            this.id = autoIncrement;
            this.type = type;
            this.account = account;
            this.category = category;
            this.amount = amount;
            this.currency = currency;
            this.description = description;
            this.date = date;
        }

        // Dispose
        public static void Dispose()
        {
            transactions = new List<Transaction>();
            autoIncrement = 0;
        }

        // Create
        public static void Create(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        // Read
        public static List<Transaction> Read(Account account)
        {
            return transactions.FindAll(x => x.account == account).OrderByDescending(x => x.date).ToList()
                ?? throw transactionNotFound;
        }

        public static List<Transaction> Read(Account account, Type? type)
        {
            return transactions.FindAll(x => x.account == account && x.type == type).OrderByDescending(x => x.date).ToList()
                ?? throw transactionNotFound;
        }

        public static Transaction Read(int id)
        {
            return transactions.Find(x => x.id == id)
                ?? throw transactionNotFound;
        }

        // Update
        public static void Update(int id, Transaction updatedTransaction)
        {
            Transaction transaction = transactions.Find(x => x.id == id)
                ?? throw transactionNotFound;
            int index = transactions.IndexOf(transaction);
            transactions[index] = updatedTransaction;
        }

        // Delete
        public static void Delete(int id)
        {
            Transaction transaction = transactions.Find(x => x.id == id)
                ?? throw transactionNotFound;
            transactions.Remove(transaction);
        }

        // Exception
        private static readonly ArgumentException transactionNotFound = new ArgumentException("Transacción no encontrada");
    }
}
