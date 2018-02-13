//Create By Saddam on 23/08/2016
using ACS.Core.Domain.RightsSelling;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.RightsSelling
{
   public partial class RightsSellingLanguageMasterMap : EntityTypeConfiguration<RightsSellingLanguageMaster>
    {
       public RightsSellingLanguageMasterMap()
       {
           this.ToTable("RightsSellingLanguageMaster");
           this.HasKey(a => a.Id);
           this.Property(a => a.RightsSellingId).IsOptional();
           this.Property(a => a.languageId).IsOptional();
          
           this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
           this.Property(a => a.EntryDate).IsRequired();
           this.Property(a => a.ModifiedDate).IsOptional();
           this.Property(a => a.DeactivateBy).IsOptional();
           this.Property(a => a.DeactivateDate).IsOptional();


           this.HasOptional(usl => usl.RightsSellingMaster)
              .WithMany(s => s.RightsSellingLanguageMaster)
              .HasForeignKey(usl => usl.RightsSellingId)
                .WillCascadeOnDelete(false);

           this.HasOptional(a => a.LanguageMaster)
               .WithMany()
               .HasForeignKey(a => a.languageId)
               .WillCascadeOnDelete(false);

           this.HasRequired(usl => usl.EnteredByForeignKey)
                     .WithMany()
                     .HasForeignKey(usl => usl.EnteredBy)
                       .WillCascadeOnDelete(false);

           this.HasOptional(usl => usl.DeactivateByForeignKey)
                       .WithMany()
                       .HasForeignKey(usl => usl.DeactivateBy)
                         .WillCascadeOnDelete(false);

           this.HasOptional(usl => usl.ModifiedByForeignKey)
                       .WithMany()
                       .HasForeignKey(usl => usl.ModifiedBy)
                         .WillCascadeOnDelete(false);
       }
    }
}
