using Craft.Blent.Base;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Bunit;
using Craft.Blent.Contracts.Providers;

namespace Craft.Blent.Tests.Base;

public class BaseBlentComponentTests : TestContext
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IUniqueIdProvider> _idGeneratorMock;

    public BaseBlentComponentTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _loggerMock = new Mock<ILogger>();
        _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(_loggerMock.Object);

        _idGeneratorMock = new Mock<IUniqueIdProvider>();
        _idGeneratorMock.Setup(x => x.Generate()).Returns("testId");
    }

    [Fact]
    public void GetId_Returns_Expected_Id_When_UserAttributes_Is_Null()
    {
        // Arrange
        const string expectedId = "testId";
        var _sut = new TestComponent { ElementId = expectedId };

        // Act
        var result = _sut.GetElementId();

        // Assert
        result.Should().Be(expectedId);
    }

    [Fact]
    public void GetId_ReturnsId_FromUserAttributeIfExists()
    {
        /// Arrange
        const string expectedId = "testId";
        var userAttributes = new Dictionary<string, object>
            {
                { "id", expectedId }
            };
        var _sut = new TestComponent { UserAttributes = userAttributes };

        // Act
        var result = _sut.GetElementId();

        // Assert
        result.Should().Be(expectedId);
    }

    [Fact]
    public void GetId_ReturnsId_WhenUserAttributeIdIsNullOrEmpty()
    {
        // Arrange
        const string expectedId = "testId";
        var userAttributes = new Dictionary<string, object>
            {
                { "id", string.Empty }
            };
        var _sut = new TestComponent { UserAttributes = userAttributes, ElementId = expectedId };

        // Act
        var result = _sut.GetElementId();

        // Assert
        result.Should().Be(expectedId);
    }

    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange & Act
        const string expectedId = "testId";
        var _sut = new TestComponent { ElementId = expectedId };

        // Assert
        _sut.Should().NotBeNull();
        _sut.Visible.Should().BeTrue(); // Default value
        _sut.ElementId.Should().NotBeNullOrEmpty();
    }
}

public class TestComponent : BaseBlentComponent { }
