using DependencyInjectionTool.Attributes;
using DependencyInjectionTool.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace DependencyInjectionTool.Extensions;

public static class ServiceCollectionExtension
{
    #region Public Methods

    /// <summary>
    /// Adds all dependencies from an assembly marked with the <see cref="RegisteredDependencyAttribute"/> to the specified service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the dependencies are to be added.</param>
    /// <param name="assembly">The <see cref="Assembly"/> to scan for dependencies.</param>
    /// <param name="logger">An optional <see cref="ILogger"/> for logging. Can be null if logging is not required.</param>
    /// <returns>The <see cref="IServiceCollection"/> to allow for chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the specified <see cref="Assembly"/> is null.</exception>
    public static IServiceCollection AddRegisteredDependenciesFromAssembly(this IServiceCollection services, Assembly assembly, ILogger? logger = null)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var registeredDependencyTypes = assembly.DefinedTypes
            .Where(typeInfo => typeInfo.IsDefined(typeof(RegisteredDependencyAttribute), false)).ToList();

        logger?.LogInformation("Number of dependencies found in {assembly}: {dependencyCount}", assembly, registeredDependencyTypes.Count);

        registeredDependencyTypes.ForEach(registeredDependencyType =>
            AddRegisteredDependency(services, registeredDependencyType, logger));

        return services;
    }

    /// <summary>
    /// Adds all dependencies from all specified assemblies marked with the <see cref="RegisteredDependencyAttribute"/> to the specified service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the dependencies are to be added.</param>
    /// <param name="assemblies">The list of <see cref="Assembly"/> instances to scan for dependencies.</param>
    /// <param name="logger">An optional <see cref="ILogger"/> for logging. Can be null if logging is not required.</param>
    /// <returns>The <see cref="IServiceCollection"/> to allow for chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the specified <see cref="Assembly"/> is null.</exception>
    public static IServiceCollection AddRegisteredDependenciesFromAssemblies(this IServiceCollection services, ILogger? logger = null, params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies);

        logger?.LogInformation("Registering dependencies from {numberOfAssemblies} assemblies.", assemblies.Length);

        foreach (var assembly in assemblies)
        {
            services.AddRegisteredDependenciesFromAssembly(assembly, logger);
        }

        return services;
    }

    /// <inheritdoc cref="AddRegisteredDependenciesFromAssemblies(IServiceCollection, ILogger?, Assembly[])"/>
    public static IServiceCollection AddRegisteredDependenciesFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
        return AddRegisteredDependenciesFromAssemblies(services, null, assemblies);
    }

    /// <summary>
    /// Installs dependencies into a <see cref="IServiceCollection"/> from all the <see cref="IDependencyInstaller"/> implementations found in the specified <paramref name="assembly"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the dependencies to.</param>
    /// <param name="assembly">The assembly to scan for <see cref="IDependencyInstaller"/> implementations.</param>
    /// <param name="configuration">The configuration to pass to the installers.</param>
    /// <param name="logger">An optional <see cref="ILogger"/> for logging. Can be null if logging is not required.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="assembly"/> is null.</exception>
    /// <returns>The <see cref="IServiceCollection"/> with dependencies installed to allow for chaining.</returns>
    public static IServiceCollection InstallDependenciesFromAssembly(this IServiceCollection services, Assembly assembly, object? configuration = null)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var dependencyInstallers = assembly.DefinedTypes
            .Where(typeInfo => typeInfo is { IsInterface: false, IsAbstract: false } && typeof(IDependencyInstaller).IsAssignableFrom(typeInfo))
            .Select(Activator.CreateInstance)
            .Cast<IDependencyInstaller>()
            .ToList();

        dependencyInstallers.ForEach(dependencyInstaller => dependencyInstaller.InstallDependencies(services, configuration));

        return services;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Registers a single dependency to the specified service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the dependency to.</param>
    /// <param name="implementationType">The <see cref="Type"/> of the implementation to register.</param>
    /// <param name="logger">An optional <see cref="ILogger"/> for logging. Can be null if logging is not required.</param>
    /// <returns>The <see cref="IServiceCollection"/> to allow for chaining.</returns>
    private static IServiceCollection AddRegisteredDependency(IServiceCollection services, Type implementationType, ILogger? logger = null)
    {
        var registeredDependencyAttribute = implementationType.GetCustomAttribute<RegisteredDependencyAttribute>()!;

        var dependencyLifetime = registeredDependencyAttribute.Lifetime;
        var dependencyType = registeredDependencyAttribute.DependencyType ?? GetDefaultDependencyType(implementationType, logger);
        var dependencyKey = registeredDependencyAttribute.DependencyKey;

        var serviceDescriptor = new ServiceDescriptor(dependencyType, dependencyKey, implementationType, dependencyLifetime);
        services.Add(serviceDescriptor);

        logger?.LogInformation("Dependency Registered, Dependency type: {dependencyType}, Implementation Type: {implementationType}, lifetime: {dependencyLifetime}, key: {dependencyKey}", dependencyType, implementationType, dependencyLifetime, dependencyKey);

        return services;
    }

    /// <summary>
    /// Gets the default dependency type for a given implementation.
    /// </summary>
    /// <param name="implementationType">The type of the implementation for which to get the dependency type.</param>
    /// <param name="logger">An optional <see cref="ILogger"/> for logging. Can be null if logging is not required.</param>
    /// <returns>The default dependency type for the specified implementation.</returns>
    /// <exception cref="ArgumentException">Thrown if the implementation type implements more than one interface and no default is specified.</exception>
    private static Type GetDefaultDependencyType(Type implementationType, ILogger? logger = null)
    {
        var directInterfaces = implementationType.GetDirectInterfaces().ToList();

        if (directInterfaces.Count > 1)
        {
            throw new ArgumentException("Dependency Type must be explicitly specified if a dependency implements multiple interfaces.");
        }

        var defaultDependencyType = directInterfaces.Count == 0
            ? implementationType
            : directInterfaces.First();

        return defaultDependencyType;
    }

    #endregion Private Methods
}