﻿//Create By Saddam on 04/08/2016
using ACS.Core.Domain.PermissionsOutbound;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.PermissionsOutbound
{
    

    public partial class PermissionsOutboundDocumentMap : EntityTypeConfiguration<PermissionsOutboundDocument>
    {
        public PermissionsOutboundDocumentMap()
        {
            this.ToTable("PermissionsOutboundDocument");
            this.HasKey(a => a.Id);
            this.Property(a => a.Documentname).IsOptional().HasMaxLength(400);
            this.Property(a => a.DocumentFile).IsOptional().HasMaxLength(400);
            this.Property(a => a.PermissionsOutboundUpdateId).IsOptional();


            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();


            this.HasRequired(usl => usl.PermissionsOutboundUpdate)
               .WithMany(s => s.PermissionsOutboundDocument)
               .HasForeignKey(usl => usl.PermissionsOutboundUpdateId)
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
