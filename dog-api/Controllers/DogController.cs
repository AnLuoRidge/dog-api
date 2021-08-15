using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace DogApi.Controllers
{
    [Route("dog")]
    public class DogController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        [HttpGet("breeds")]
        public async Task<ActionResult> GetBreeds()
        {
            try
            {
                string responseJson = await client.GetStringAsync("https://dog.ceo/api/breeds/list/all");
                var response = JsonSerializer.Deserialize<RawBreedModel>(responseJson, jsonSerializerOptions);

                if (response.Status == "success")
                {
                    var rawBreeds = response.Message;
                    // Convert breed format
                    var breedListResult = new BreedListResponseModel();

                    foreach (var key in rawBreeds.Keys)
                    {
                        breedListResult.Breeds.Add(new BreedModel
                        {
                            Breed = key,
                            Subbreeds = rawBreeds[key]
                        });
                    }
                    return Ok(breedListResult);
                }
                else
                {
                    return BadRequest(response.Status);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message :{0} ", ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("images")]
        public async Task<ActionResult> GetDogImageByBreed([FromQuery] string breed, [FromQuery] string subbreed)
        {
            try
            {
                string requestUrl;
                if (subbreed != null)
                {
                    requestUrl = $"https://dog.ceo/api/breed/{breed}/{subbreed}/images";
                }
                else
                {
                    requestUrl = $"https://dog.ceo/api/breed/{breed}/images";
                }
                HttpResponseMessage rawResponse = await client.GetAsync(requestUrl);
                if (rawResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    rawResponse.EnsureSuccessStatusCode();
                    string responseJson = await rawResponse.Content.ReadAsStringAsync();

                    var response = JsonSerializer.Deserialize<RawDogImageListModel>(responseJson, jsonSerializerOptions);

                    if (response.Status == "success")
                    {

                        var dogImagesResult = new DogImagesResponseModel
                        {
                            Images = response.Message
                        };
                        return Ok(dogImagesResult);
                    }
                    else
                    {
                        return BadRequest(response.Status);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message :{0} ", ex.Message);
                return BadRequest("Not found");
            }
        }
    }
}
