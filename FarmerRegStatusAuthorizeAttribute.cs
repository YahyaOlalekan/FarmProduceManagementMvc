using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmProduceManagement.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace FarmProduceManagement
{
    public class FarmerRegStatusAuthorizeAttribute : AuthorizeAttribute
    {
        private FarmerRegStatus _allowedStatus;

        public FarmerRegStatusAuthorizeAttribute(FarmerRegStatus allowedStatus)
        {
            _allowedStatus = allowedStatus;
        }

        // protected override bool AuthorizeCore(HttpContextBase httpContext)
        // {
        //     var user = httpContext.User;
        //     if(!user.Identity.IsAuthenticated)
        //     return false;

        //     FarmerRegStatus farmerRegStatus = GetUserStatus(user.Identity.Name);
        //     return farmerRegStatus == _allowedStatus;

        // }

        // private FarmerRegStatus GetUserStatus(string username)
        // {
            
        // }
    
    
   
   
   
   
   
   
   
   
   
   
    // public class FarmerRegStatusAuthorizeAttribute : AuthorizeAttribute
    // {
    //     private FarmerRegStatus _allowedStatus;

    //     public FarmerRegStatusAuthorizeAttribute(FarmerRegStatus allowedStatus)
    //     {
    //         _allowedStatus = allowedStatus;
    //     }

    //     protected override bool AuthorizeCore(HttpContextBase httpContext)
    //     {
    //         var user = httpContext.User;
    //         if(!user.Identity.IsAuthenticated)
    //         return false;

    //         FarmerRegStatus farmerRegStatus = GetUserStatus(user.Identity.Name);
    //         return farmerRegStatus == _allowedStatus;

    //     }

    //     private FarmerRegStatus GetUserStatus(string username)
    //     {

    //     }

    }
}