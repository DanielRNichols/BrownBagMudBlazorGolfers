﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MudBlazorGolfers.Shared;
using System.Text.Json;

namespace MudBlazorGolfers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldRankingsController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly IMapper _mapper;

        public WorldRankingsController(IHostEnvironment env, IMapper mapper)
        {
            _env = env;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IList<WorldRanking>>> GetAll()
        {
            string worldRankingsJsonString = System.IO.File.ReadAllText(_env.ContentRootPath + "/data/worldrankings.json");

            var worldRankingsJson = JsonSerializer.Deserialize<IList<WorldRankingJson>>(worldRankingsJsonString)?.ToList();
            var worldRankings = _mapper.Map<IList<WorldRanking>>(worldRankingsJson);

            var response = await Task.FromResult(worldRankings);

            return Ok(response);
        }

    }
}
