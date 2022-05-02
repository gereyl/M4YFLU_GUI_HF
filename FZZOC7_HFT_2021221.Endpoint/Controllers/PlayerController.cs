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
    public class PlayerController : ControllerBase
    {
        IPlayerLogic PlayerLogic;
        IHubContext<SignalRHub> hub;


        public PlayerController(IPlayerLogic playerLogic, IHubContext<SignalRHub> hub)
        {
            PlayerLogic = playerLogic;
            this.hub = hub;
        }

        // GET: api/<PlayerController>
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return PlayerLogic.ReadAll();
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public Player Get(int id)
        {
            return PlayerLogic.Read(id);
        }

        // POST api/<PlayerController>
        [HttpPost]
        public void Post([FromBody] Player value)
        {
            PlayerLogic.Create(value);
            this.hub.Clients.All.SendAsync("PlayerCreated",value);
        }

        // PUT api/<PlayerController>/5
        [HttpPut]
        public void Put([FromBody] Player value)
        {
            PlayerLogic.Update(value);
            this.hub.Clients.All.SendAsync("PlayerUpdated", value);
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var playerToDelete = this.PlayerLogic.Read(id);
            PlayerLogic.Delete(id);
            this.hub.Clients.All.SendAsync("PlayerDeleted", playerToDelete);
        }
    }
}
