using Moto.Domain.Entities;

namespace Moto.Domain.Tests.Entities;

public class CourierTest
{
    [Fact]
    public void Should_Return_Success_When_All_Fields_Correct()
    {
        var motorcycle = Courier.Create("70319171000163", DateTime.Now.AddYears(-18), "14035198001", "A");

        Assert.True(motorcycle.IsValid);
        Assert.False(motorcycle.Errors.Any());
    }

    [Fact]
    public void Should_Return_Error_When_Cnpj_Is_Empty()
    {
        var motorcycle = Courier.Create("", DateTime.Now.AddYears(-18), "14035198001", "A");

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
             Assert.Equal("CnpjValidator", error3.ErrorCode);
         });
    }

    [Fact]
    public void Should_Return_Error_When_Date_Is_Empty()
    {
        var motorcycle = Courier.Create("70319171000163", new DateTime(), "14035198001", "A");

        Assert.False(motorcycle.IsValid);
        Assert.Collection(motorcycle.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_Driving_License_Is_Empty()
    {
        var motorcycle = Courier.Create("70319171000163", new DateTime(1990,10,25), "", "A");

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
            Assert.Equal("CnhValidator", error3.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_Driving_Type_Is_Empty()
    {
        var motorcycle = Courier.Create("70319171000163", new DateTime(1990, 10, 25), "14035198001", "");

        Assert.False(motorcycle.IsValid);
        Assert.Collection(motorcycle.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        });
    }
}
