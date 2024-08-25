namespace Prods.Utilites
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string Customer = "Customer";


        public static bool IsAdmin(HttpContext context)
        {
            var role = context.Session.GetString("Role");
            return role == "Admin";
        }
    }
}
