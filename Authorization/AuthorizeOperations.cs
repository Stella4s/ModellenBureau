using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Authorization
{
    public static class AuthorizeOperations
    {
        public static OperationAuthorizationRequirement Create =
         new OperationAuthorizationRequirement { Name = ConOperations.CreateOperationName };
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = ConOperations.ReadOperationName };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = ConOperations.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = ConOperations.DeleteOperationName };
        public static OperationAuthorizationRequirement Approve =
          new OperationAuthorizationRequirement { Name = ConOperations.ApproveOperationName };
        public static OperationAuthorizationRequirement Reject =
          new OperationAuthorizationRequirement { Name = ConOperations.RejectOperationName };
    }

    public class ConOperations
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";
        public static readonly string RejectOperationName = "Reject";
    }

    public class ConRoles
    {
        public static readonly string AdministratorRole = "Administrator";
        public static readonly string ManagerRole = "Manager";
        public static readonly string PhotoModelRole = "PhotoModel";
        public static readonly string CustomerRole = "Customer";
    }
}
