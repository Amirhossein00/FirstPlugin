using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Projects.Models
{
    public class ProjectSearchModel : BaseSearchModel
    {
        #region Ctor
        public ProjectSearchModel()
        {
            Published = new List<SelectListItem>();
        }
        #endregion
        public string Name { get; set; }

        public int PublishId { get; set; }

        public string ShortDescription { get; set; }

        public IList<SelectListItem> Published { get; set; }
    }
}
