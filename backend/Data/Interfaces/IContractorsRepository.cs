using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public interface IContractorsRepository
    {
        public List<Contractor> GetContractors();
    }
}
