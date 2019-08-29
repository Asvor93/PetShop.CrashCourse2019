using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService.Services
{
    public class OwnerService: IOwnerService
    {
        private IOwnerRepository _ownerRepository;
        public OwnerService(IOwnerRepository ownerRepository)
        {
            this._ownerRepository = ownerRepository;


        }

        public Owner AddOwner(string firstName, string lastName, string address, string phoneNr, string email)
        {
            Owner newOwner = new Owner
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNr,
                Email = email
            };
            return _ownerRepository.CreateOwner(newOwner);
        }

        public List<Owner> ReaOwners()
        {
            throw new System.NotImplementedException();
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public Owner RemoveOwner(Owner ownerToDelete)
        {
            throw new System.NotImplementedException();
        }
    }
}