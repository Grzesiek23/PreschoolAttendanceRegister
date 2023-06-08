namespace PAR.API.Helpers;

public static class ResultHelper
{
    public static IResult CheckAndReturnResult<T>(T result)
    {
        return result == null ? Results.NotFound() : TypedResults.Ok(result);
    }
}