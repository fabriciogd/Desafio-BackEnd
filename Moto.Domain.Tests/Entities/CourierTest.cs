using Moto.Domain.Entities;

namespace Moto.Domain.Tests.Entities;

public class CourierTest
{
    [Fact]
    public void Should_Return_Success_When_All_Fields_Correct()
    {
        var courier = Courier.Create("70319171000163", new DateOnly(1990, 10, 25), "14035198001", "A");

        Assert.True(courier.IsValid);
        Assert.False(courier.Errors.Any());
    }

    [Fact]
    public void Should_Return_Error_When_Cnpj_Is_Empty()
    {
        var courier = Courier.Create("", new DateOnly(1990, 10, 25), "14035198001", "A");

        Assert.False(courier.IsValid);
        Assert.Collection(courier.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        },
         error2 =>
         {
             Assert.Equal("ExactLengthValidator", error2.ErrorCode);
         },
         error3 =>
         {
             Assert.Equal("CnpjValidator", error3.ErrorCode);
         });
    }

    [Fact]
    public void Should_Return_Error_When_Date_Is_Empty()
    {
        var courier = Courier.Create("70319171000163", new DateOnly(), "14035198001", "A");

        Assert.False(courier.IsValid);
        Assert.Collection(courier.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_Driving_License_Is_Empty()
    {
        var courier = Courier.Create("70319171000163", new DateOnly(1990,10,25), "", "A");

        Assert.False(courier.IsValid);
        Assert.Collection(courier.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        },
        error2 =>
        {
            Assert.Equal("ExactLengthValidator", error2.ErrorCode);
        },
        error3 =>
        {
            Assert.Equal("CnhValidator", error3.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_Driving_Type_Is_Empty()
    {
        var courier = Courier.Create("70319171000163", new DateOnly(1990, 10, 25), "14035198001", "");

        Assert.False(courier.IsValid);
        Assert.Collection(courier.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_Age_Under_18()
    {
        var courier = Courier.Create("70319171000163", new DateOnly(2010, 10, 25), "14035198001", "A");

        Assert.False(courier.IsValid);
        Assert.Collection(courier.Errors, error1 =>
        {
            Assert.Equal("LessThanOrEqualValidator", error1.ErrorCode);
        });
    }
}
