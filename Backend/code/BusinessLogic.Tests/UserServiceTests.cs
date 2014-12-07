using NUnit.Framework;
using Hangout.DataAccess;
using Simple.Mocking;
using Hangout.BusinessLogic;
using Hangout.Entities;
using System.Linq;
using System;
using Hangout.Core;

namespace BusinessLogic.Tests
{
	[TestFixture]
	public class UserManagementTests
	{

		[Test]
		public void EmptyLoginCredsTest ()
		{

            const string userName = "";
            const string password = "";

            var dataAccess = Mock.Interface<IDataAccess>();
            var userService = new UserService(dataAccess);

            Assert.Throws<ArgumentNullException>(() =>
                {
                    User user;
                    userService.Login(userName, password, out user);
                });

		}

        [Test]
        public void InvalidCredsTest()
        {

            const string userName = "username1";
            const string password = "password1";

            var dataAccess = Mock.Interface<IDataAccess>();
            var userService = new UserService(dataAccess);

            var users = new User[0];
            Expect.MethodCall( () => dataAccess.All<User>() ).Returns( users.AsQueryable() );

            Assert.Throws<BadCredentialsException>(() =>
                {
                    User user;
                    userService.Login(userName, password, out user);
                });

        }

        [Test]
        public void LoginTest()
        {

            const string userName = "user1";
            const string password = "password1";

            var dataAccess = Mock.Interface<IDataAccess>();

            var expectedUser = new User
            {
                    Email = userName,
                    Password = PasswordHash.HashPassword (password),
                    PhoneNumber = "000-000-0000",
            };

            var users = new [] { expectedUser };
            Expect.MethodCall( () => dataAccess.All<User>() ).Returns( users.AsQueryable() );

            var userService = new UserService(dataAccess);

            User actualUser;
            userService.Login(userName, password, out actualUser);

            Assert.IsTrue(actualUser == expectedUser);

        }

        [Test]
        public void CreateNullUserTest()
        {

            var dataAccess = Mock.Interface<IDataAccess>();
            var userService = new UserService(dataAccess);

            Assert.Throws<ArgumentNullException>(() =>
            {
                User newUser = null;
                userService.CreateUser(newUser);
            });

        }

        [Test]
        public void InvalidUserCreationTet()
        {

            var dataAccess = Mock.Interface<IDataAccess>();
            var userService = new UserService(dataAccess);

            Assert.Throws<ValidationException>(() =>
            {
                var newUser = new User();
                userService.CreateUser(newUser);
            });

        }

	}
}