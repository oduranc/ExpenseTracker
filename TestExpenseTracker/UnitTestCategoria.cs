using System;
using Xunit;
using ExpenseTracker;
using System.Text;


namespace TestExpenseTracker
{
    public class UnitTestCategoria
    {
        [Fact]
        public void TestCreateCategoryWithConstructor()
        {
            // Arrange
            var sut = new List<ExpenseTracker.CategoriaCRUD>();
            var name = "Ahorros";
            var Id = sut.Count() > 0 ? sut.Max(r => r.Id) + 1 : 1;
            var Category = new ExpenseTracker.CategoriaCRUD(name, Id);

            // Act
            sut.Add(Category);
            var createdCategory = sut.SingleOrDefault(r => r.Id == Category.Id);

            // Assert
            Assert.NotNull(createdCategory);
            Assert.Equal(name, createdCategory!.Name);
            Assert.Equal(Id, createdCategory.Id);
        }


        [Fact]
        public void TestReadCategoryWithConstructor()
        {
            // Arrange
            var sut = new List<ExpenseTracker.CategoriaCRUD>();
            var name = "Ahorros";
            var Id = sut.Count() > 0 ? sut.Max(r => r.Id) + 1 : 1;
            var Category = new ExpenseTracker.CategoriaCRUD(name, Id);
            sut.Add(Category);

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            Console.SetOut(writer);

            // Act
            Console.WriteLine(sut.Count() > 0
            ? "Id: " + sut[0].Id + Environment.NewLine + "Name: " + sut[0].Name
            : "No records found.");

            // Assert
            var output = sb.ToString().Trim();
            Assert.Equal($"Id: {Category.Id}{Environment.NewLine}Name: {Category.Name}", output);
        }


        [Fact]
        public void TestUpdateRecordWithConstructor()
        {
            // Arrange
            var sut = new List<ExpenseTracker.CategoriaCRUD>();
            var name = "Ahorros";
            var Category = new ExpenseTracker.CategoriaCRUD(name, 1);
            sut.Add(Category);

            // Act
            var newName = "Gastos";
            Category.Name = newName;

            // Assert
            var updateCategory = sut.SingleOrDefault(r => r.Id == Category.Id);
            Assert.NotNull(updateCategory);
            Assert.Equal(newName, updateCategory!.Name);
        }


        [Fact]
        public void TestDeleteCategoryWithConstructor()
        {
            // Arrange
            var sut = new List<ExpenseTracker.CategoriaCRUD>();
            var name = "Ahorros";
            var Category = new ExpenseTracker.CategoriaCRUD(name, 1);
            sut.Add(Category);

            // Act
            sut.Remove(Category);

            // Assert
            var deletedCategory = sut.SingleOrDefault(r => r.Id == Category.Id);
            Assert.Null(deletedCategory);
        }

    }


}