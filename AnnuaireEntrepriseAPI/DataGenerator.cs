using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnnuaireEntrepriseAPI.Database;
using AnnuaireEntrepriseAPI.DTOs;
using AnnuaireEntrepriseAPI.Models;
using Bogus;

namespace AnnuaireEntrepriseAPI
{
    public class DataGenerator
    {
        private readonly AnnuaireEntrepriseContext _context;

        public DataGenerator(AnnuaireEntrepriseContext context)
        {
            _context = context;
        }

        public void GenerateData(AnnuaireEntrepriseContext dataContext)
        {
            var faker = new Faker("fr");

            var random = new Random();

            var employees = new List<User>();

            for (int i = 0; i < 1000; i++)
            {
                var mobilePhone = Convert.ToInt32(GenerateRandomPhoneNumber(random));
                var phoneNumber = Convert.ToInt32(GenerateRandomPhoneNumber(random));

                var employee = new User
                {
                    Name = faker.Name.LastName(),
                    Surname = faker.Name.FirstName(),
                    MobilePhone = mobilePhone.ToString(),
                    PhoneNumber = phoneNumber.ToString(),
                    Email = faker.Internet.Email(),
                    Service = GetRandomService(random),
                    Site = GetRandomSite(random)

                };

                employees.Add(employee);
            }

            dataContext.Users.AddRange(employees);
            dataContext.SaveChanges();

            Console.WriteLine("Insertion des données terminée !");
        }

        private string GenerateRandomPhoneNumber(Random random)
        {
            var phoneNumber = "0";

            for (int i = 0; i < 9; i++)
            {
                phoneNumber += random.Next(0, 10);
            }

            return phoneNumber;
        }

        private Service GetRandomService(Random random)
        {
            List<Service> services = _context.Services.ToList();


            return services[random.Next(services.Count)];
        }

        private Site GetRandomSite(Random random)
        {
            List<Site> sites = _context.Sites.ToList();

            return sites[random.Next(sites.Count)];
        }
    }
}