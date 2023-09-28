using Flex.Data;
using Flex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Flex
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityCustomFields>
    {
        private ApplicationDbContext _appliationDbContext;
        public MyUserClaimsPrincipalFactory(
        UserManager<IdentityCustomFields> userManager,
        IOptions<IdentityOptions> optionsAccessor, ApplicationDbContext applicationDbContext)
            : base(userManager, optionsAccessor)
        {
            _appliationDbContext = applicationDbContext;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityCustomFields user)
        {
            //get the data from dbcontext
            var Iuser = _appliationDbContext.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();

            var identity = await base.GenerateClaimsAsync(user);
            //Get the data from EF core

            identity.AddClaim(new Claim("UserType", Iuser.UserType.ToString()));
            return identity;
        }
    }
}
