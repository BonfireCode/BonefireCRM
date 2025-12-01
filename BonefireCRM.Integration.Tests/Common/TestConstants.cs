using Respawn.Graph;

namespace BonefireCRM.Integration.Tests.Common
{
    internal class TestConstants
    {
        internal const int DATASEED = 1234;

        internal const string USERNAME = "testuser";
        internal const string USEREMAIL = "test@test.com";
        internal const string USERPASSWORD = "test1234";

        internal const string AUTHSCHEMA = "TestScheme";

        internal static readonly Table[] RESPANWTABLESTOIGNORE = ["AspNetUsers", "AspNetRoles", "AspNetUserRoles", "AspNetUserClaims", "AspNetRoleClaims", "AspNetUserLogins", "AspNetUserTokens", "__EFMigrationsHistory"];
    }
}
