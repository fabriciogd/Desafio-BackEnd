using Moto.Domain.Entities;

namespace Moto.Domain.Tests.Entities;

public class MotorcycleTest
{
    [Fact]
    public void Should_Return_Success_When_All_Fields_Correct()
    {
        var motorcycle = Motorcycle.Create(2024, "Honda", "IQQ-0076");

        Assert.True(motorcycle.IsValid);
        Assert.False(motorcycle.Errors.Any());
    }

    [Fact]
    public void Should_Return_Error_When_Model_Is_Empty()
    {
        var motorcycle = Motorcycle.Create(2024, "", "IQQ-0076");

        Assert.False(motorcycle.IsValid);
        Assert.Collection(motorcycle.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_License_Plate_Is_Empty()
    {
        var motorcycle = Motorcycle.Create(2024, "Honda", "");

        Assert.False(motorcycle.IsValid);
        Assert.Collection(motorcycle.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        },
        error2 =>
        {
            Assert.Equal("MinimumLengthValidator", error2.ErrorCode);
        },
        error3 =>
        {
            Assert.Equal("LicensePlateValidator", error3.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_License_Plate_Is_Incomplete()
    {
        var motorcycle = Motorcycle.Create(2024, "Honda", "IQQ");

        Assert.False(motorcycle.IsValid);
        Assert.Collection(motorcycle.Errors,
            error1 =>
            {
                Assert.Equal("MinimumLengthValidator", error1.ErrorCode);
            },
            error2 =>
            {
                Assert.Equal("LicensePlateValidator", error2.ErrorCode);
            });
    }

    [Fact]
    public void Should_Return_Error_When_License_Plate_Is_Incorrect()
    {
        var motorcycle = Motorcycle.Create(2024, "Honda", "IQQ-00AB");

        Assert.False(motorcycle.IsValid);
        Assert.Collection(motorcycle.Errors,
            error1 =>
            {
                Assert.Equal("LicensePlateValidator", error1.ErrorCode);
            });
    }
}