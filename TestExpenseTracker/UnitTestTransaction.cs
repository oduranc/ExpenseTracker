using ExpenseTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExpenseTracker
{
    public class UnitTestTransaction : IDisposable
    {
        Transaction SUT, secondTransaction, updatedTransaction;
        Account account;
        Categoria category;

        public UnitTestTransaction()
        {
            account = new Account("Oscar", 1);
            category = new Categoria("Prueba", 1);
            SUT = new Transaction(Transaction.Type.Gasto, account, category, 500, Transaction.Currency.DOP, "Me compré un baconator", DateTime.Now);
            secondTransaction = new Transaction(Transaction.Type.Ingreso, account, category, 25000, Transaction.Currency.DOP, "Bárbaro, me depositaron", DateTime.Now);
            updatedTransaction = new Transaction(Transaction.Type.Gasto, account, category, 590, Transaction.Currency.DOP, "Me compré un baconator", DateTime.Now);
        }

        public void Dispose()
        {
            Transaction.Dispose();
        }

        [Fact]
        public void Test_Create_Transaction()
        {
            // Arrange

            // Act
            Transaction.Create(SUT);

            // Assert
            Assert.Single(Transaction.transactions);
            Assert.Equal(SUT, Transaction.Read(SUT.id));
        }

        [Fact]
        public void Test_Get_One_Transaction()
        {
            // Arrange
            Transaction.Create(SUT);

            // Act
            var transaction = Transaction.Read(SUT.id);

            // Assert
            Assert.Equal(SUT, transaction);
        }

        [Fact]
        public void Test_Get_Transaction_Not_Found()
        {
            // Arrange
            Transaction.Create(SUT);

            // Act
            var transaction = Transaction.Read(SUT.id);

            // Assert
            Assert.Throws<ArgumentException>(() => Transaction.Read(2));
        }

        [Fact]
        public void Test_Get_All_Transactions()
        {
            // Arrange
            Transaction.Create(SUT);
            Transaction.Create(secondTransaction);

            // Act
            var transaction = Transaction.Read(account);

            // Assert
            Assert.Equal(2, Transaction.transactions.Count());
        }

        [Fact]
        public void Test_Update_Transaction()
        {
            // Arrange
            Transaction.Create(SUT);

            // Act
            Transaction.Update(SUT.id, updatedTransaction);

            // Assert
            Assert.Equal(Transaction.Type.Gasto, Transaction.transactions[0].type);
            Assert.Equal(590, Transaction.transactions[0].amount);
        }

        [Fact]
        public void Test_Update_Transaction_Not_Found()
        {
            // Arrange

            // Act
            Transaction.Create(SUT);

            // Assert
            Assert.Throws<ArgumentException>(() => Transaction.Update(2, updatedTransaction));
        }

        [Fact]
        public void Test_Delete_Transaction()
        {
            // Arrange
            Transaction.Create(SUT);
            Transaction.Create(secondTransaction);

            // Act
            Transaction.Delete(SUT.id);

            // Assert
            Assert.DoesNotContain<Transaction>(SUT, Transaction.transactions);
        }

        [Fact]
        public void Test_Delete_Transaction_Not_Found()
        {
            // Arrange

            // Act
            Transaction.Create(SUT);
            Transaction.Create(secondTransaction);

            // Assert
            Assert.Throws<ArgumentException>(() => Transaction.Delete(3));
        }
    }
}