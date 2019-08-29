using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {
        Owner CreateOwner(Owner owner);

        IEnumerable<Owner> ReadAllOwners();

        Owner UpdateOwner(string firstName, string lastName, string address, string phoneNr, string email);

        Owner DeleteOwner(Owner owner);

    }
}