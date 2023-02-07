namespace ManageEntityProperties.Entities;

public class Product : IEntityBase
{
    public int Id { get; set; }

    public string Status { get; set; } = "A";

    public DateTime ModifiedOn { get; set; } = DateTime.MinValue;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
