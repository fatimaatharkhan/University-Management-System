using Microsoft.AspNetCore.Identity;

namespace Flex.Models
{
    public class IdentityCustomFields: IdentityUser
    {
        public int UserType {  get; set; }  
    }
}
