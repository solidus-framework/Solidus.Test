using System.Text.Json;

namespace Solidus.Test;

/// <summary>
/// An environment variables accessor.
/// </summary>
public class EnvironmentVariables
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly Dictionary<string, object> JsonDataCache = [];

    /// <summary>
    /// Read environment variable value.
    /// </summary>
    /// <param name="variableName">The environment variable name.</param>
    /// <returns>The environment variable value.</returns>
    /// <exception cref="InvalidOperationException">
    /// When unable to read environment variable value.
    /// </exception>
    public string ReadEnvironmentVariable(string variableName)
    {
        try
        {
            return Environment.GetEnvironmentVariable(variableName)
                ?? throw new InvalidOperationException($"Environment variable '{variableName}' is null.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unable to read environment variable '{variableName}'", ex);
        }
    }

    /// <summary>
    /// Deserialize environment variable JSON value.
    /// </summary>
    /// <param name="variableName">The environment variable name.</param>
    /// <returns>The deserialized environment variable JSON value.</returns>
    /// <exception cref="InvalidOperationException">
    /// When unable to read or deserialize environment variable JSON value.
    /// </exception>
    /// <remarks>
    /// JSON property naming policy is camel case, which will be mapped to pascal case object properties.
    /// </remarks>
    public T ReadJsonFromEnvironmentVariable<T>(string variableName)
    {
        if (JsonDataCache.TryGetValue(variableName, out var cachedResult))
        {
            return (T)cachedResult;
        }

        var json = ReadEnvironmentVariable(variableName);
        try
        {
            var result = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions)
                ?? throw new InvalidOperationException($"'{variableName}' deserializes to null.");

            JsonDataCache[variableName] = result;
            return result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unable to deserialize '{variableName}' to type '{typeof(T).FullName}'.", ex);
        }
    }
}
