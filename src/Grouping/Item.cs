namespace Grouping;

public record Item(string Unity, string Serie, string Order, string Value)
{
    public override string ToString()
    {
        return $"{Unity} - {Serie} - {Order} - {Value}";
    }
}
