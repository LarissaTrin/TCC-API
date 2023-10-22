using System.Runtime.Serialization;

namespace Project.Domain.Enum
{
    public enum RoleEnum
    {
        [EnumMember(Value = "0")]
        SuperAdmin,
        
        [EnumMember(Value = "1")]
        Admin,
        
        [EnumMember(Value = "2")]
        Leader,
        
        [EnumMember(Value = "3")]
        User,
    }
}