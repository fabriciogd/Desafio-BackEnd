using Moto.Domain.Entities;

namespace Moto.Domain.Tests.Entities
{
    public class RentalTest
    {
        [Fact]
        public void Should_Return_Success_When_All_Fields_Correct()
        {
            var motorcycle = Rental.Create(1, 1, 7, DateTime.Now.AddDays(7));

            Assert.True(motorcycle.IsValid);
            Assert.False(motorcycle.Errors.Any());
        }

        [Fact]
        public void Should_Return_Error_When_Courier_Is_Inavlid()
        {
            var motorcycle = Rental.Create(0, 1, 7, DateTime.Now.AddDays(7));

            Assert.False(motorcycle.IsValid);
            Assert.Collection(motorcycle.Errors, error1 =>
            {
                Assert.Equal("NotEmptyValidator", error1.ErrorCode);
            });
        }

        [Fact]
        public void Should_Return_Error_When_Motorcycle_Is_Inavlid()
        {
            var motorcycle = Rental.Create(1, 0, 7, DateTime.Now.AddDays(7));

            Assert.False(motorcycle.IsValid);
            Assert.Collection(motorcycle.Errors, error1 =>
            {
                Assert.Equal("NotEmptyValidator", error1.ErrorCode);
            });
        }

        [Fact]
        public void Should_Return_Error_When_Plan_Is_Inavlid()
        {
            var motorcycle = Rental.Create(1, 1, 0, DateTime.Now.AddDays(7));

            Assert.False(motorcycle.IsValid);
            Assert.Collection(motorcycle.Errors, error1 =>
            {
                Assert.Equal("NotEmptyValidator", error1.ErrorCode);
            });
        }

        [Fact]
        public void Should_Return_Error_When_Expected_Date_Is_Inavlid()
        {
            var motorcycle = Rental.Create(1, 1, 1, DateTime.MinValue);

            Assert.False(motorcycle.IsValid);
            Assert.Collection(motorcycle.Errors, error1 =>
            {
                Assert.Equal("NotEmptyValidator", error1.ErrorCode);
            });
        }

        [Fact]
        public void Should_Return_Error_When_Expected_Date_Is_Less_Than_Start_Date()
        {
            var motorcycle = Rental.Create(1, 1, 1, DateTime.Now.AddDays(-2));

            Assert.False(motorcycle.IsValid);
            Assert.Collection(motorcycle.Errors, error1 =>
            {
                Assert.Equal("LessThanStartDate", error1.ErrorCode);
            });
        }
    }
}
