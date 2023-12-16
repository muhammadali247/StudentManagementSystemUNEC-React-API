namespace StudentManagementSystemUNEC.API.Middleware;

public class JwtCookieMiddleware
{
    private readonly RequestDelegate _next;

    public JwtCookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Read the accessToken and refreshToken cookies
        if (context.Request.Cookies.TryGetValue("accessToken", out string accessToken))
        {
            // Add the accessToken to the Authorization header
            context.Request.Headers.Append("Authorization", $"Bearer {accessToken}");
        }

        if (context.Request.Cookies.TryGetValue("refreshToken", out string refreshToken))
        {
            // Store refreshToken in the HttpContext.Items dictionary for later use
            context.Items["refreshToken"] = refreshToken;
        }


        // Call the next middleware in the pipeline
        await _next(context);
    }
}