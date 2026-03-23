namespace ChessExample.Tests;

public class CheckerTests
{
    [Fact]
    public void XLetter_ToX()
    {
        // Arrange
        var positions = new List<CheckerBoardPosition>();
        for (byte x = 1; x <= 8; x++)
        {
            for (byte y = 1; y <= 8; y++)
            {
                positions.Add(new CheckerBoardPosition(x, y));
            }
        }

        // Assert
        foreach (var pos in positions)
        {
            var expectedLetter = (char)('@' + pos.X);
            Assert.Equal(expectedLetter, pos.XLetter);
        }
    }

    [Theory]
    [InlineData(2,6)]
    [InlineData(1,3)]
    [InlineData(8,8)]
    public void Position_PassValid(byte x, byte y)
    {
        //Act
        var position = new CheckerBoardPosition(x,y);

        //Assert
        Assert.Equal(x, position.X);
        Assert.Equal(y, position.Y);
    }

    [Theory]
    [InlineData(9,10)]
    [InlineData(1,16)]
    [InlineData(11,6)]
    public void Position_PassError(byte x, byte y)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new CheckerBoardPosition(x, y));
    }

    [Theory]
    [InlineData(1, 'A')]
    [InlineData(2, 'B')]
    [InlineData(3, 'C')]
    [InlineData(4, 'D')]
    [InlineData(5, 'E')]
    [InlineData(6, 'F')]
    [InlineData(7, 'G')]
    [InlineData(8, 'H')]
    public void XLetters_PassValid(byte x, char symb)
    {
        //Arrange
        var position = new CheckerBoardPosition(x, 1);

        //Act
        var letter = position.XLetter;

        //Assert
        Assert.Equal(symb, letter);  
    }

    [Theory]
    [InlineData(1,2,"A2")]
    [InlineData(3,4,"C4")]
    public void ToString_PassValid(byte x, byte y, string str)
    {
        //Arrange
        var position = new CheckerBoardPosition(x, y);

        //Act
        var result = position.ToString();

        //Assert
        Assert.Equal(str, result);
    }

    [Theory]
    [InlineData("A1",1,1)]
    [InlineData("C4",3,4)]
    public void Parse_PassValid(string input,byte x, byte y)
    {
        //Act
        var position = new CheckerBoardPosition(x, y);

        //Assert
        Assert.Equal(x, position.X);
        Assert.Equal(y, position.Y);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("A11")]
    [InlineData("AA")]              
    [InlineData("11")]
    public void Parse_PassError(string input)
    {
        var exception = Assert.Throws<FormatException>(() => CheckerBoardPosition.Parse(input, null));
    }

    [Theory]
    [InlineData("B3", 2, 3)]
    [InlineData("D1", 4, 1)]
    [InlineData("E5", 5, 5)]
    public void TryParse_PassValid(string input, byte x, byte y)
    {
        //Act
        var result = CheckerBoardPosition.TryParse(input,null,out var position);

        //Assert
        Assert.True(result);
        Assert.NotNull(position);
        Assert.Equal(x, position.X);
        Assert.Equal(y, position.Y);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("A11")]
    [InlineData("AA")]
    [InlineData("11")]
    public void TryParse_PassError(string input)
    {
        // Act
        var success = CheckerBoardPosition.TryParse(input, null, out var position);

        // Assert
        Assert.False(success);
        Assert.Null(position);
    }
}