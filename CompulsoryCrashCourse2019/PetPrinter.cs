using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Services;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using PetShop.Infrastructure.Data.Repositories;

namespace PetShop.Console2019
{
    public class PetPrinter : IPrinter
    {
        private IPetService service;
        private IOwnerService oService;
        public PetPrinter(IPetService petService, IOwnerService ownerService)
        {
            service = petService;
            oService = ownerService;
        }

        public void MakeMenu()
        {
            int choice = 0;

            while (choice != 14)
            {
                MenuChoices();
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a number!!");

                }

                switch (choice)
                {
                    case 1:
                        CreatePet();
                        break;
                    case 2:
                        ReadPet();
                        break;
                    case 3:
                        DeletePet();
                        break;
                    case 4:
                        UpdatePet();
                        break;
                    case 5:
                        PrintPets();
                        break;
                    case 6:
                        GetPetByName();
                        break;
                    case 7:
                        PrintPetByType();
                        break;
                    case 8:
                        SortByPrice();
                        break;
                    case 9:
                        FiveCheapPets();
                        break;
                    case 10:
                        PrintOwners();
                        break;
                    case 11:
                        NewOwner();
                        break;
                    case 12:
                        UpdateOwner();
                        break;
                    case 13:
                        DeleteOwner();
                        break;
                    default:
                        ExitMenu();
                        break;
                }

                Console.ReadLine();
            }
        }

        

        public void PrintPets()
        {
           Console.WriteLine("Printing all pets!\n");


           foreach (var pet in service.GetPets())
           {
               Console.WriteLine($"Name: {pet.Name}, Type: {pet.PetType}, Birthday: {pet.BirthDate}, Color: {pet.Color}, " +
                                 $"Previous owner: {pet.PreviousOwner} Price: {pet.Price}, Sold date: {pet.SoldDate}\n");

           }
        }

        public void MenuChoices()
        {
            Console.Clear();

            Console.WriteLine("1. Create pet");
            Console.WriteLine("2. Read pet");
            Console.WriteLine("3. Delete pet");
            Console.WriteLine("4. Update pet");
            Console.WriteLine("5. List all pets");
            Console.WriteLine("6. Get pet by name");
            Console.WriteLine("7. Get pet by type");
            Console.WriteLine("8. Sort pets by price");
            Console.WriteLine("9. Show the five cheapest pets:");
            Console.WriteLine("10. Print owners");
            Console.WriteLine("11. Create new owner");
            Console.WriteLine("12. Update owner");
            Console.WriteLine("13. Delete owner");
            Console.WriteLine("14. Exit");
            Console.WriteLine("\nChoose a menu item!");
        }

        public void ReadPet()
        {
            var id = PrintPetById();
            var petToRead = service.FindPetById(id);

            if (petToRead != null)
            {
                if (!service.ValidateId(petToRead.Id, id))
                {
                    Console.WriteLine($"The pet to read: Id {petToRead.Id} name: {petToRead.Name} Type: {petToRead.PetType}, Birthday: {petToRead.BirthDate}, Color: {petToRead.Color}, " +
                                      $"Previous owner: {petToRead.PreviousOwner} Price: {petToRead.Price}, Sold date: {petToRead.SoldDate}");
                }
            }
            else
            {
                Console.WriteLine("Not a valid id!");
            }
        }

        public void CreatePet()
        {
            Console.WriteLine("Enter the name of the pet: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the type of pet: ");
            string type = Console.ReadLine();

            Console.WriteLine("Enter the birthdate of the pet: ");
            DateTime bdate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Enter the date when the pet was sold: ");
            DateTime soldDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Enter the color of the pet: ");
            string color = Console.ReadLine();

            Console.WriteLine("Enter the name of the previous owner: ");
            string prevOwner = Console.ReadLine();

            Console.WriteLine("Enter the price of the pet");
            double price = Convert.ToDouble(Console.ReadLine());

            service.AddPet(name, type, bdate, soldDate, color, prevOwner, price);
        }

        public void DeletePet()
        {
            var id = PrintPetById();
            var petFound = service.FindPetById(id);
            service.Delete(petFound);
        }

        public void UpdatePet()
        {
            var id = PrintPetById();
            var petToUpdate = service.FindPetById(id);

            Console.WriteLine($"The pet to edit: Id {petToUpdate.Id} name: {petToUpdate.Name} Type: {petToUpdate.PetType}, Birthday: {petToUpdate.BirthDate}, Color: {petToUpdate.Color}, " +
                              $"Previous owner: {petToUpdate.PreviousOwner} Price: {petToUpdate.Price}, Sold date: {petToUpdate.SoldDate}\n");

            Console.WriteLine("Enter the name of the pet: ");
            petToUpdate.Name = Console.ReadLine();

            Console.WriteLine("Enter the type of pet: ");
            petToUpdate.PetType = Console.ReadLine();

            Console.WriteLine("Enter the birthdate of the pet: ");
            petToUpdate.BirthDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Enter the date when the pet was sold: ");
            petToUpdate.SoldDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Enter the color of the pet: ");
            petToUpdate.Color = Console.ReadLine();

            Console.WriteLine("Enter the name of the previous owner: ");
            petToUpdate.PreviousOwner = Console.ReadLine();

            Console.WriteLine("Enter the price of the pet");
            petToUpdate.Price = Convert.ToDouble(Console.ReadLine());

        }

        public void GetPetByName()
        {
            Console.WriteLine("Enter pet name: ");
            var petName = Console.ReadLine();
            var petToGet = service.FindPetByName(petName);

            if (petToGet != null)
            {
                Console.WriteLine($"The pet you are looking for: Id {petToGet.Id} name: {petToGet.Name} Type: {petToGet.PetType}, Birthday: {petToGet.BirthDate}, Color: {petToGet.Color}, " +
                                  $"Previous owner: {petToGet.PreviousOwner} Price: {petToGet.Price}, Sold date: {petToGet.SoldDate}\n");

            }
            else
            {
                Console.WriteLine("Could not find a match");
            }
        }

        public void PrintPetByType()
        {
            Console.WriteLine("Enter the pet type: ");
            string petType = Console.ReadLine();

            foreach (var pet in service.GetPets())
            {
                if (petType != null && pet.PetType.ToLower().Equals(petType.ToLower()))
                {
                    Console.WriteLine($"The type of pet you are looking for: Id {pet.Id} name: {pet.Name} Type: {pet.PetType}, Birthday: {pet.BirthDate}, Color: {pet.Color}, " +
                                      $"Previous owner: {pet.PreviousOwner} Price: {pet.Price}, Sold date: {pet.SoldDate}\n");
                }

            }
            Console.WriteLine("Not a valid type!");

        }

        public void SortByPrice()
        {
            Console.WriteLine("Ordered by price!");
            service.OrderByPrice();
        }

        public void FiveCheapPets()
        {
            Console.WriteLine("Five cheap pets");

            var fivePets = service.GetPets().OrderBy(i => i.Price).Take(5);

            foreach (var pet in fivePets)
            {
                Console.WriteLine($"The five cheapest pets are: Id {pet.Id} name: {pet.Name} Type: {pet.PetType}, Birthday: {pet.BirthDate}, Color: {pet.Color}, " +
                                  $"Previous owner: {pet.PreviousOwner} Price: {pet.Price}, Sold date: {pet.SoldDate}\n");
            }

        }


        public int PrintPetById()
        {
            Console.WriteLine("Enter pet id: ");

            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please enter a valid id!");
            }

            return id;
        }

        public void PrintOwners()
        {
            Console.WriteLine("Printing all owners!\n");

            foreach (var owner in oService.ReaOwners())
            {
                Console.WriteLine($"Id : {owner.Id}, Firstname: {owner.FirstName}, Lastname: {owner.LastName}, Address: {owner.Address}, " +
                                  $"Phone number: {owner.PhoneNumber}, Email: {owner.Email}");
            }
        }

        public void NewOwner()
        {
            Console.WriteLine("Enter firstname: ");
            var firstName = Console.ReadLine();

            Console.WriteLine("Enter lastname: ");
            var lastName = Console.ReadLine();

            Console.WriteLine("Enter Address: ");
            var address = Console.ReadLine();

            Console.WriteLine("Enter phone number: ");
            var phoneNr = Console.ReadLine();

            Console.WriteLine("Enter email: ");
            var email = Console.ReadLine();

            oService.AddOwner(firstName, lastName, address, phoneNr, email);
        }

        public void UpdateOwner()
        {
            var id = PrintOwnerById();
            var ownerToUpdate = oService.FindOwnerById(id);

            Console.WriteLine($"Firstname: {ownerToUpdate.FirstName}, Lastname: {ownerToUpdate.LastName}, Address: {ownerToUpdate.Address}, " +
                              $"Phone number: {ownerToUpdate.PhoneNumber}, Email: {ownerToUpdate.Email}");

            Console.WriteLine("Enter firstname: ");
            ownerToUpdate.FirstName = Console.ReadLine();

            Console.WriteLine("Enter lastname: ");
            ownerToUpdate.LastName = Console.ReadLine();

            Console.WriteLine("Enter address: ");
            ownerToUpdate.Address = Console.ReadLine();

            Console.WriteLine("Enter phone number: ");
            ownerToUpdate.PhoneNumber = Console.ReadLine();

            Console.WriteLine("Enter email: ");
            ownerToUpdate.Email = Console.ReadLine();
        }

        public void DeleteOwner()
        {
            var id = PrintOwnerById();
            var ownerFound = oService.FindOwnerById(id);
            oService.RemoveOwner(ownerFound);
        }

        public int PrintOwnerById()
        {
            Console.WriteLine("Enter owner id: ");

            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please enter a valid id!");
            }

            return id;
        }

        public void ExitMenu()
        {
            Console.WriteLine("Exiting Program!");
        }
    }
}