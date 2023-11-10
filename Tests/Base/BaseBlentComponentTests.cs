using Craft.Blent.Base;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Bunit;

namespace Craft.Blent.Tests.Base;

public class BaseBlentComponentTests : TestContext
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly Mock<ILogger> _loggerMock;

    public BaseBlentComponentTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _loggerMock = new Mock<ILogger>();
        _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(_loggerMock.Object);
    }

    [Fact]
    public void GetId_Returns_Expected_Id_When_UserAttributes_Is_Null()
    {
        // Arrange
        const string expectedId = "testId";
        var _sut = new TestComponent { ElementId = expectedId };

        // Act
        var result = _sut.GetId();

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
        var result = _sut.GetId();

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
        var result = _sut.GetId();

        // Assert
        result.Should().Be(expectedId);
    }

    [Fact]
    public void OnInitialized_Sets_UniqueId()
    {
        // Arrange
        var cut = RenderComponent<BaseBlentComponent>();
        var _sut = cut.Instance;

        // Act
        // _sut.OnInitialized();

        // Assert
        _sut.ElementId.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void SetParametersAsync_Does_Not_Call_StateHasChanged_When_Visible_Is_Not_Changed()
    {
        // Arrange
        var cut = RenderComponent<BaseBlentComponent>();
        var _sut = cut.Instance;

        _sut.Visible = false;

        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Visible, false));

        // Assert
        _sut.Visible.Should().BeFalse();
    }

    [Fact]
    public void SetParametersAsync_Calls_StateHasChanged_When_Visible_Is_Changed()
    {
        // Arrange
        var cut = RenderComponent<BaseBlentComponent>();
        var _sut = cut.Instance;

        _sut.Visible = true;

        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Visible, false));

        // Assert
        _sut.Visible.Should().BeFalse();
    }
}

public class TestComponent : BaseBlentComponent { }
