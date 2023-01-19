using System;
using System.Linq;
using System.Collections.Generic;

namespace CRUDUsuario { 

    public class CRUD
    {
        static void Main(string[] args)
        {
            var records = new List<Record>();

            while (true)
            {
                Console.WriteLine("\n1. Create record");
                Console.WriteLine("\n2. Read record");
                Console.WriteLine("\n3. Update record");
                Console.WriteLine("\n4. Delete record");
                Console.WriteLine("\n5. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("\nEnter name: ");
                        var Name = Console.ReadLine();

                        var Id = records.Count() > 0 ? records.Max(r => r.Id) + 1 : 1;

                        var record = new Record(Name,Id);
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
                            Console.WriteLine("No records found.");
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
            }
        }
    }

    public class Record
    {
        public Record(string? name, int? id)
        {
            Name = name;
            Id = id;
        }

        public int? Id { get; set; }
        public string Name { get; set; }
    }

  

}