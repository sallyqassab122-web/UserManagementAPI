namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(
                    "Authorization",
                    out var token))
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(
                    "Unauthorized: Token is missing.");

                return;
            }

            if (token != "Bearer TechHiveToken123")
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(
                    "Unauthorized: Invalid token.");

                return;
            }

            await _next(context);
        }
    }
}