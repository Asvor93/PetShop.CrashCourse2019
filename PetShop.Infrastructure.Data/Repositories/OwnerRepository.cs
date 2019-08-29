using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {

        public Owner CreateOwner(Owner owner)
        {
            owner.Id = FakeDb.OwnerId;
            FakeDb.Owners.Add(owner);
            return owner;
        }

        public IEnumerable<Owner> ReadAllOwners()
        {
            throw new System.NotImplementedException();
        }

        public Owner UpdateOwner(string firstName, string lastName, string address, string phoneNr, string email)
        {
            throw new System.NotImplementedException();
        }

        public Owner DeleteOwner(Owner owner)
        {
            throw new System.NotImplementedException();
        }
    }
}