using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using dog_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace dog_api.Controllers
{
    [Route("dog")]
    public class DogController : Controller
    {
        static readonly HttpClient client = new ();
        [HttpGet("breeds")]
        public async Task<ActionResult> getBreeds()
        {
            try
            {
                string breedsJson = await client.GetStringAsync("https://dog.ceo/api/breeds/list/all");
                var rawBreeds = JsonSerializer.Deserialize<RawBreedModel>(breedsJson).Message;
                // Convert breed format
                var breeds = new List<BreedModel>();
                foreach (var key in rawBreeds.Keys)
                {
                    breeds.Add(new BreedModel
                    {
                        Breed = key,
                        Subbreeds = rawBreeds[key]
                    });
                }
                return Ok(breeds);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
                return BadRequest();
            }
            // TODO: Json Parse Exception
        }
    }
}
