using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Projects.Factory;
using Nop.Plugin.Projects.Models;
using Nop.Plugin.Projects.Services;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Controllers;

namespace Nop.Plugin.Projects.Controllers
{
    public class ProjectAdminController : BaseAdminController
    {
        #region Dependenci

        IProjectModelFactory _projectModelFactory;
        IProjectService _projectService;
        IUrlRecordService _urlRecordService;
        IPictureService _pictureService;

        public ProjectAdminController(IProjectModelFactory projectModelFactory,
        IProjectService projectservice,
        IUrlRecordService urlRecordsService,
        IPictureService pictureservice)
        {
            _projectService = projectservice;
            _projectModelFactory = projectModelFactory;
            _pictureService = pictureservice;
            _urlRecordService = urlRecordsService;
        }
        #endregion


        public IActionResult Index()
        {
            var model = _projectModelFactory.PreaperProjectSearchModel(new ProjectSearchModel());
            return View("~/Plugins/Projects/Views/ProjectAdmin/Index.cshtml", model);
        }

        public IActionResult ProjectList(ProjectSearchModel projectSearchModel)
        {
            var model = _projectModelFactory.PreaperProjectListModel(projectSearchModel);
            return Json(model);
        }

        public IActionResult List()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create() => View("~/Plugins/Projects/Views/ProjectAdmin/Create.cshtml");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Name,ShortDescription,FullDescription,Published,PictureId,")]ProjectModel projectModel)
        {
            if (ModelState.IsValid)
            {
                var project = _projectModelFactory.GetProjectFromModel(projectModel);

                try
                {
                    _projectService.InsertProject(project);
                    var seName = _urlRecordService.ValidateSeName(project, projectModel.SeName, project.Name, true);
                    _urlRecordService.SaveSlug(project, seName, 0);
                    return await Task.FromResult((IActionResult)RedirectToAction(nameof(Index)));
                }
                catch
                {
                    ModelState.AddModelError("", "Model is not valid");
                }
            }
            return View("~/Plugins/Projects/Views/ProjectAdmin/Create.cshtml", projectModel);
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            var project = _projectService.GetProjectById(id);
            if (project == null)
                return NotFound();
            var model = _projectModelFactory.GetModelFromProject(project);
            return View("~/Plugins/Projects/Views/ProjectAdmin/Edit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "Id,Name,ShortDescription,FullDescription,Published,PictureId")]ProjectModel projectModel)
        {
            if (ModelState.IsValid)
            {
                var project = _projectModelFactory.GetProjectFromModel(projectModel);
                if (project != null)
                {
                    try
                    {
                        _pictureService.UpdatePicture(_pictureService.GetPictureById(project.PictureId));
                        _urlRecordService.SaveSlug(project, _urlRecordService.ValidateSeName(project, projectModel.SeName, project.Name, true), 0);
                        _projectService.UpdateProject(project);
                        return await Task.FromResult((IActionResult)RedirectToAction(nameof(Index)));
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Model is not valid");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The model was not found may be its delete or modified by another user");
                }
            }
            return View("~/Plugins/Projects/Views/ProjectAdmin/Edit.cshtml", projectModel);
        }

        [HttpPost]
        public IActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (selectedIds != null)
            {
                _projectService.DeleteProjectByIds(_projectService.GetProjectsbyIds(selectedIds.ToArray()));
                _urlRecordService.DeleteUrlRecords(_urlRecordService.GetUrlRecordsByIds(selectedIds.ToArray()));
            }
            return Json(new { Result = true });
        }

    }
}
