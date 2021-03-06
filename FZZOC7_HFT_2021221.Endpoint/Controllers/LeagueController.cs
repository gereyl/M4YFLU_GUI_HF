using FZZOC7_HFT_2021221.Endpoint.Services;
using FZZOC7_HFT_2021221.Logic;
using FZZOC7_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FZZOC7_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        ILeagueLogic LeagueLogic;
        IHubContext<SignalRHub> hub;

        public LeagueController(ILeagueLogic leagueLogic, IHubContext<SignalRHub> hub)
        {
            LeagueLogic = leagueLogic;
            this.hub = hub;
        }

        // GET: api/<LeagueController>
        [HttpGet]
        public IEnumerable<League> Get()
        {
            return LeagueLogic.ReadAll();
        }

        // GET api/<LeagueController>/5
        [HttpGet("{id}")]
        public League Get(int id)
        {
            return LeagueLogic.Read(id);
        }

        // POST api/<LeagueController>
        [HttpPost]
        public void Post([FromBody] League value)
        {
            LeagueLogic.Create(value);
            this.hub.Clients.All.SendAsync("LeagueCreated", value);
        }

        // PUT api/<LeagueController>/5
        [HttpPut]
        public void Put([FromBody] League value)
        {
            LeagueLogic.Update(value);
            this.hub.Clients.All.SendAsync("LeagueUpdated", value);
        }

        // DELETE api/<LeagueController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var leagueToDelete = this.LeagueLogic.Read(id);
            LeagueLogic.Delete(id);
            this.hub.Clients.All.SendAsync("LeagueDeleted", leagueToDelete);
        }
    }
}
