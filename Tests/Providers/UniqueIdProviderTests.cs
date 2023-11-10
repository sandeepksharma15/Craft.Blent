using Craft.Blent.Providers;
using FluentAssertions;

namespace Craft.Blent.Tests.Providers;

public class UniqueIdProviderTests
{
    [Fact]
    public void Generate_ShouldReturnUniqueId()
    {
        // Arrange
        var idProvider = new UniqueIdProvider();

        // Act
        var generatedId = idProvider.Generate();

        // Assert
        generatedId.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Generate_ShouldReturnUniqueIds()
    {
        // Arrange
        var idProvider = new UniqueIdProvider();

        // Act
        var id1 = idProvider.Generate();
        var id2 = idProvider.Generate();

        // Assert
        id1.Should().NotBeEquivalentTo(id2);
    }

    [Fact]
    public void Generate_ShouldHaveCorrectLength()
    {
        // Arrange
        var idProvider = new UniqueIdProvider();

        // Act
        var generatedId = idProvider.Generate();

        // Assert
        generatedId.Length.Should().Be(13);
    }

    [Fact]
    public void Generate_ShouldHaveValidCharacters()
    {
        // Arrange
        var idProvider = new UniqueIdProvider();
        const string source = "0123456789ABCDEFGHIJKLMNOPQRSTUV";

        // Act
        var generatedId = idProvider.Generate();
        var result = generatedId.ToCharArray().All(c => source.Contains(c));

        // Assert
        result.Should().BeTrue();
    }
}
