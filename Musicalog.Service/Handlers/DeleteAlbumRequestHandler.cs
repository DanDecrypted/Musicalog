﻿using Musicalog.Data.Repositories.Interfaces;
using Musicalog.Service.Handlers.Interfaces;
using Musicalog.Service.Models;
using System.Threading.Tasks;

namespace Musicalog.Service.Handlers
{
    public class DeleteAlbumRequestHandler : IDeleteAlbumRequestHandler
    {
        private IAlbumsRepository repository;

        public DeleteAlbumRequestHandler(
            IAlbumsRepository repository
        )
        {
            this.repository = repository;
        }

        public Task<ActionResultModel> Handle(DeleteAlbumRequestModel request)
        {
            var album = repository.GetById(request.Id);

            var artist = album.Artist.Name;
            var albumName = album.Name;

            repository.RemoveAsync(request.Id);

            return Task.FromResult(new ActionResultModel()
            {
                Message = $"Successfully deleted {artist}'s Album \"{albumName}\""
            });
        }
    }
}