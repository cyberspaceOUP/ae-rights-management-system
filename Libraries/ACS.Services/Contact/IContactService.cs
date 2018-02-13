using System.Collections.Generic;
using ACS.Core.Domain.Contact;

namespace ACS.Services.Contact
{
    public interface IContactService
    {
        /// <summary>
        /// Get Contact according to contactId
        /// </summary>
        /// <param name="contactId">contactId as int</param>
        /// <returns>Contact</returns>
        ACS.Core.Domain.Master.ExecutiveMaster  GetContactDetailById(int contactId);

        /// <summary>
        /// Get Contact according to EmailId
        /// </summary>
        /// <param name="EmailId">EmailId as int</param>
        /// <returns>Contact</returns>
        ACS.Core.Domain.Master.ExecutiveMaster  GetContactDetailByEmailID(string EmailId);

        /// <summary>
        /// Insert Contact 
        /// </summary>
        /// <param name="contact">Contact class object</param>
        /// <returns></returns>
        void insertContact(ACS.Core.Domain.Master.ExecutiveMaster Executive);

        /// <summary>
        /// Update Contact 
        /// </summary>
        /// <param name="contact">Contact class object</param>
        /// <returns></returns>
        void updateContact(ACS.Core.Domain.Master.ExecutiveMaster Executive);

        /// <summary>
        /// Delete Contact 
        /// </summary>
        /// <param name="contact">Contact class object</param>
        /// <returns></returns>
        void deleteContact(ACS.Core.Domain.Master.ExecutiveMaster Executive);

        

        ContactLoginResults ValidateContactLogin(string email, string password, string ipAddress);

        /// <summary>
        /// Search the flat owner by the flat contact detail and flat number
        /// </summary>
        /// <param name="Contact, FlatNumber">Contact obj, Flat Number</param>
        /// <returns>Flat Contact List</returns>
      
        /// <summary>
        /// Get all Contact of flat according to flatId
        /// </summary>
        /// <param name="flatId">flatId as int</param>
        /// <returns>Contact</returns>
      
        /// <summary>
        /// Delete FlatContactLink data based on ContactId 
        /// </summary>
        /// <param name="flatContact">FlatContactLink class object</param>
        /// <returns></returns>
      
        
        /// <summary>
        /// Get Flat according to flatId
        /// </summary>
        /// <param name="flatId">flatId as int</param>
        /// <returns>Flat</returns>
       

        /// <summary>
        /// Get all pending contact for RWA validation
        /// </summary>
        /// <param name="societyId">societyId as int</param>
        /// <returns>Contact</returns>
      
        /// <summary>
        /// Get FlatContactLink according to contactId and flatId
        /// </summary>
        /// <param name="contactId,flatId">contactId,flatId as int</param>
        /// <returns>FlatContactLink</returns>
      
        /// <summary>
        /// Update FlatContact 
        /// </summary>
        /// <param name="flatContact">FlatContactLink class object</param>
        /// <returns></returns>
        
        /// <summary>
        /// search validated contacts by emailid or mobileno
        /// </summary>
        /// <param name="emailId,mobileNo">emailId as string,mobileNo as string</param>
        /// <returns>Contact</returns>
       /// <summary>
        /// Insert FlatContact 
        /// </summary>
        /// <param name="flatContact">FlatContactLink class object</param>
        /// <returns></returns>
       

        #region Contact roles

        /// <summary>
        /// Delete a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        //void DeleteContactRole(ContactRole customerRole);

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>Customer role</returns>
        //ContactRole GetContactRoleById(int customerRoleId);

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        //ContactRole GetContactRoleBySystemName(string systemName);

        /// <summary>
        /// Gets all customer roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Customer role collection</returns>
        //IList<ContactRole> GetAllContactRoles(bool showHidden = false);

        /// <summary>
        /// Inserts a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        //void InsertContactRole(ContactRole customerRole);

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        //void UpdateContactRole(ContactRole customerRole);

        #endregion
    }
}
