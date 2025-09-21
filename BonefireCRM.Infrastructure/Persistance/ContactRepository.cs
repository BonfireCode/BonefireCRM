using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class ContactRepository : BaseRepository<Contact>, IBaseRepository<Contact>
    {
        private readonly CRMContext _context;

        public ContactRepository(CRMContext context) : base(context)
        {
            _context = context;
        }

        // add specific methods if needed otherwise remove this class
    }
}
