using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Transaction
    {
        public static List<Transaction> transactions = new List<Transaction>();

        private static int autoIncrement = 0;

        // Enums
        public enum Type { Ingreso, Gasto }
        public enum Currency { DOP, USD }

        // Propiedades
        public int id { get; set; }
        public Type type { get; set; }
        public string account { get; set; }
        public string category { get; set; }
        public float amount { get; set; }
        public Currency currency { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        // Constructor
        public Transaction(Type type, string account, string category, float amount, Currency currency, string description, DateTime date)
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

        // Create
        public static void Create(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        // Read
        public static List<Transaction> Read()
        {
            return transactions;
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
