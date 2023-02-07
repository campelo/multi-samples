namespace ManageEntityProperties.Entities;

public interface IEntityBase
{
    /// <summary>
    /// A - Active
    /// I - Inactive
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// For audit, date time when the element was saved.
    /// </summary>
    public DateTime ModifiedOn { get; set; }

}
