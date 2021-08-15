using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using DogApi.Models;

namespace DogApiTest
{
    
    public class DogControllerTests : IClassFixture<WebApplicationFactory<DogApi.Startup>>
    {
        private readonly HttpClient _client;
        static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public DogControllerTests(WebApplicationFactory<DogApi.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task GetBreedListTest()
        {
            var rawResponse = await _client.GetAsync("/dog/breeds");
            rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseJson = await rawResponse.Content.ReadAsStringAsync();
            var breedResponse = JsonSerializer.Deserialize<BreedListResponseModel>(responseJson, jsonSerializerOptions);
            breedResponse.Breeds.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetDogImagesByBreedTest()
        {
            var rawBreedsResponse = await _client.GetAsync("/dog/breeds");
            var responseJson = await rawBreedsResponse.Content.ReadAsStringAsync();
            var breeds = JsonSerializer.Deserialize<BreedListResponseModel>(responseJson, jsonSerializerOptions).Breeds;
            var rawImagesResponse = await _client.GetAsync($"/dog/images?breed={breeds[0].Breed}");
            rawImagesResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var imagesJson = await rawImagesResponse.Content.ReadAsStringAsync();
            var imageResponse = JsonSerializer.Deserialize<DogImagesResponseModel>(imagesJson, jsonSerializerOptions);
            imageResponse.Images.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetDogImagesByBreedAndSubbreedTest()
        {
            var rawBreedsResponse = await _client.GetAsync("/dog/breeds");
            var responseJson = await rawBreedsResponse.Content.ReadAsStringAsync();
            var breeds = JsonSerializer.Deserialize<BreedListResponseModel>(responseJson, jsonSerializerOptions).Breeds;
            bool hasFoundSubbreed = false;
            foreach (var breed in breeds)
            {
                if (breed.Subbreeds.Count > 0)
                {
                    var rawImagesResponse = await _client.GetAsync($"/dog/images?breed={breed.Breed}&subbreed={breed.Subbreeds[0]}");
                    rawImagesResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    var imagesJson = await rawImagesResponse.Content.ReadAsStringAsync();
                    var imageResponse = JsonSerializer.Deserialize<DogImagesResponseModel>(imagesJson, jsonSerializerOptions);
                    imageResponse.Images.Should().HaveCountGreaterThan(0);
                    hasFoundSubbreed = true;
                    break;
                }
            }
            hasFoundSubbreed.Should().BeTrue("No subbreed is found.");
        }

        [Fact]
        public async Task GetDogImagesByWrongSubbreedTest()
        {
            var rawImagesResponse = await _client.GetAsync($"/dog/images?breed=akita&subbreed=kkksubbreed");
            rawImagesResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetDogImagesByWrongBreedTest()
        {
            var rawImagesResponse = await _client.GetAsync($"/dog/images?breed=kkkbreed");
            rawImagesResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetDogImagesWithoutParameterTest()
        {
            var rawImagesResponse = await _client.GetAsync($"/dog/images");
            rawImagesResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
