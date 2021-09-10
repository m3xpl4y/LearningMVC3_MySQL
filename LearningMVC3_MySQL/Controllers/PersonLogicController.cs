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

        public void EditPersonView(ApplicationDbContext context, PersonViewModel personViewModel)
        {
            Person pax = new Person
            {
                Id = personViewModel.Id,
                FirstName = personViewModel.FirstName,
                LastName = personViewModel.LastName
            };
            Adress adress = new Adress
            {
                Id = pax.AdressId,
                Street = personViewModel.Street,
                City = personViewModel.City
            };
            Pet pet = new Pet
            {
                Id = pax.PetId,
                Type = personViewModel.Type,
                Name = personViewModel.Name
            };

            context.Update(adress);
            context.Update(pet);
            context.SaveChanges();


            pax.AdressId = adress.Id;
            pax.PetId = pet.Id;
            context.Update(pax);
            context.SaveChanges();
        }
    }
}
