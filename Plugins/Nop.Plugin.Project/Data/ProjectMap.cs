using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Projects.Domain;

namespace Nop.Plugin.Projects.Data
{
    public class ProjectMap: NopEntityTypeConfiguration<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).HasMaxLength(450).IsRequired();

            builder.Property(b => b.ShortDescription).IsRequired().HasMaxLength(450);

            builder.Property(b => b.FullDescription).IsRequired();

        }
    }
}
