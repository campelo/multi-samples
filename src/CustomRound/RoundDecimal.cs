namespace CustomRound;

public class RoundDecimal
{
    public decimal Round(decimal value)
    {
        decimal preResultat = Math.Round(value, 4, MidpointRounding.ToPositiveInfinity);
        return preResultat;
    }
}
