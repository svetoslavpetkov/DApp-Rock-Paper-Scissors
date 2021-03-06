﻿using System;
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

        [HttpGet("open")]
        public IActionResult GetOpen()
        {
            var result = contactService
                .GetAllGames()
                .Where(x=> x.Status == Models.Dto.GameStatus.Started)
                .OrderBy(x=> x.GameID);
            return Ok(result);
        }

        [HttpGet("{gameID:int}")]
        public IActionResult GetByGameId(int gameID)
        {
            var started = contactService.GetCreatedGame(gameID);
            var complted = contactService.GetCompletedByGameID(gameID);
            if (complted != null)
            {
                return Ok(Game.FromCompletedGameData(complted));
            }
            return Ok(Game.FromGameInitated(started));
        }

        [HttpGet("created/{gameID:int}")]
        public IActionResult GetCreatedGameByID(int gameID)
        {
            return Ok(contactService.GetCreatedGame(gameID));
        }
    }
}