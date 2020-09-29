using Microsoft.AspNetCore.Authorization;

namespace AuthenticationApi.AuthorizationPolicies
{
    public class UserAuthorizationRequirement : IAuthorizationRequirement
    {
        public bool DevelopmentMode { get; set; }

        public UserAuthorizationRequirement(bool developmentMode)
        {
            DevelopmentMode = developmentMode;
        }
    }
}
