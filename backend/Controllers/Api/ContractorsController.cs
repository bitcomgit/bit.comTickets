using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Authorization;

namespace bitcomTickets.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {

        public ContractorsController(IContractorsRepository contractorsReposytory)
        {
            _contractorsReposytory = contractorsReposytory;
        }

        private IContractorsRepository _contractorsReposytory;

        [HttpGet]
        [Authorize(Roles = "employee")]
        public List<Contractor> GetAllContracotrs()
        {
            return _contractorsReposytory.GetContractors();
        }


    }
}
