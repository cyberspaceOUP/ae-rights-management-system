using ACS.Core.Domain.Asset;
using ACS.Core.Domain.Vehicle;
using System.Data.Entity.ModelConfiguration;

namespace ACS.Data.Mapping.Society
{
    public partial class VehicleDetailMap : EntityTypeConfiguration<VehicleDetail>
    {
        public VehicleDetailMap()
        {
            this.ToTable("VehicleDetail");
            this.HasKey(vd => vd.Id);


            this.Property(vd => vd.VehicalType);
            this.Property(vd => vd.Make).HasMaxLength(50);
            this.Property(vd => vd.Model).HasMaxLength(50);
            this.Property(vd => vd.RegistrationNumber).HasMaxLength(50);
            this.Property(vd => vd.ParkingStickerNumber).HasMaxLength(50).IsOptional();
            this.Property(vd => vd.RFID).HasMaxLength(20).IsOptional();
            this.Property(vd => vd.RFIDValidFrom).IsOptional();
            this.Property(vd => vd.RFIDValidTill).IsOptional();

             
            this.HasRequired(g => g.Flat)
                .WithMany()
                .HasForeignKey(g => g.FlatId)
                .WillCascadeOnDelete(false);

             
            this.HasRequired(g => g.Parking)
                .WithMany(g=>g.VehicleDetails)
                .HasForeignKey(g => g.ParkingId)
                .WillCascadeOnDelete(false);

        }
    }
}
