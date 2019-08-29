using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        public Owner CreateOwnerOwner(Owner owner)
        {
            owner.Id = FakeDb.OwnerId;
            FakeDb.Owners.Add(owner);
            return owner;
        }
    }
}