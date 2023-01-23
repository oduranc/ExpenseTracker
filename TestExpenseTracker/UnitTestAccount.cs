using ExpenseTracker;
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
            // Arrange
            var sut = new List<Account>();
            var name = "Nathalie Elias";
            var Id = sut.Count() > 0 ? sut.Max(r => r.Id) + 1 : 1;
            var record = new Account(name, Id);

            // Act
            sut.Add(record);
            var createdRecord = sut.SingleOrDefault(r => r.Id == record.Id);

            // Assert
            Assert.NotNull(createdRecord);
            Assert.Equal(name, createdRecord.Name);
            Assert.Equal(Id, createdRecord.Id);
        }


        [Fact]
        public void TestReadRecordWithConstructor()
        {
            // Arrange
            var sut = new List<Account>();
            var name = "Nathalie Elias";
            var Id = sut.Count() > 0 ? sut.Max(r => r.Id) + 1 : 1;
            var record = new Account(name, Id);
            sut.Add(record);

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            Console.SetOut(writer);

            // Act
            Console.WriteLine(sut.Count() > 0
            ? "Id: " + sut[0].Id + Environment.NewLine + "Name: " + sut[0].Name
            : "No records found.");

            // Assert
            var output = sb.ToString().Trim();
            Assert.Equal($"Id: {record.Id}{Environment.NewLine}Name: {record.Name}", output);
        }

        [Fact]
        public void TestUpdateRecordWithConstructor()
        {
            // Arrange
            var records = new List<Account>();
            var name = "Nathalie Elias";
            var record = new Account(name, 1);
            records.Add(record);

            // Act
            var newName = "Nathalie E.";
            record.Name = newName;

            // Assert
            var updatedRecord = records.SingleOrDefault(r => r.Id == record.Id);
            Assert.NotNull(updatedRecord);
            Assert.Equal(newName, updatedRecord.Name);
        }

        [Fact]
        public void TestDeleteRecordWithConstructor()
        {
            // Arrange
            var records = new List<Account>();
            var name = "Nathalie Elias";
            var record = new Account(name, 1);
            records.Add(record);

            // Act
            records.Remove(record);

            // Assert
            var deletedRecord = records.SingleOrDefault(r => r.Id == record.Id);
            Assert.Null(deletedRecord);
        }
    }
}
