namespace FriendShowDataAccess.Migrations
{
    using FriendsShow.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendsShowDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendsShowDbContext context)
        {
            context.Friends.AddOrUpdate(f => f.FirstName,
                new Friend() { FirstName = "Victor", LastName = "Mark", Email = "vee@hotmail.com" },
                new Friend() { FirstName = "John", LastName = "Deo", Email = "jay@gmail.com" },
                new Friend() { FirstName = "Solomon", LastName = "Tim", Email = "Sol@gmail.com" },
                new Friend() { FirstName = "Matthew", LastName = "Jessy", Email = "Matt@gmail.com" },
                new Friend() { FirstName = "Angel", LastName = "Papa", Email = "Angee@outlook.com" }
                );

            context.ProgrammingLanguages.AddOrUpdate(pl => pl.Name,
                new ProgrammingLanguage() { Name = "C#" },
                new ProgrammingLanguage() { Name = "Swift" },
                new ProgrammingLanguage() { Name = "TypeScript" },
                new ProgrammingLanguage() { Name = "F#" },
                new ProgrammingLanguage() { Name = "Php" },
                new ProgrammingLanguage() { Name = "C++" }
                );

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                new FriendPhoneNumber { Number = "+49 12345678", FriendId = context.Friends.First().Id });

            context.Meetings.AddOrUpdate(m => m.Title,
            new Meeting
            {
                Title = "Watching Soccer",
                DateFrom = new DateTime(2019, 3, 3),
                DateTo = new DateTime(2019, 3, 3),
                Friends = new List<Friend>
                {
                    context.Friends.Single(f => f.FirstName == "Victor" && f.LastName == "Mark"),
                    context.Friends.Single(f => f.FirstName == "John" && f.LastName == "Deo")
                }
            });
        }
    }
}
