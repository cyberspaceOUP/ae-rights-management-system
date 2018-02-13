using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Contact
{
    public partial class Contact
    {
        public int Id { get; set; }

        public int Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile1 { get; set; }

        public string Mobile2 { get; set; }

        public string ContactNumber { get; set; }

        public string EmailId1 { get; set; }

        public string EmailId2 { get; set; }

        public System.DateTime? DOB { get; set; }

        public System.DateTime? DOA { get; set; }

        public int Gender { get; set; }

        public string PermanentAddress { get; set; }

        public string WorkAddress { get; set; }

        public string PhotoURL { get; set; }

        public string[] Languages { get; set; }

        public string[] Professions { get; set; }

        public string[] Hobbies { get; set; }

        public string[] AreaOfInterests { get; set; }

        public int FlatId { get; set; }

        public int RelationId { get; set; }

        public bool IsLoginRequired { get; set; }

        public int SocietyId { get; set; }

        public string ValidationRemarks { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }

        public string FullName { get; set; }

        public int PrimaryContactId { get; set; }



    }
}
