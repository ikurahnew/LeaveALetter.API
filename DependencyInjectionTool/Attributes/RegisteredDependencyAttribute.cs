using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionTool.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegisteredDependencyAttribute(Type? dependencyType = null, ServiceLifetime lifetime = ServiceLifetime.Scoped, object? dependencyKey = null) : Attribute
{
    #region Properties

    /// <summary>
    /// The <see cref="Type"/> of the dependency.
    /// </summary>
    public virtual Type? DependencyType { get; set; } = dependencyType;

    /// <summary>
    /// The <see cref="ServiceLifetime"/> of the dependency.
    /// </summary>
    public virtual ServiceLifetime Lifetime { get; set; } = lifetime;

    /// <summary>
    /// The key of the dependency if the service should be keyed.
    /// </summary>
    public virtual object? DependencyKey { get; set; } = dependencyKey;

    #endregion Properties

    #region Public Constructors

    /// <inheritdoc />
    public RegisteredDependencyAttribute(ServiceLifetime lifetime)
        : this(null, lifetime) { }

    /// <inheritdoc />
    public RegisteredDependencyAttribute(object dependencyKey, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        : this(null, lifetime, dependencyKey) { }

    #endregion Public Constructors
}