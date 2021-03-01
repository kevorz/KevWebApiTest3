using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using TodoApi.Models;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {

            SetupTestDB();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SetupTestDB()
        {
            string dbName = "TodoSqlite.db";

            if (File.Exists(dbName))
            {
                //Yay!
            }
            using var dbContext = new TodoContext();
            //Ensure database is created
            dbContext.Database.EnsureCreated();
            if (!dbContext.TodoItems.Any())
            {
                dbContext.TodoItems.AddRange(new TodoItem[]
                    {
                             new TodoItem{ Id=1, Name="Blog 1", IsComplete=false },
                             new TodoItem{ Id=2, Name="Blog 2", IsComplete=true},
                             new TodoItem{ Id=3, Name="Blog 3", IsComplete=false }
                    });
                dbContext.SaveChanges();
            }

        }
    }
}
