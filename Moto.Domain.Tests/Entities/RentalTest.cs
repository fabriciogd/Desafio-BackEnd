using Moto.Domain.Entities;

namespace Moto.Domain.Tests.Entities
{
    public class RentalTest
    {
        [Fact]
        public void Should_Return_Success_When_All_Fields_Correct()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedEndDate = startDate.AddDays(7);

            var rental = Rental.Create(1, 1, 7, startDate, expectedEndDate);

            Assert.True(rental.IsValid);
            Assert.False(rental.Errors.Any());
        }

        [Fact]
        public void Should_Return_Error_When_Courier_Is_Inavlid()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedEndDate = startDate.AddDays(7);

            var rental = Rental.Create(0, 1, 7, startDate, expectedEndDate);

            Assert.False(rental.IsValid);
            Assert.Collection(rental.Errors, error1 =>
            {
                Assert.Equal("NotEmptyValidator", error1.ErrorCode);
            });
        }

        [Fact]
        public void Should_Return_Error_When_Motorcycle_Is_Inavlid()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedEndDate = startDate.AddDays(7);

            var rental = Rental.Create(1, 0, 7, startDate, expectedEndDate);

            Assert.False(rental.IsValid);
            Assert.Collection(rental.Errors, error1 =>
            {
                Assert.Equal("NotEmptyValidator", error1.ErrorCode);
            });
        }

        [Fact]
        public void Should_Return_Error_When_Plan_Is_Inavlid()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedEndDate = startDate.AddDays(7);

            var rental = Rental.Create(1, 1, 0, startDate, expectedEndDate);

            Assert.False(rental.IsValid);
            Assert.Collection(rental.Errors, error1 =>
            {
                Assert.Equal("NotEmptyValidator", error1.ErrorCode);
            });
        }

        [Fact]
        public void Should_Return_Success_When_End_Date_In_Expected_Date()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedEndDate = startDate.AddDays(7);

            var rental = Rental.Create(1, 1, 1, startDate, expectedEndDate);

            rental.UpdatePlan(new Plan(7, 30, 0.2M));

            rental.Complete(startDate.AddDays(7));

            Assert.True(rental.IsValid);

            Assert.Equal(rental.TotalPayment, 210);
        }

        [Fact]
        public void Should_Return_Fee_When_End_Date_After_Expected_Date()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedEndDate = startDate.AddDays(7);

            var rental = Rental.Create(1, 1, 1, startDate, expectedEndDate);
            rental.UpdatePlan(new Plan(7, 30, 0.2M));

            rental.Complete(startDate.AddDays(8));

            Assert.True(rental.IsValid);

            Assert.Equal(rental.TotalPayment, 260);
        }

        [Fact]
        public void Should_Return_Fee_When_End_Date_Before_Expected_Date()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedEndDate = startDate.AddDays(7);

            var rental = Rental.Create(1, 1, 1, startDate, expectedEndDate);

            rental.UpdatePlan(new Plan(7, 30, 0.2M));

            rental.Complete(startDate.AddDays(6));

            Assert.True(rental.IsValid);

            Assert.Equal(rental.TotalPayment, 186);
        }
    }
}
