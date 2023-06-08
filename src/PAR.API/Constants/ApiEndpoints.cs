namespace PAR.API.Constants;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Account
    {
        public const string Login = $"{ApiBase}/account/login";
        public const string Create = $"{ApiBase}/account";
    }
    
    public static class Users
    {
        public const string Get = $"{ApiBase}/users/{{id}}";
    }
    
    public static class Roles
    {
        public const string Get = $"{ApiBase}/roles/{{id}}";
        public const string Create = $"{ApiBase}/roles";
    }
}