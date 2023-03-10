using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Categoria
    {
        public static List<Categoria> category = new List<Categoria>();

        public Categoria(string? name, int? id)
        {
            Name = name;
            Id = id;
        }

        public string? Name { get; set; }
        public int? Id { get; set; }

        public static void CRUD()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1. Create category");
                Console.WriteLine("2. Read category");
                Console.WriteLine("3. Update category");
                Console.WriteLine("4. Delete category");
                Console.WriteLine("5. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("\nEnter name: ");
                        var Name = Console.ReadLine();

                        var Id = category.Count() > 0 ? category.Max(r => r.Id) + 1 : 1;

                        var Category = new Categoria(Name, Id);
                        category.Add(Category);


                        Console.WriteLine("\nCategory created successfully");
                        break;

                    case "2":
                        if (category.Count() > 0)
                        {
                            foreach (var category01 in category)
                            {
                                Console.WriteLine("\nId: " + category01.Id);
                                Console.WriteLine("Name: " + category01.Name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found.");
                        }
                        break;

                    case "3":
                        Console.Write("\nEnter id: ");
                        var updateId = int.Parse(Console.ReadLine()!);

                        var updateCategory = category.SingleOrDefault(r => r.Id == updateId)!;
                        if (updateCategory != null)
                        {
                            Console.Write("\nEnter new name: ");
                            var newName = Console.ReadLine();
                            updateCategory.Name = newName;

                            Console.WriteLine("\nCategory updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("\nCategory not found.");
                        }
                        break;

                    case "4":
                        Console.Write("\nEnter id: ");
                        var deleteId = int.Parse(Console.ReadLine()!);

                        var deleteRecord = category.SingleOrDefault(r => r.Id == deleteId);
                        if (deleteRecord != null)
                        {
                            category.Remove(deleteRecord);

                            Console.WriteLine("\nCategory deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("\nCategory not found");
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
        