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
                var mobilePhone = GenerateRandomPhoneNumber(random);
                var phoneNumber = GenerateRandomPhoneNumber(random);

                var randomName = faker.Name.FirstName();
                var randomSurname = faker.Name.LastName();

                var employee = new User
                {
                    Name = randomName,
                    Surname = randomSurname,
                    MobilePhone = mobilePhone.ToString(),
                    PhoneNumber = phoneNumber.ToString(),
                    Email = faker.Internet.Email(randomName, randomSurname),
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
                phoneNumber = phoneNumber + random.Next(0, 10);
            }

            return phoneNumber;
        }

        private Service GetRandomService(Random random)
        {
            List<Service> listServiceAdd = _context.Services.ToList();

            if (listServiceAdd.Count == 0)
            {
                //listServiceAdd = new List<Service>();

                listServiceAdd.Add(new Service()
                {
                    Id = 0,
                    Name = "Accueil"
                });

                listServiceAdd.Add(new Service()
                {
                    Id = 1,
                    Name = "Informatique"
                });

                listServiceAdd.Add(new Service()
                {
                    Id = 2,
                    Name = "Logistique"
                });

                listServiceAdd.Add(new Service()
                {
                    Id = 3,
                    Name = "Comptabilité"
                });

                listServiceAdd.Add(new Service()
                {
                    Id = 4,
                    Name = "Administration"
                });

                _context.Services.AddRange(listServiceAdd);
                _context.SaveChanges();

                Console.WriteLine("Service ajoutés !");
            }


            return listServiceAdd[random.Next(listServiceAdd.Count)];
        }

        private Site GetRandomSite(Random random)
        {
            List<Site> listSiteAdd = _context.Sites.ToList();

            if (listSiteAdd.Count == 0)
            {
                //listSiteAdd = new List<Site>();

                listSiteAdd.Add(new Site()
                {
                    Id = 0,
                    Town = "Paris"
                });

                listSiteAdd.Add(new Site()
                {
                    Id = 1,
                    Town = "Nantes"
                });

                listSiteAdd.Add(new Site()
                {
                    Id = 2,
                    Town = "Toulouse"
                });

                listSiteAdd.Add(new Site()
                {
                    Id = 3,
                    Town = "Nice"
                });

                listSiteAdd.Add(new Site()
                {
                    Id = 4,
                    Town = "Lille"
                });


                _context.Sites.AddRange(listSiteAdd);
                _context.SaveChanges();

                Console.WriteLine("Site ajoutés !");
            }

            return listSiteAdd[random.Next(listSiteAdd.Count)];
        }
    }
}