using System.ComponentModel.DataAnnotations;
using Nop.Core.Domain.Seo;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Projects.Models
{
    public class ProjectModel:BaseNopEntityModel,ISlugSupported
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public bool Published { get; set; }

        [UIHint("Picture")]
        public int PictureId { get; set; }

        public string SeName { get; set; }

        public string PictureUrl { get; set; }
    }
}
