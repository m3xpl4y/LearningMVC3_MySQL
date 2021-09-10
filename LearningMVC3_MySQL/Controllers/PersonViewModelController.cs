using LearningMVC3_MySQL.Data;
using LearningMVC3_MySQL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningMVC3_MySQL.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace LearningMVC3_MySQL.Controllers
{
    //[Authorize] //if uncomment need to login to be able to create and view models
    public class PersonViewModelController : Controller
    {
        private readonly ApplicationDbContext context;

        public PersonViewModelController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pax = await context.Persons
                .Include(x => x.Adress)
                .Include(x => x.Pet)
                .ToListAsync();

            return View(pax);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName, LastName, Street, City, Type, Name")] PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                var test = person; //Debug

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

                return RedirectToAction("Index");
            }

            return View(person);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Person person = context.Persons.Find(id);
            Adress adress = context.Adresses.Find(person.AdressId);
            Pet pet = context.Pets.Find(person.PetId);
            var pvm = new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Street = adress.Street,
                City = adress.City,
                Type = pet.Type,
                Name = pet.Name
            };

            if(person == null)
            {
                return BadRequest();
            }

            return View(pvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,FirstName, LastName, Street, City, Type, Name")] PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {

                var plc = new PersonLogicController();
                plc.EditPersonView(context, personViewModel);

                return RedirectToAction("Index");
            }

            return View(personViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = context.Persons.Find(id);
            var adress = context.Adresses.Find(person.AdressId);
            var pet = context.Pets.Find(person.PetId);
            var personViewModel = new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Street = adress.Street,
                City = adress.City,
                Type = pet.Type,
                Name = pet.Name
            };

            if (person == null)
            {
                return NotFound();
            }
            return View(personViewModel);
        }
        
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Person p = context.Persons.Find(id);
            if(p == null)
            {
                return BadRequest();
            }
            Adress adress = context.Adresses.Find(p.AdressId);
            Pet pet = context.Pets.Find(p.PetId);

            var pvm = new PersonViewModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Street = adress.Street,
                City = adress.City,
                Type = pet.Type,
                Name = pet.Name
            };

            return View(pvm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var pax = context.Persons.Find(id);
            var adress = context.Adresses.Find(pax.AdressId);
            var pet = context.Pets.Find(pax.PetId);

            context.Persons.Remove(pax);
            context.SaveChanges();
            context.Adresses.Remove(adress);
            context.Pets.Remove(pet);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
