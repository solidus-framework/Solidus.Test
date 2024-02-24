namespace Solidus.Test;

/// <summary>
/// A base test class that covers common cases.
/// </summary>
public abstract class TestBase
{
    private static readonly EnvironmentVariables _environmentVariables = new();

    private MotherFactory? _motherFactory = null;
    private ITestDataGenerator? _testDataGenerator = null;

#pragma warning disable IDE1006 // Naming Styles for convenient use property names will use filed naming convention
    /// <summary>
    /// Environment variables accessor.
    /// </summary>
    protected static EnvironmentVariables env => _environmentVariables;

    /// <summary>
    /// Test units mother factory.
    /// </summary>
    protected MotherFactory mf
    {
        get
        {
            _motherFactory ??= CreateMotherFactory();
            return _motherFactory;
        }
    }

    /// <summary>
    /// Test Data generation utility.
    /// </summary>
    protected ITestDataGenerator gen
    {
        get
        {
            _testDataGenerator ??= CreateTestDataGenerator();
            return _testDataGenerator;
        }
    }
#pragma warning restore IDE1006 // Naming Styles

    /// <summary>
    /// Creates a mother factory instance.
    /// </summary>
    /// <returns>The mother factory instance.</returns>
    protected virtual MotherFactory CreateMotherFactory() => new();

    /// <summary>
    /// Creates a test data generator instance.
    /// </summary>
    /// <returns>The test data generator instance.</returns>
    protected virtual ITestDataGenerator CreateTestDataGenerator() => DefaultTestDataGenerator.CreateDefaultTestDataGenerator();
}
