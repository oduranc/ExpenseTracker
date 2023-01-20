﻿using ExpenseTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExpenseTracker
{
    public class UnitTestAccount
    {
        [Fact]
        public void TestCreateRecordWithConstructor()
        {
            var records = new List<Account>();

            var name = "Nathalie Elias";
            var Id = records.Count() > 0 ? records.Max(r => r.Id) + 1 : 1;
            var record = new Account(name, Id);
            records.Add(record);

            var createdRecord = records.SingleOrDefault(r => r.Id == record.Id);
            Assert.NotNull(createdRecord);
            Assert.Equal(name, createdRecord.Name);
            Assert.Equal(Id, createdRecord.Id);
        }


        [Fact]
        public void TestReadRecordWithConstructor()
        {
            var records = new List<Account>();

            var name = "Nathalie Elias";
            var Id = records.Count() > 0 ? records.Max(r => r.Id) + 1 : 1;
            var record = new Account(name, Id);
            records.Add(record);

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            Console.SetOut(writer);

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

            var output = sb.ToString().Trim();
            Assert.Equal($"Id: {record.Id}{Environment.NewLine}Name: {record.Name}", output);
        }

        [Fact]
        public void TestUpdateRecordWithConstructor()
        {
            var records = new List<Account>();

            var name = "Nathalie Elias";
            var record = new Account(name, 1);
            records.Add(record);

            var newName = "Nathalie E.";
            record.Name = newName;

            var updatedRecord = records.SingleOrDefault(r => r.Id == record.Id);
            Assert.NotNull(updatedRecord);
            Assert.Equal(newName, updatedRecord.Name);
        }

        [Fact]
        public void TestDeleteRecordWithConstructor()
        {
            var records = new List<Account>();

            var name = "Nathalie Elias";
            var record = new Account(name, 1);
            records.Add(record);

            records.Remove(record);

            var deletedRecord = records.SingleOrDefault(r => r.Id == record.Id);
            Assert.Null(deletedRecord);
        }
    }
}