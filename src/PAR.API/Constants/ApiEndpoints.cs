namespace PAR.API.Constants;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Account
    {
        public const string Tag = "Account";
        public const string Create = $"{ApiBase}/account";
    }
    
    public static class Authorization
    {
        public const string Tag = "Authorization";
        public const string Login = $"{ApiBase}/authorization/login";
    }
    
    public static class Users
    {
        public const string Tag = "User";
        public const string Get = $"{ApiBase}/users/{{id}}";
        public const string AssignUserToRole = $"{ApiBase}/users/{{userId}}/assign-to-role/{{roleId}}";
        public const string RemoveUserFromRole = $"{ApiBase}/users/{{userId}}/remove-from-role/{{roleId}}";
    }
    
    public static class Roles
    {
        public const string Tag = "Role";
        public const string Get = $"{ApiBase}/roles/{{id}}";
        public const string Create = $"{ApiBase}/roles";
        public const string Update = $"{ApiBase}/roles/{{id}}";
        public const string Delete = $"{ApiBase}/roles/{{id}}";
    }
}