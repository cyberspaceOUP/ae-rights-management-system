using System.Collections.Generic;
using ACS.Core.Domain.Common;
using ACS.Core.Domain.Security;

namespace ACS.Services.Security
{
    /// <summary>
    /// Standard permission provider
    /// </summary>
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //customer portal permissions
        public static readonly PermissionRecord AccessCustomerPortal = new PermissionRecord { Name = "Access customer portal", SystemName = "AccessCustomerPortal", Category = "CustomerPortal" };
        public static readonly PermissionRecord AccessCustomerSubscription = new PermissionRecord { Name = "Access customer subscription", SystemName = "AccessCustomerSubscription", Category = "CustomerPortal" };

        //Common for all Employees
        public static readonly PermissionRecord ChangePassword = new PermissionRecord { Name = "Emp Common. Change Password", SystemName = "ChangePassword", Category = "EmpCommon" };

        //Call Center Admin permissions
        public static readonly PermissionRecord ManageAgents = new PermissionRecord { Name = "CC Admin area. Manage Agents", SystemName = "ManageAgents", Category = "CallCenterAdmin" };
        public static readonly PermissionRecord ManageTicketAssignment = new PermissionRecord { Name = "CC Admin area. Manage Ticket Assignment", SystemName = "ManageTicketAssignment", Category = "CallCenterAdmin" };

        //Call Center Agent permissions
        public static readonly PermissionRecord SearchCustomer = new PermissionRecord { Name = "CC Agent area. Search Customer", SystemName = "SearchCustomer", Category = "CallCenterAgent" };
        public static readonly PermissionRecord ManageTickets = new PermissionRecord { Name = "CC Agent area. Manage Tickets", SystemName = "ManageTickets", Category = "CallCenterAgent" };

        
        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[] 
            {
                AccessCustomerPortal,
                AccessCustomerSubscription,
                ChangePassword,
                ManageAgents,
                ManageTicketAssignment,
                SearchCustomer,
                ManageTickets,
                //AccessAdminPanel,
                //AllowCustomerImpersonation,
            };
        }

        public virtual IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return null;
            //return new[] 
            //{
            //    new DefaultPermissionRecord 
            //    {
            //        RoleSystemName = SystemRoleNames.ARCustomer,
            //        PermissionRecords = new[] 
            //        {
            //            AccessCustomerPortal,
            //            AccessCustomerSubscription,
            //        }
            //    },
            //    new DefaultPermissionRecord 
            //    {
            //        RoleSystemName = SystemRoleNames.ANRCustomer,
            //        PermissionRecords = new[] 
            //        {
            //            AccessCustomerPortal,
            //        }
            //    },
            //    new DefaultPermissionRecord
            //    {
            //        RoleSystemName = SystemRoleNames.Administrator,
            //        PermissionRecords = new[]
            //        {
            //            ChangePassword,
            //        }
            //    },
            //    new DefaultPermissionRecord
            //    {
            //        RoleSystemName = SystemRoleNames.CallCenterAdmin,
            //        PermissionRecords = new[]
            //        {
            //            ManageAgents,
            //            ManageTicketAssignment,
            //            ChangePassword,
            //        }
            //    },
            //    new DefaultPermissionRecord
            //    {
            //        RoleSystemName = SystemRoleNames.CallCenterAgent,
            //        PermissionRecords = new[]
            //        {
            //            SearchCustomer,
            //            ManageTickets,
            //            ChangePassword,
            //        }
            //    },
            //    new DefaultPermissionRecord
            //    {
            //        RoleSystemName = SystemRoleNames.ASP,
            //        PermissionRecords = new[]
            //        {
            //            ChangePassword,
            //        }
            //    },
            //    new DefaultPermissionRecord
            //    {
            //        RoleSystemName = SystemRoleNames.ServiceCenter,
            //        PermissionRecords = new[]
            //        {
            //            ChangePassword,
            //        }
            //    },
            //    new DefaultPermissionRecord
            //    {
            //        RoleSystemName = SystemRoleNames.ISD,
            //        PermissionRecords = new[]
            //        {
            //            ChangePassword,
            //        }
            //    },
            //    new DefaultPermissionRecord
            //    {
            //        RoleSystemName = SystemRoleNames.HonExecutive,
            //        PermissionRecords = new[]
            //        {
            //            ChangePassword,
            //        }
            //    },
            //};
        }
    }
}