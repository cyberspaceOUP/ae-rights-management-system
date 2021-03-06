﻿//Create By Saddam on 01/08/2016
using ACS.Core.Domain.PermissionsOutbound;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.PermissionsOutbound
{
  public partial  class PermissionsOutboundTypeOfRightsMap : EntityTypeConfiguration<PermissionsOutboundTypeOfRightsMaster>
    {
      public PermissionsOutboundTypeOfRightsMap()
        {
            this.ToTable("PermissionsOutboundTypeOfRightsMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.PermissionsOutboundId).IsOptional();
            this.Property(a => a.TypeofRightsId).IsOptional();
            this.Property(a => a.Quantity).IsOptional().HasMaxLength(40);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();




            this.HasOptional(usl => usl.PermissionsOutboundMaster)
            .WithMany(s => s.PermissionsOutboundTypeOfRightsMaster)
            .HasForeignKey(usl => usl.PermissionsOutboundId)
            .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.TypeOfRights )
                         .WithMany()
                         .HasForeignKey(usl => usl.TypeofRightsId)
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
