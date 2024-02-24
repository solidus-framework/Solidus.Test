namespace Solidus.Test;

/// <summary>
/// Test data generator.
/// </summary>
public interface ITestDataGenerator
{
    /// <summary>
    /// Creates test data value.
    /// </summary>
    /// <typeparam name="T">The test data type.</typeparam>
    /// <returns>The test data.</returns>
    T Create<T>();
}
