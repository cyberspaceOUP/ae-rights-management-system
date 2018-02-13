//Create by Saddam on 02/05/2016
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
   public partial class CopyRightHolderMasterMap :EntityTypeConfiguration< CopyRightHolderMaster>
    {
       public CopyRightHolderMasterMap()
       {
           this.ToTable("CopyRightHolderMaster");
           this.HasKey(a => a.Id);
           this.Property(a => a.CopyRightHolderCode).HasMaxLength(50);
           this.Property(a => a.CopyRightHolderName).IsRequired().HasMaxLength(200);
           this.Property(a => a.ContactPerson).IsRequired().HasMaxLength(200);
           this.Property(a => a.Address).IsRequired().HasMaxLength(2000);
           this.Property(a => a.CountryId);
           this.Property(a => a.OtherCountry).HasMaxLength(100);
           this.Property(a => a.Stateid);
           this.Property(a => a.OtherState).HasMaxLength(100);
           this.Property(a => a.Cityid);
           this.Property(a => a.OtherState).HasMaxLength(100);
           this.Property(a => a.Pincode).HasMaxLength(10);
           this.Property(a => a.Mobile).HasMaxLength(25);
           this.Property(a => a.Email).IsRequired().HasMaxLength(100);
           this.Property(a => a.URL).HasMaxLength(100);
           this.Property(a => a.BankName).HasMaxLength(200);
           this.Property(a => a.AccountNo).HasMaxLength(100);
           this.Property(a => a.BankAddress).HasMaxLength(500);
           this.Property(a => a.IFSCCode).HasMaxLength(100);
           this.Property(a => a.PANNo).HasMaxLength(20);
           this.Property(a => a.VendorCOde).HasMaxLength(50);
           this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
           this.Property(a => a.EnteredBy).IsRequired();
           this.Property(a => a.EntryDate).IsRequired();
           this.Property(a => a.ModifiedBy).IsOptional();
           this.Property(a => a.ModifiedDate).IsOptional();
           this.Property(a => a.DeactivateBy).IsOptional();
           this.Property(a => a.DeactivateDate).IsOptional();



       
       }

    }
}
