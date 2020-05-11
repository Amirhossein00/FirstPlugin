using Nop.Core;
using Nop.Core.Domain.Seo;

namespace Nop.Plugin.Projects.Domain
{
    public class Project : BaseEntity, ISlugSupported
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public bool Published { get; set; }

        public int PictureId { get; set; }
    }
}
