using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class ContracotrsRepository : IContractorsRepository
    {
        public ContracotrsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private ApplicationDbContext _context;

      
        public List<Contractor> GetContractors()
        {
            return _context.Contractors.ToList();
        }
    }
}
