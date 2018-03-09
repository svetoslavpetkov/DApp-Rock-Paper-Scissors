using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Dto;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/contract")]
    public class ContractController : Controller
    {
        private ContractService contactService;

        public ContractController(ContractService contactService)
        {
            this.contactService = contactService;
        }


        [HttpGet("metadata")]
        public ContractMetadata Metadata()
        {
            ContractMetadata metadata = contactService.GetMetadata();
            return metadata;
        }
    }
}