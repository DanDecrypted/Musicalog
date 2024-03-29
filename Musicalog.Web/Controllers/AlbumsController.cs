﻿using Musicalog.Web.Adapters;
using Musicalog.Web.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Musicalog.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly ICreateAlbumModelAdapter createModelAdapter;
        private readonly IEditAlbumModelAdapter editModelAdapter;

        public AlbumsController(
            IAlbumService albumService,
            ICreateAlbumModelAdapter createModelAdapter,
            IEditAlbumModelAdapter editModelAdapter
        )
        {
            this.albumService = albumService;
            this.createModelAdapter = createModelAdapter;
            this.editModelAdapter = editModelAdapter;
        }

        [HttpGet]
        public async Task<ActionResult> List(AlbumListRequestModel request)
        {
            request.Page = request.Page < 1 ? 1 : request.Page;
            request.PageSize = request.PageSize < 1 ? AppSettings.PageSize : request.PageSize;
            var model = await albumService.ListAsync(request);
            model.SuccessMessage = request.SuccessMessage;
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            return View(await albumService.GetDetailsAsync(id.Value));
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var serviceModel = await albumService.GetDefaultCreateModelAsync();
            var model = createModelAdapter.FromService(serviceModel);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Models.CreateAlbumRequestModel model)
        {
            var result = await albumService.CreateAsync(createModelAdapter.ToService(model));

            return RedirectToAction("Details", new { Id = result.EntityId });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await albumService.DeleteAsync(id);

            return RedirectToAction("List", new AlbumListRequestModel()
            {
                SuccessMessage = result.Message
            });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var editModel = await albumService.EditDetailsAsync(new EditModelDetailsRequest() { Id = id });
            return View(editModelAdapter.FromService(editModel));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Models.EditAlbumRequestModel model)
        {
            var result = await albumService.EditAsync(editModelAdapter.ToService(model));

            return RedirectToAction("Details", new { model.Id });
        }
    }
}