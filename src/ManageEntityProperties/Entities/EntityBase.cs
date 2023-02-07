namespace ManageEntityProperties.Entities;

public class EntityBase
{
    /// <summary>
    /// A - Active
    /// I - Inactive
    /// </summary>
    public string Status { get; set; } = String.Empty;

    /// <summary>
    /// For audit, date time when the element was saved.
    /// </summary>
    public DateTime ModifiedOn { get; set; } = DateTime.MinValue;

}
