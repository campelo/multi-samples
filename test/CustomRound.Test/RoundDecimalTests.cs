using Xunit;
using Shouldly;

namespace CustomRound.Test;

public class RoundDecimalTests
{
    [Theory]
    [InlineData(5.1234, 5.1234)]
    [InlineData(5.12340, 5.1234)]
    [InlineData(5.12341, 5.1235)]
    [InlineData(5.12342, 5.1235)]
    [InlineData(5.12343, 5.1235)]
    [InlineData(5.12344, 5.1235)]
    [InlineData(5.12345, 5.1235)]
    [InlineData(5.12346, 5.1235)]
    [InlineData(5.12347, 5.1235)]
    [InlineData(5.12348, 5.1235)]
    [InlineData(5.12349, 5.1235)]
    [InlineData(5.123401, 5.1235)]
    [InlineData(5.123402, 5.1235)]
    [InlineData(5.123403, 5.1235)]
    [InlineData(5.123404, 5.1235)]
    [InlineData(5.123405, 5.1235)]
    [InlineData(5.123406, 5.1235)]
    [InlineData(5.123407, 5.1235)]
    [InlineData(5.123408, 5.1235)]
    [InlineData(5.123409, 5.1235)]
    public void RoundDecimal_ShouldReturnExpectedResult(decimal valeur, decimal expectedResult)
    {
        // arrange
        // act
        decimal result = new RoundDecimal().Round(valeur);

        // assert...
        result.ShouldBe(expectedResult);
    }
}
