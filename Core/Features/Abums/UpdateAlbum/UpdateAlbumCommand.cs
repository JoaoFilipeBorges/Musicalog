using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Abums.UpdateAlbum
{
    public class UpdateAlbumCommand : IRequest
    {
        public Guid Id { get; set; }

        public JsonPatchDocument<Album> Document { get; }

        public UpdateAlbumCommand(Guid id, JsonPatchDocument<Album> document)
        {
            this.Id = id;
            this.Document = document;
        }

    }
}
