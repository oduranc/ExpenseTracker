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
            var category = new List<ExpenseTracker.CategoriaCRUD>();

            var name = "Ahorros";
            var Id = category.Count() > 0 ? category.Max(r => r.Id) + 1 : 1;
            var Category = new ExpenseTracker.CategoriaCRUD(name, Id);
            category.Add(Category);

            var createdCategory = category.SingleOrDefault(r => r.Id == Category.Id);
            Assert.NotNull(createdCategory);
            Assert.Equal(name, createdCategory!.Name);
            Assert.Equal(Id, createdCategory.Id);
        }


        [Fact]
        public void TestReadCategoryWithConstructor()
        {
            var category = new List<ExpenseTracker.CategoriaCRUD>();

            var name = "Ahorros";
            var Id = category.Count() > 0 ? category.Max(r => r.Id) + 1 : 1;
            var Category = new ExpenseTracker.CategoriaCRUD(name, Id);
            category.Add(Category);

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            Console.SetOut(writer);

            if (category.Count() > 0)
            {
                foreach (var category01 in category)
                {
                    Console.WriteLine("Id: " + category01.Id);
                    Console.WriteLine("Name: " + category01.Name);
                }
            }
            else
            {
                Console.WriteLine("No records found.");
            }

            var output = sb.ToString().Trim();
            Assert.Equal($"Id: {Category.Id}{Environment.NewLine}Name: {Category.Name}", output);
        }

        [Fact]
        public void TestUpdateRecordWithConstructor()
        {
            var category = new List<ExpenseTracker.CategoriaCRUD>();

            var name = "Ahorros";
            var Category = new ExpenseTracker.CategoriaCRUD(name, 1);
            category.Add(Category);

            var newName = "Gastos";
            Category.Name = newName;

            var updateCategory = category.SingleOrDefault(r => r.Id == Category.Id);
            Assert.NotNull(updateCategory);
            Assert.Equal(newName, updateCategory!.Name);
        }

        [Fact]
        public void TestDeleteCategoryWithConstructor()
        {
            var category = new List<ExpenseTracker.CategoriaCRUD>();

            var name = "Ahorros";
            var Category = new ExpenseTracker.CategoriaCRUD(name, 1);
            category.Add(Category);

            category.Remove(Category);

            var deletedCategory = category.SingleOrDefault(r => r.Id == Category.Id);
            Assert.Null(deletedCategory);
        }



    }


}