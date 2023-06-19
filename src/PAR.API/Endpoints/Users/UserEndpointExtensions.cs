namespace PAR.API.Endpoints.Users;

public static class UserEndpointExtensions
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetUserEndpoint();
        app.MapGetUsersEndpoint();
        app.MapCreateUserEndpoint();
        app.MapUpdateUserEndpoint();
        app.MapCheckEmailExistsEndpoint();
        app.MapAssignUserToRoleEndpoint();
        app.MapRemoveUserFromRoleEndpoint();
        return app;
    }
}