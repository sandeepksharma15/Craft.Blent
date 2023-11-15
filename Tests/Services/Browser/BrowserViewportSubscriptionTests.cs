using Craft.Blent.Services.Browser;
using FluentAssertions;

namespace Craft.Blent.Tests.Services.Browser;

public class BrowserViewportSubscriptionTests
{
    [Fact]
    public void Equals_ReturnsTrueForEqualObjects()
    {
        // Arrange
        var subscription1 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        var subscription2 = new BrowserViewportSubscription(subscription1.JavaScriptListenerId, subscription1.ObserverId);

        // Act
        var result = subscription1.Equals(subscription2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_ReturnsFalseForDifferentObjects()
    {
        // Arrange
        var subscription1 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        var subscription2 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var result = subscription1.Equals(subscription2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_ObjectOverload_ReturnsTrueForEqualObjects()
    {
        // Arrange
        var subscription = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        object obj = new BrowserViewportSubscription(subscription.JavaScriptListenerId, subscription.ObserverId);

        // Act
        var result = subscription.Equals(obj);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_ObjectOverload_ReturnsFalseForDifferentObjects()
    {
        // Arrange
        var subscription1 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        var subscription2 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        object obj = subscription2;

        // Act
        var result = subscription1.Equals(obj);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_ObjectOverload_ReturnsFalseForObjectIsNull()
    {
        // Arrange
        var subscription = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        object? obj = null;

        // Act
        var result = subscription.Equals(obj);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var subscription = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        BrowserViewportSubscription? other = null;

        // Act
        var result = subscription.Equals(other);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ReturnsSameValueForEqualObjects()
    {
        // Arrange
        var subscription1 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        var subscription2 = new BrowserViewportSubscription(subscription1.JavaScriptListenerId, subscription1.ObserverId);

        // Act
        var hashCode1 = subscription1.GetHashCode();
        var hashCode2 = subscription2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void GetHashCode_ReturnsDifferentValueForDifferentObjects()
    {
        // Arrange
        var subscription1 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());
        var subscription2 = new BrowserViewportSubscription(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var hashCode1 = subscription1.GetHashCode();
        var hashCode2 = subscription2.GetHashCode();

        // Assert
        hashCode1.Should(). NotBe(hashCode2);
    }
}
