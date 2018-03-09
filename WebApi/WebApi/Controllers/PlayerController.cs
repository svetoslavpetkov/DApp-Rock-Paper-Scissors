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
    [Route("api/player")]
    public class PlayerController : Controller
    {
        private ContractService contactService;

        public PlayerController(ContractService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet("top")]
        public IActionResult Top()
        {
            return Ok(contactService.GetTopPlayers(10));
        }


        [HttpGet("{address}/games")]
        public IActionResult GetGames(string address)
        {
            return Ok(contactService.GetTopPlayers(10));
        }
    }
}