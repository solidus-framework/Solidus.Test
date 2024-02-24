using AutoFixture;

namespace Solidus.Test;

/// <summary>
/// Test data generator based on <see cref="Fixture"> by default.
/// </summary>
public class DefaultTestDataGenerator : ITestDataGenerator
{
    private static Func<ITestDataGenerator> Factory = () => new DefaultTestDataGenerator();

    /// <summary>
    /// Creates test data generator instance.
    /// </summary>
    /// <returns>The test data generator instance.</returns>
    public static ITestDataGenerator CreateDefaultTestDataGenerator() => Factory();

    /// <summary>
    /// Sets default test data generator factory for all test.
    /// </summary>
    /// <param name="factory">The test data generator factory.</param>
    public static void SetDefaultTestDataGenerator(Func<ITestDataGenerator> factory) => Factory = factory;

    private readonly IFixture _fixture;

    private DefaultTestDataGenerator()
    {
        _fixture = new Fixture();
    }

    /// <inheritdoc/>
    public T Create<T>() => _fixture.Create<T>();
}
