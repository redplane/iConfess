using System;
using System.Data.Entity.Migrations;
using iConfess.Database.Enumerations;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;

namespace iConfess.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ConfessionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ConfessionDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            #region Accounts

            // Element index initiator.
            var indexGenerator = new Random();

            for (var i = 0; i < 100; i++)
            {
                var account = new Account();
                account.Email = $"{Guid.NewGuid().ToString("N")}@gmail.com";
                account.Nickname =
                    $"{_firstNames[indexGenerator.Next(_firstNames.Length)]} {_lastNames[indexGenerator.Next(_lastNames.Length)]}";

                // Password is : administrator
                account.Password = "200CEB26807D6BF99FD6F4F0D1CA54D4";
                account.Status = AccountStatus.Active;

                // Role.
                if (i < 50)
                    account.Role = AccountRole.Admin;
                else
                    account.Role = AccountRole.Ordinary;

                account.PhotoRelativeUrl = "https://s6.postimg.org/w6lcoipkh/avatar_pacman.png";
                account.Joined = 1480762181918;

                // Add or update existing account.
                context.Accounts.AddOrUpdate(account);
            }

            #endregion

            #region Categories

            #endregion
        }

        #region Properties

        /// <summary>
        ///     List of first names.
        /// </summary>
        private readonly string[] _firstNames = {"Nguyen", "Tran", "Cao", "Do", "La", "Le"};

        /// <summary>
        ///     List of last names.
        /// </summary>
        private readonly string[] _lastNames =
        {
            "Linh", "Duong", "Phuong", "Tuan", "Hung", "Thang", "Trong", "Hoang Anh",
            "Hieu"
        };

        #endregion
    }
}