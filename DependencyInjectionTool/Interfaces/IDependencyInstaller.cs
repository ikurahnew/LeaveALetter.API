using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionTool.Interfaces;

/// <summary>
/// Defines the contract for installing depencies into a <see cref="IServiceCollection"/>.
/// </summary>
public interface IDependencyInstaller
{
    /// <summary>
    /// Install the required dependencies into the provided services based on the given configuration.
    /// </summary>
    /// <param name="services">The collection of service descriptors to which the dependencies will be added.</param>
    /// <param name="configuration">The configuration properties which might be required for configuring services.</param>
    /// <returns>The modified instance of <see cref="IServiceCollection"/> after the depencies have been installed to allow for chaining.</returns>
    public IServiceCollection InstallDependencies(IServiceCollection services, object? configuration);
}
