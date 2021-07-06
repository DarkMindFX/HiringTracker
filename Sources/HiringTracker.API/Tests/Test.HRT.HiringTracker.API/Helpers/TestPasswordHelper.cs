using HRT.HiringTracker.API.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.E2E.HiringTracker.API.Helpers
{
    public class TestPasswordHelper
    {
        [Fact]
        public void GenerateSalt_Success()
        {
            string salt = PasswordHelper.GenerateSalt(10);

            Assert.NotNull(salt);
            Assert.True(!string.IsNullOrEmpty(salt));
            Assert.Equal(10, salt.Length);
        }

        [Fact]
        public void GenerateSalt_InvalidLength()
        {
            try
            {
                string salt = PasswordHelper.GenerateSalt(0);
                Assert.False(true, $"ArgumentException was expected but salt is generated instead - {salt}");
            }
            catch (ArgumentException)
            {
                Assert.True(true, "ArgumentException thrown as expected");
            }
        }

        [Fact]
        public void GeneratePasswordHash_Success()
        {
            string password = "TestPassword-202107061104";
            string salt = "SALT12345";

            string pwdHash = PasswordHelper.GenerateHash(password, salt);

            Assert.NotNull(pwdHash);
            Assert.True(!string.IsNullOrEmpty(pwdHash));
        }

        [Fact]
        public void GeneratePasswordHash_EmptySalt()
        {
            try
            {
                string password = "TestPassword-202107061104";
                string salt = string.Empty;

                string pwdHash = PasswordHelper.GenerateHash(password, salt);

                Assert.False(true, $"ArgumentException was expected but pwdHash is generated instead - {pwdHash}");
            }
            catch (ArgumentException)
            {
                Assert.True(true, "ArgumentException thrown as expected");
            }
        }

        [Fact]
        public void GeneratePasswordHash_EmptyPassword()
        {
            try
            {
                string password = string.Empty;
                string salt = "SALT12345";

                string pwdHash = PasswordHelper.GenerateHash(password, salt);

                Assert.False(true, $"ArgumentException was expected but pwdHash is generated instead - {pwdHash}");
            }
            catch (ArgumentException)
            {
                Assert.True(true, "ArgumentException thrown as expected");
            }
        }
    }
}
