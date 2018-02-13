//Create by Saddam on 14/06/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.OtherContract;

namespace ACS.Data.Mapping.OtherContract
{
    public partial class OtherContractMap : EntityTypeConfiguration<OtherContractMaster>
    {
        public OtherContractMap()
          {
              this.ToTable("OtherContractMaster");
              this.HasKey(a => a.Id);


              this.Property(a => a.othercontractcode).IsRequired().HasMaxLength(20);
              this.Property(a => a.partyname).IsRequired().HasMaxLength(200);
              this.Property(a => a.natureofserviceid);
              this.Property(a => a.natureofsubserviceid).IsOptional();
              this.Property(a => a.Address).IsRequired().HasMaxLength(2000);
              this.Property(a => a.CountryId).IsOptional();
              this.Property(a => a.OtherCountry).HasMaxLength(100);
              this.Property(a => a.Stateid).IsOptional();
              this.Property(a => a.OtherState).HasMaxLength(100);
              this.Property(a => a.Cityid).IsOptional();
              this.Property(a => a.OtherCity).HasMaxLength(100);
              this.Property(a => a.Pincode).IsOptional().HasMaxLength(20);
              this.Property(a => a.Mobile).IsOptional().HasMaxLength(25);
              this.Property(a => a.Email).IsOptional().HasMaxLength(100);
              this.Property(a => a.PANNo).IsOptional().HasMaxLength(20);
              this.Property(a => a.Requestdate).IsOptional();
             this.Property(a => a.ProjectTitle).IsOptional().HasMaxLength(100);
              this.Property(a => a.ProjectISBN).IsOptional().HasMaxLength(13);
              this.Property(a => a.Contracttypeid);

              this.Property(a => a.ContractDate).IsOptional().HasMaxLength(25);

              this.Property(a => a.Periodofagreement).IsOptional();

              this.Property(a => a.Expirydate).IsOptional().HasMaxLength(25);

              this.Property(a => a.Territoryrightsid);
              this.Property(a => a.Payment).HasMaxLength(3);
              this.Property(a => a.paymentperiodid).IsOptional();

              this.Property(a => a.NatureOfWork).IsOptional().HasMaxLength(500);
             // this.Property(a => a.divisionid);

              this.Property(a => a.ContractSignedByExecutiveid).IsOptional();
              this.Property(a => a.Remarks).IsOptional().HasMaxLength(500);

              this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
              this.Property(a => a.EnteredBy).IsRequired();
              this.Property(a => a.EntryDate).IsRequired();
              this.Property(a => a.ModifiedBy).IsOptional();
              this.Property(a => a.ModifiedDate).IsOptional();
              this.Property(a => a.DeactivateBy).IsOptional();
              this.Property(a => a.DeactivateDate).IsOptional();

              this.Property(a => a.PaymentAmount).IsOptional().HasMaxLength(200);

              this.Property(a => a.CurrencyMasterId).IsOptional();

              this.HasRequired(a => a.OtherContractCompanyCountry)
                 .WithMany()
                 .HasForeignKey(a => a.CountryId)
                 .WillCascadeOnDelete(false);

              this.HasRequired(a => a.OtherContractCompanyState)
                  .WithMany()
                  .HasForeignKey(a => a.Stateid)
                  .WillCascadeOnDelete(false);

              this.HasRequired(a => a.OtherContractCompanyCity)
                  .WithMany()
                  .HasForeignKey(a => a.Cityid)
                  .WillCascadeOnDelete(false);


              this.HasRequired(a => a.OtherContractContractType)
                 .WithMany()
                 .HasForeignKey(a => a.Contracttypeid)
                 .WillCascadeOnDelete(false);


              this.HasRequired(a => a.OtherContractTerritoryRightsMaster)
               .WithMany()
               .HasForeignKey(a => a.Territoryrightsid)
               .WillCascadeOnDelete(false);


              this.HasOptional(a => a.OtherContractPaymentPeriod)
               .WithMany()
               .HasForeignKey(a => a.paymentperiodid)
               .WillCascadeOnDelete(false);


              //this.HasRequired(a => a.OtherContractDivisionMaster)
              // .WithMany()
              // .HasForeignKey(a => a.divisionid)
              // .WillCascadeOnDelete(false);

              this.HasOptional(a => a.OtherContractExecutiveMaster)
               .WithMany()
               .HasForeignKey(a => a.ContractSignedByExecutiveid)
               .WillCascadeOnDelete(false);

          }
    }
}
