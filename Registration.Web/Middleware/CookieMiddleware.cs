namespace Registration.Web.Middleware
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("regData", out var cookieValue))
            {
                // Разделение значения куки
                var parts = cookieValue.Split('|');
                if (parts.Length == 2)
                {
                    bool isSubscribed = bool.TryParse(parts[1], out bool subscribed) && subscribed;
                    if (subscribed)
                    {
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        await context.Response.WriteAsync("Code activated.");
                        return;
                    }
                }
            }
            await _next(context);
        }
    }
}
