using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/game")]
    public class GameController : Controller
    {
        private ContractService contactService;

        public GameController(ContractService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet("all")]
        public IActionResult GetGamesAll()
        {
            var result = contactService.GetAllGames();
            return Ok(result);
        }

        public void GetOpen()
        {

        }
    }
}