using CarEnthusiast.Models;


namespace CarEnthusiast.Data
{
    public static class DbInitializer
    {
       
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();

            //Look for any user
            if (context.Users.Any())
                return; //DB has been seeded

            var users = new User[]
                {
                new User{Name="Bison", Email="slimeto@gmail.com", Password="slimeto", CreatedAt=DateTime.Now},
                new User{Name="Amber", Email="boutubuss@gmail.com", Password="ambatukam", CreatedAt=DateTime.Now},
                new User{Name="Elvin", Email="somi@gmail.com", Password="ambatukam", CreatedAt=DateTime.Now}
                };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();

            //var cars = new Car[]
            //{
            // //   new Car{Make="", Model="", Year="", Image="", Showroom=""}
            //};

            //foreach (Car c in cars)
            //{
            //    context.Cars.Add(c);
            //}

            //context.SaveChanges();

            //var carDetail = new CarDetail[]
            //{
            //  //  new CarDetail{UserId="", CarId="", DateAdded="", Type=Models.Type.Sports}
            //};

            //foreach (CarDetail cd in carDetail)
            //{
            //    context.CarDetails.Add(cd);
            //}
            //context.SaveChanges();
        }
    }
}
