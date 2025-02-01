using System.Runtime.Serialization;

namespace CBTTechnicalChallenge.Enums
{
    public enum ContactType
    {
        [EnumMember(Value = "Phone Contact")]
        Phone,

        [EnumMember(Value = "Email Contact")]
        Email
    }
}
