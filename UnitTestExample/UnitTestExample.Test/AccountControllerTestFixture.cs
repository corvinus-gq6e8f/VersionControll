using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //arrange
            var accountController = new AccountController();

            //act
            var actualResult = accountController.ValidateEmail(email);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [
            Test,
            TestCase("ABCDabcd", false),
            TestCase("ABCD1234", false),
            TestCase("abcd1234", false),
            TestCase("ABCabc1", false),
            TestCase("ABCabc12", true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //arrange
            var accountController = new AccountController();

            //act
            var actualResult = accountController.ValidatePassword(password);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "ABCabc12"),
            TestCase("irf2@uni-corvinus.hu", "Abcd1234")
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            //arrange
            var accountController = new AccountController();

            //act
            var actualResult = accountController.Register(email, password);

            //assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            //arrange
            var accountController = new AccountController();

            //act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {

                Assert.IsInstanceOf<ValidationException>(ex);
            }

            //assert
        }
    }
}
