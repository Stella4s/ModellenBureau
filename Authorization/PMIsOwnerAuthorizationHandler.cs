using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using ModellenBureau.Data;
using ModellenBureau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Authorization
{
    public class PMIsOwnerAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, PhotoModel>
    {
        UserManager<AppUser> _userManager;

        public PMIsOwnerAuthorizationHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   PhotoModel resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != ConOperations.CreateOperationName &&
                requirement.Name != ConOperations.ReadOperationName &&
                requirement.Name != ConOperations.UpdateOperationName &&
                requirement.Name != ConOperations.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.AppUserId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
