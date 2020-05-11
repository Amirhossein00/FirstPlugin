using System.Collections.Generic;
using System.Linq;
using Nop.Plugin.Projects.Domain;
using Nop.Plugin.Projects.Models;
using Nop.Services.Media;
using Nop.Services.Seo;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Projects.Services;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Projects.Factory
{
    public class ProjectModelFactory : IProjectModelFactory
    {

        #region Dependencie
        IPictureService _pictureServcie;
        IUrlRecordService _urlRecordService;
        IProjectService _projectService;
        public ProjectModelFactory(IPictureService pictureService,
            IUrlRecordService urlrecordService,
            IProjectService projectService)
        {
            _pictureServcie = pictureService;
            _urlRecordService = urlrecordService;
            _projectService = projectService;
        }
        #endregion

        #region Methods
        public ProjectModel GetModelFromProject(Project project)
        {
            var projectModel = new ProjectModel
            {
                ShortDescription = project.ShortDescription,
                FullDescription = project.FullDescription,
                Id = project.Id,
                Name = project.Name,
                PictureId = project.PictureId,
                PictureUrl = _pictureServcie.GetPictureUrl(project.PictureId),
                Published = project.Published,
                SeName = _urlRecordService.GetSeName(project)
            };

            return projectModel;
        }

        public IList<ProjectModel> GetModeListFromProjectList(List<Project> projectsList)
        {
            var projectModelList = projectsList.Select(p => new ProjectModel
            {
                FullDescription = p.FullDescription,
                Id = p.Id,
                Name = p.Name,
                PictureId = p.PictureId,
                PictureUrl = _pictureServcie.GetPictureUrl(p.PictureId),
                Published = p.Published,
                SeName = _urlRecordService.GetSeName(p),
                ShortDescription = p.ShortDescription
            }).ToList();

            return projectModelList;
        }

        public Project GetProjectFromModel(ProjectModel projectModel)
        {
            var project = new Project
            {
                FullDescription = projectModel.FullDescription,
                Id = projectModel.Id,
                Name = projectModel.Name,
                PictureId = projectModel.PictureId,
                Published = projectModel.Published,
                ShortDescription = projectModel.ShortDescription
            };
            return project;
        }

        public ProjectViewModel GetViewModelFromProject(Project project)
        {
            var viewModel = new ProjectViewModel
            {
                FullDescription=project.FullDescription,
                Name=project.Name,
                PictureUrl=_pictureServcie.GetPictureUrl(_pictureServcie.GetPictureById(project.PictureId)),
                ShortDescription=project.ShortDescription
            };
            return viewModel;
        }

        public ProjectListModel PreaperProjectListModel(ProjectSearchModel projectSearchModel)
        {
            var projects = _projectService.SearchProjects(published: projectSearchModel.PublishId,
                name: projectSearchModel.Name,
                shortDescription: projectSearchModel.ShortDescription);

            var model = new ProjectListModel().PrepareToGrid(projectSearchModel, projects, () =>
                {
                    return projects.Select(p =>
                   {
                       var projectModel = GetModelFromProject(p);

                       projectModel.FullDescription = null;
                       projectModel.SeName = _urlRecordService.GetSeName(p);
                       return projectModel;
                   });
                });
            return model;
        }

        public ProjectSearchModel PreaperProjectSearchModel(ProjectSearchModel searchModel)
        {
            searchModel.Published.Add(new SelectListItem
            {
                Text = "All",
                Value = "0"
            });
            searchModel.Published.Add(new SelectListItem
            {
                Value = "1",
                Text = "PublihsedOnly"
            });
            searchModel.Published.Add(new SelectListItem
            {
                Value = "2",
                Text = "UnPublishedOnly"
            });

            return searchModel;
        }

        #endregion
    }
}
