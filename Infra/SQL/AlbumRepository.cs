using Core;
using Core.Exceptions;
using Core.Features.Abums;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using static Infra.SQL.SqlErrorCodes;

namespace Infra
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IConfiguration configuration;

        public AlbumRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            using IDbConnection connection = new SqlConnection(this.configuration.GetConnectionString("Default"));

            var query = "DELETE FROM Album WHERE clientid = @clientid";
            await connection.ExecuteAsync(query, new { clientid = id });
        }

        public async Task Save(Album album, CancellationToken cancellationToken = default)
        {        
            try
            {
                using IDbConnection connection = new SqlConnection(this.configuration.GetConnectionString("Default"));

                var query = "INSERT INTO Album (clientid, title, artistName, type, stock, cover) values (@clientid, @title, @artistName, @type, @stock, @cover)";

                var rowsAffected = await connection.ExecuteAsync(query, new { clientid = album.Id, title = album.Title, artistName = album.ArtistName, type = album.Type.ToString(), stock = album.Stock, cover = album.Cover });
            }
            catch (SqlException ex)
            {
                if(ex.Number == UNIQUE_VIOLATION)
                {
                    throw new ResourceAlreadyExistsException($"Resource Already Exists.");
                }

                throw;
            }
        }

        public async Task Update(Album album, CancellationToken cancellationToken = default)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(this.configuration.GetConnectionString("Default"));

                var query = "UPDATE Album SET title = @title, artistName = @artistName, type = @type, stock = @stock, cover = @cover WHERE clientid = @clientid";
                var rowsAffected = await connection.ExecuteAsync(query, new { clientid = album.Id, title = album.Title, artistName = album.ArtistName, type = album.Type.ToString(), stock = album.Stock, cover = album.Cover });
            }
            catch (SqlException ex)
            {
                if (ex.Number == UNIQUE_VIOLATION)
                {
                    throw new ResourceAlreadyExistsException($"Resource Already Exists.");
                }

                throw;
            }       
        }

        public async Task<Page<Album>> Search(int pageNumber, int pageSize, string title, string artistName, CancellationToken cancellationToken = default)
        {
            using IDbConnection connection = new SqlConnection(this.configuration.GetConnectionString("Default"));
            
            var parameters = new DynamicParameters();
            parameters.Add("@title", title);
            parameters.Add("@artistName", artistName);
            var totalRows = (await connection.QueryAsync<int>("GetAlbumsCount", parameters, commandType: CommandType.StoredProcedure)).First();
            var totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var query = @$"GetAlbumsPage";
            parameters.Add("pageSz", pageSize);
            parameters.Add("pageNum", pageNumber);
            var albums = await connection.QueryAsync<Album>(query, parameters, commandType: CommandType.StoredProcedure);
            
            return new Page<Album>(pageNumber, albums.Count(), totalPages, albums);
        }

        public async Task<Album> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            using IDbConnection connection = new SqlConnection(this.configuration.GetConnectionString("Default"));

            var query = @$"SELECT clientId as id, title, artistName, type, stock
                           FROM Album
                           WHERE clientid = @clientid";


            var album = (await connection.QueryAsync<Album>(query, new { clientid = id })).FirstOrDefault();

            return album;
        }
    }
}
