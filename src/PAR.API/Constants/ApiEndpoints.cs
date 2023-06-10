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
        public const string Get = $"{ApiBase}/users/{{id:int}}";
        public const string AssignUserToRole = $"{ApiBase}/users/{{userId:int}}/assign-to-role/{{roleId:int}}";
        public const string RemoveUserFromRole = $"{ApiBase}/users/{{userId:int}}/remove-from-role/{{roleId:int}}";
    }
    
    public static class Roles
    {
        public const string Tag = "Role";
        public const string Get = $"{ApiBase}/roles/{{id:int}}";
        public const string Create = $"{ApiBase}/roles";
        public const string Update = $"{ApiBase}/roles/{{id:int}}";
        public const string Delete = $"{ApiBase}/roles/{{id:int}}";
    }

    public static class Groups
    {
        public const string Tag = "Group";
        public const string Create = $"{ApiBase}/groups";
        public const string Get = $"{ApiBase}/groups/{{id:int}}";
        public const string Update = $"{ApiBase}/groups/{{id:int}}";
        public const string Delete = $"{ApiBase}/groups/{{id:int}}";
    }

    public static class SchoolYears
    {
        public const string Tag = "SchoolYear";
        public const string Create = $"{ApiBase}/school-years";
        public const string Get = $"{ApiBase}/school-years/{{id:int}}";
        public const string Update = $"{ApiBase}/school-years/{{id:int}}";
        public const string Delete = $"{ApiBase}/school-years/{{id:int}}";
    }
}