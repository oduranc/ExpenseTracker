using System.Linq;
using Xunit;
using CRUDUser4;

public class CRUDTests
{
    [Fact]
    public void Create_ShouldAddRecordToList()
    {
        // Arrange
        var sut = new CRUD();

        // Act
        sut.Create("John");
        sut.Create("Jane");

        // Assert
        var records = sut.Read();
        Assert.Equal(2, records.Count);
        Assert.Equal(1, records[0].ID);
        Assert.Equal("John", records[0].Name);
        Assert.Equal(2, records[1].ID);
        Assert.Equal("Jane", records[1].Name);
    }

    [Fact]
    public void Read_ShouldReturnAllRecords()
    {
        // Arrange
        var sut = new CRUD();
        sut.Create("John");
        sut.Create("Jane");

        // Act
        var records = sut.Read();

        // Assert
        Assert.Equal(2, records.Count);
        Assert.Equal(1, records[0].ID);
        Assert.Equal("John", records[0].Name);
        Assert.Equal(2, records[1].ID);
        Assert.Equal("Jane", records[1].Name);
    }

    [Fact]
    public void Update_ShouldChangeNameOfRecord()
    {
        // Arrange
        var sut = new CRUD();
        sut.Create("John");
        sut.Create("Jane");

        // Act
        sut.Update(1, "John Smith");

        // Assert
        var records = sut.Read();
        Assert.Equal("John Smith", records[0].Name);
    }

    [Fact]
    public void Update_ShouldReturnNotFound()
    {
        // Arrange
        var sut = new CRUD();
        sut.Create("John");
        sut.Create("Jane");

        // Act
        sut.Update(3, "John Smith");

        // Assert
        var records = sut.Read();
        Assert.Equal(2, records.Count);
        Assert.Equal("John", records[0].Name);
        Assert.Equal("Jane", records[1].Name);
    }

    [Fact]
    public void Delete_ShouldRemoveRecordFromList()
    {
        // Arrange
        var sut = new CRUD();
        sut.Create("John");
        sut.Create("Jane");

        // Act
        sut.Delete(1);

        // Assert
        var records = sut.Read();
        Assert.Single(records);
        Assert.Equal(2, records[0].ID);
        Assert.Equal("Jane", records[0].Name);
    }
}

