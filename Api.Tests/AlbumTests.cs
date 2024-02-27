using AutoFixture.Xunit2;
using Core;
using Core.Features.Abums;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Api.Tests
{
    public class AlbumTests
    {

        [Fact]
        public async Task CreateAlbum_Should_return_200_Ok()
        {
            // arrange
            var api = new ApiFactory();
            var httpClient = api.CreateClient();
            var title = "Title_" + Guid.NewGuid();

            // act
            var response = await httpClient.PostAsJsonAsync("/api/albums", new
            {
                Title = title,
                ArtistName = "artist",
                Type = 2,
                Stock = 10,
                Cover = ""
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateAlbum_Already_exists_Should_return_409_conflict()
        {
            // arrange
            var api = new ApiFactory();
            var httpClient = api.CreateClient();
            var title = "Title_"+Guid.NewGuid();

            // act
            var response = await httpClient.PostAsJsonAsync("/api/albums", new
            {
                Title = title,
                ArtistName = "Artist",
                Type = 2,
                Stock = 10,
                Cover = ""
            });

            response = await httpClient.PostAsJsonAsync("/api/albums", new
            {
                Title = title,
                ArtistName = "Artist",
                Type = 2,
                Stock = 10,
                Cover = ""
            });

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task UpdateAlbum_Should_return_404_not_found(Guid albumId)
        {
            // arrange
            var api = new ApiFactory();
            var httpClient = api.CreateClient();
            var jsonPatchDoc = new JsonPatchDocument<Album>().Replace(x => x.ArtistName, "New Title");
            var json = JsonConvert.SerializeObject(jsonPatchDoc);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // act
            var response = await httpClient.PatchAsync($"/api/albums/{albumId.ToString()}", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task Should_return_204_nocontent_delete_album(Guid albumId)
        {
            // arrange
            var api = new ApiFactory();
            var httpClient = api.CreateClient();

            // act
            var response = await httpClient.DeleteAsync($"/api/albums/{albumId.ToString()}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Should_return_200_ok_get_albums()
        {
            // arrange
            var api = new ApiFactory();
            var httpClient = api.CreateClient();
            var title = "Title_" + Guid.NewGuid();

            // act
            var response = await httpClient.PostAsJsonAsync("/api/albums", new
            {
                Title = title,
                ArtistName = "artist",
                Type = 2,
                Stock = 10,
                Cover = ""
            });

            response = await httpClient.GetAsync("/api/albums");

            // assert
            var responseString = await response.Content.ReadAsStringAsync();
            var albumResponse = JsonConvert.DeserializeObject<Page<Album>>(responseString);
            Assert.NotEmpty(albumResponse.Entries);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); 

        }
    }
}