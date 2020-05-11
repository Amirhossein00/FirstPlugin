using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Projects.Factory;
using Nop.Plugin.Projects.Services;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Controllers;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Projects.Controllers
{
    public class ProjectController : BasePublicController
    {
        #region Dependencie

        IProjectService _projectService;
        IProjectModelFactory _projectModelFactory;
        IUrlRecordService _urlRecordService;
        IPictureService _pictureService;

        public ProjectController(IProjectService projectService,
        IProjectModelFactory projectModelFactory,
        IPictureService pictureService,
        IUrlRecordService urlRecordService)
        {
            _projectModelFactory = projectModelFactory;
            _projectService = projectService;
            _urlRecordService = urlRecordService;
            _pictureService = pictureService;
        }
        #endregion

        public IActionResult Index()
        {
            var project = _projectService.GetAllPublicProjects();
            var model = _projectModelFactory.GetModeListFromProjectList(project.ToList());

            return View("~/Plugins/Projects/Views/Project/Index.cshtml", model);
        }

        public IActionResult Detail(int id)
        {
            if (id == 0)
                return NotFound();

            var project = _projectService.GetProjectById(id);

            if (project == null || !_projectService.IsPublic(id))
                return NotFound();

            var viewModel = _projectModelFactory.GetViewModelFromProject(project);
            return View("~/Plugins/Projects/Views/Project/Detail.cshtml", viewModel);
        }

    }
}
