using FluentAssertions;
using Renetdux.Infrastructure.Services.Users;
using Xunit;

namespace Renetdux.Tests
{
    public class UnitTestUsers
    {
        [Theory]
        [InlineData("asd@test.com", "asd", "zxc", "zxc111")]
        public void ValidateUser_Valid(string email, string firstName, string lastName, string password)
        {
            var userService = new UserService();
            var result = userService.ValidateUser(email, firstName, lastName, password);
            result.Result.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "asd", "zxc", "zxc111")]
        [InlineData("asd", "asd", "zxc", "zxc111")]
        [InlineData("asd@test.com", "", "zxc", "zxc111")]
        [InlineData("asd@test.com", "asd", "", "zxc111")]
        [InlineData("asd@test.com", "asd", "zxc", "")]
        [InlineData("asd@test.com", "asd", "zxc", "zxc")]
        [InlineData("asd@test.com", "asd", "zxc", "111")]
        public void ValidateUser_Invalid(string email, string firstName, string lastName, string password)
        {
            var userService = new UserService();
            var result = userService.ValidateUser(email, firstName, lastName, password);
            result.Result.Should().BeFalse();
        }
    }
}
