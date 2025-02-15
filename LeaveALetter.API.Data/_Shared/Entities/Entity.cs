namespace LeaveALetter.API.Data._Shared.Entities;

/// <summary>
/// An abstract data for all entity object.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// The identifier of the entity.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The created date of the entity.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Whether the object is deprecated or not.
    /// </summary>
    public bool IsDeprecated { get; set; }
}
