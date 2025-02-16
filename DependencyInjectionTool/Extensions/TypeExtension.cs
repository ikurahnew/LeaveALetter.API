namespace DependencyInjectionTool.Extensions;

public static class TypeExtension
{
    #region Public Methods

    /// <summary>
    /// Retrieves all interfaces directly implemented by the specified type.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> to be checked for implementing interfaces.</param>
    /// <returns>A <see cref="IEnumerable{Type}"/> representing all interfaces directly implemented by the specified type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="type"/> parameter is null.</exception>
    public static IEnumerable<Type> GetDirectInterfaces(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var allInterfaces = type.GetInterfaces();
        var directInterfaces = allInterfaces.Except(allInterfaces.SelectMany(implementedInterface => implementedInterface.GetInterfaces()));

        return type.BaseType is null
            ? directInterfaces
            : directInterfaces.Except(type.BaseType.GetInterfaces());
    }

    #endregion Public Methods
}