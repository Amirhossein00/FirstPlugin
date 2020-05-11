using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using Nop.Core.Data;
using Nop.Plugin.Projects.Domain;
using Nop.Core;
using System;

namespace Nop.Plugin.Projects.Services
{

    class ProjectService : IProjectService
    {
        #region Dependecies
        IRepository<Project> _projectRepository;

        public ProjectService(IRepository<Project> projectRpepository)
        {
            _projectRepository = projectRpepository;
        }
        #endregion

        #region Methodes
        public void DeleteProject(Project project)
        {
            var result = GetProjectById(project.Id);
            _projectRepository.Delete(result);
        }

        public void DeleteProject(int projectId)
        {
            var result = GetProjectById(projectId);
            _projectRepository.Delete(result);
        }

        public List<Project> GetAllProjects()
        {
            var allprojects = _projectRepository.Table.ToList();
            return allprojects;
        }

        public IList<Project> GetAllPublicProjects()
        {
            var publicProjects = _projectRepository.Table.Where(p => p.Published == true);
            return publicProjects.ToList();
        }

        public Project GetProjectById(int projectId)
        {
            var result = _projectRepository.GetById(projectId);
            return result;
        }

        public IList<Project> GetProjectsbyIds(int[] projectIds)
        {
            if (projectIds == null)
                throw new ArgumentNullException();

            var query = from q in _projectRepository.Table
                        where projectIds.Contains(q.Id)
                        select q;
            var projectList = query.ToList();
            var sortedProjectList = new List<Project>();
            foreach (var id in projectIds)
            {
                var project = projectList.Find(p => p.Id == id);
                if (project != null)
                    sortedProjectList.Add(project);
            }
            return sortedProjectList;
        }

        public void InsertProject(Project project)
        {
            _projectRepository.Insert(project);
        }

        public bool IsPublic(int projectId)
        {
            var isPublic = GetProjectById(projectId).Published;
            return isPublic;
        }

        public IPagedList<Project> SearchProjects(int published = 0, string name = null, string shortDescription = null)
        {
            var model = _projectRepository.Table;
            IQueryable<Project> query;
            if (!string.IsNullOrEmpty(name))
                model = model.Where(m => m.Name.Contains(name));

            if (!string.IsNullOrEmpty(shortDescription))
                model = model.Where(m => m.ShortDescription.Contains(shortDescription));

            switch (published)
            {
                case 0:
                    query = from q in model
                            select q;
                    break;
                case 1:
                    query = from q in model
                            where q.Published == true
                            select q;
                    break;
                case 2:
                    query = from q in model
                            where q.Published != true
                            select q;
                    break;
                default:
                    query = from q in model
                            select q;
                    break;
            }
            var totalRecords = query.Count();

            return new PagedList<Project>(query.ToList(), 0, int.MaxValue, totalRecords);
        }

        public void UpdateProject(Project project)
        {
            _projectRepository.Update(project);
        }

        public void DeleteProjectByIds(IList<Project> projects)
        {
            if (projects == null)
                throw new ArgumentNullException(nameof(projects));

            foreach (var project in projects)
            {
                _projectRepository.Delete(project);
            }

        }
        #endregion
    }
}
