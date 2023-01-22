using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDUser4
{

    public class CRUD
    {
        static void Main(string[] args)
        {
            var crud = new CRUD();
            crud.Create("John");
            crud.Create("Jane");

            var records = crud.Read();
            foreach (var record in records)
            {
                Console.WriteLine(record.Name);
            }

            crud.Update(1, "John Smith");
            crud.Delete(2);

            records = crud.Read();
            Console.WriteLine("After update and delete:");
            foreach (var record in records)
            {
                Console.WriteLine(record.Name);
            }

            Console.ReadKey();
        }

        public List<Record> _records = new List<Record>();

        public void Create(string name)
        {
            int id = _records.Count == 0 ? 1 : _records.Max(r => r.ID) + 1;
            _records.Add(new Record { ID = id, Name = name });
            Console.WriteLine("Record created with ID " + id);
        }

        public List<Record> Read()
        {
            Console.WriteLine("ID\tName");
            _records.ForEach(r => Console.WriteLine(r.ID + "\t" + r.Name));
            return _records;
        }

        public void Update(int id, string name)
        {
            Record record = _records.FirstOrDefault(r => r.ID == id);
            if (record != null)
            {
                record.Name = name;
                Console.WriteLine("Record with ID " + id + " updated");
            }
            else
            {
                Console.WriteLine("Record with ID " + id + " not found");
            }
        }

        public void Delete(int id)
        {
            Record record = _records.FirstOrDefault(r => r.ID == id);
            if (record != null)
            {
                _records.Remove(record);
                Console.WriteLine("Record with ID " + id + " deleted");
            }
            else
            {
                Console.WriteLine("Record with ID " + id + " not found");
            }
        }
    }

    public class Record
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }
}

