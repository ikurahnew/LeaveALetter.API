using LeaveALetter.API.Data._Shared.Entities;

namespace LeaveALetter.API.Data._Shared.Repositories;

/// <summary>
/// The contract for the abstract entity repository to handle the entity operations.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Retrieve an entity from the fake data by its id.
    /// </summary>
    /// <param name="Id">The Id of the entity.</param>
    /// <param name="fakeData">A fake database to pass to mimic database.</param>
    /// <returns>The existing entity or null if not found.</returns>
    TEntity? GetById(long Id, ICollection<TEntity> fakeData);

    /// <summary>
    /// Create a new entity.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    TEntity Create(TEntity entity);

    /// <summary>
    /// Deprecate an entity.
    /// </summary>
    /// <param name="entity">The entity to deprecate.</param>
    /// <returns>The deprecated entity.</returns>
    TEntity Deprecate(TEntity entity);
}


/// <summary>
/// The abstract entity repository to handle the entity operations.
/// </summary>
/// <typeparam name="TEntity">The entity to be handled.</typeparam>
public abstract class EntityRepository<TEntity>: IEntityRepository<TEntity> where TEntity : Entity
{
    /// <inheritdoc />
    public TEntity? GetById(long Id, ICollection<TEntity> fakeData)
    {
        return fakeData.FirstOrDefault(x => x.Id == Id);
    }

    /// <inheritdoc />
    public TEntity Create(TEntity entity)
    {
        entity.Id = new Random().Next(1, 1000);
        entity.CreatedDate = DateTime.Now;
        entity.IsDeprecated = false;
        return entity;
    }

    /// <inheritdoc />
    public TEntity Deprecate(TEntity entity)
    {
        entity.IsDeprecated = true;
        return entity;
    }
}
