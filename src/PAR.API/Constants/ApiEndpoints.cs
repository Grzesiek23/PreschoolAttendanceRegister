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
        public const string GetAll = $"{ApiBase}/users";
        public const string GetAllAsOptionList = $"{ApiBase}/users/options";
        public const string Create = $"{ApiBase}/users";
        public const string Update = $"{ApiBase}/users/{{id:int}}";
        public const string Exists = $"{ApiBase}/users/exists/{{email}}";
        public const string AssignUserToRole = $"{ApiBase}/users/{{userId:int}}/assign-to-role/{{roleId:int}}";
        public const string RemoveUserFromRole = $"{ApiBase}/users/{{userId:int}}/remove-from-role/{{roleId:int}}";
    }
    
    public static class Roles
    {
        public const string Tag = "Role";
        public const string Get = $"{ApiBase}/roles/{{id:int}}";
        public const string GetAll = $"{ApiBase}/roles";
        public const string Create = $"{ApiBase}/roles";
        public const string Update = $"{ApiBase}/roles/{{id:int}}";
        public const string Delete = $"{ApiBase}/roles/{{id:int}}";
    }

    public static class Groups
    {
        public const string Tag = "Group";
        public const string Create = $"{ApiBase}/groups";
        public const string Get = $"{ApiBase}/groups/{{id:int}}";
        public const string GetAll = $"{ApiBase}/groups";
        public const string GetAllWithDetails = $"{ApiBase}/groups/details";
        public const string GetAllAsOptionList = $"{ApiBase}/groups/options";
        public const string Update = $"{ApiBase}/groups/{{id:int}}";
        public const string Delete = $"{ApiBase}/groups/{{id:int}}";
    }

    public static class SchoolYears
    {
        public const string Tag = "SchoolYear";
        public const string Create = $"{ApiBase}/school-years";
        public const string Get = $"{ApiBase}/school-years/{{id:int}}";
        public const string GetAll = $"{ApiBase}/school-years";
        public const string GetAllAsOptionList = $"{ApiBase}/school-years/options";
        public const string Update = $"{ApiBase}/school-years/{{id:int}}";
        public const string Delete = $"{ApiBase}/school-years/{{id:int}}";
    }

    public static class Preschoolers
    {
        public const string Tag = "Preschooler";
        public const string Create = $"{ApiBase}/preschoolers";
        public const string Get = $"{ApiBase}/preschoolers/{{id:int}}";
        public const string GetAll = $"{ApiBase}/preschoolers";
        public const string Update = $"{ApiBase}/preschoolers/{{id:int}}";
        public const string Delete = $"{ApiBase}/preschoolers/{{id:int}}";
    }
}