using LearningMVC3_MySQL.Data;
using LearningMVC3_MySQL.Models;
using LearningMVC3_MySQL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Controllers
{
    public class PersonLogicController
    {
        private readonly ApplicationDbContext context;
        private readonly PersonViewModel personViewModel;

        public PersonLogicController()
        {

        }
        public void CreatePersonView(ApplicationDbContext context, PersonViewModel person)
        {

            Person pax = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName
            };
            Adress adress = new Adress
            {
                Street = person.Street,
                City = person.City
            };
            Pet pet = new Pet
            {
                Type = person.Type,
                Name = person.Name
            };

            context.Add(adress);
            context.Add(pet);
            context.SaveChanges();

            pax.AdressId = adress.Id;
            pax.PetId = pet.Id;
            context.Add(pax);
            context.SaveChanges();

        }

    }
}
