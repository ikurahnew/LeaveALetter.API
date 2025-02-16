using DependencyInjectionTool.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionTool.Factories;
/// <summary>
///   Represents a factory for creating instances of type T.
/// </summary>
/// <typeparam name="T">The type of instance that will be created..</typeparam>
public interface IFactory<T>
{
    /// <summary>
    ///     Create instance of type T.
    /// </summary>
    /// <returns>Instance of service of type T.</returns>
    T CreateInstance();
    
    /// <summary>
    ///     Creates instace of type Tdpending on the provided key.
    /// </summary>
    /// <param name="key">Key to the service.</param>
    /// <returns>Instances of keyed service of type T.</returns>
    T CreateInstance(object key);
}

/// <summary>
///     Factory model to be used to create instances of type T
/// </summary>
/// <typeparam name="T">The type of which the instances will be created</typeparam>
[RegisteredDependency(typeof(IFactory<>), ServiceLifetime.Scoped)]
public class Factory<T>(IServiceProvider services) : IFactory<T>
{
    /// <inheritdoc/>
    public T CreateInstance() => services.GetRequiredService<T>();

    /// <inheritdoc/>
    public T CreateInstance(object key) => services.GetRequiredKeyedService<T>(key);
}