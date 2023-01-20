using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Account
    {
        public static List<Account> records = new List<Account>();
        public Account(string? name, int? id)
        {
            Name = name;
            Id = id;
        }

        public int? Id { get; set; }
        public string Name { get; set; }

        public static void CRUD()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1. Create user");
                Console.WriteLine("2. Get users");
                Console.WriteLine("3. Update user");
                Console.WriteLine("4. Delete user");
                Console.WriteLine("5. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("\nEnter name: ");
                        var Name = Console.ReadLine();

                        var Id = records.Count() > 0 ? records.Max(r => r.Id) + 1 : 1;

                        var record = new Account(Name, Id);
                        records.Add(record);

                        Console.WriteLine("\nRecord created successfully");
                        break;

                    case "2":
                        if (records.Count() > 0)
                        {
                            foreach (var record1 in records)
                            {
                                Console.WriteLine("Id: " + record1.Id);
                                Console.WriteLine("Name: " + record1.Name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNo records found.\n");
                        }
                        break;

                    case "3":
                        Console.Write("\nEnter id: ");
                        var updateId = int.Parse(Console.ReadLine());

                        var updateRecord = records.SingleOrDefault(r => r.Id == updateId);
                        if (updateRecord != null)
                        {
                            Console.Write("\nEnter new name: ");
                            var newName = Console.ReadLine();
                            updateRecord.Name = newName;

                            Console.WriteLine("\nRecord updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("\nRecord not found.");
                        }
                        break;

                    case "4":
                        Console.Write("\nEnter id: ");
                        var deleteId = int.Parse(Console.ReadLine());

                        var deleteRecord = records.SingleOrDefault(r => r.Id == deleteId);
                        if (deleteRecord != null)
                        {
                            records.Remove(deleteRecord);

                            Console.WriteLine("\nRecord deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("\nRecord not found");
                        }
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("\nInvalid choice.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

        }
    }
}
