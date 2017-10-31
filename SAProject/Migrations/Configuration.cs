namespace SAProject.Migrations
{
    using SAProject.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SAProject.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SAProject.DAL.DataContext context)
        {
            var students = new List<Student>
                {
                    new Student{
                        Surname ="Иванов",
                        Name ="Иван",
                        Patronymic ="Иванович",
                        PhoneNumber = "380675552233"
                    },
                    new Student{
                        Surname ="Сидоров",
                        Name ="Дмитрий",
                        Patronymic ="Петрович",
                        PhoneNumber = "380673335599"
                    },
                    new Student{
                        Surname ="Петров",
                        Name ="Олег",
                        Patronymic ="Владимирович",
                        PhoneNumber = "380678887755"
                    }
                };

            students.ForEach(s => context.Students.AddOrUpdate(s));
          

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
