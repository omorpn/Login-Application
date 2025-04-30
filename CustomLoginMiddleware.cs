using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Login_Application
{
    public class CustomLoginMiddleware
    {
        public readonly RequestDelegate next;
        public CustomLoginMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Path == "/" && context.Request.Method == "POST")
            {
                //Read response from bodystring
                using var reader = new StreamReader(context.Request.Body);

                var body = await reader.ReadToEndAsync();

                Dictionary<string, StringValues> querydict = QueryHelpers.ParseQuery(body);

                StringValues email, password;
                List<string> errors = [];

                if (!(querydict.TryGetValue("email", out email)))
                {
                    errors.Add("Invalid input for 'email'");

                }


                if (!(querydict.TryGetValue("password", out password)))
                {
                    errors.Add("Invalid input for 'password'");

                }


                if (errors.Count > 0)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(string.Join("\n", errors));
                    return;
                }

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    string validEmail = "admin@example.com", validPassword = "admin1234";
                    string? emailValues = email.FirstOrDefault();
                    string? passwordValues = password.FirstOrDefault();

                    bool isEmailValid = string.Equals(emailValues, validEmail, StringComparison.OrdinalIgnoreCase);

                    bool isPasswordValid = string.Equals(passwordValues, validPassword);

                    if (isEmailValid && isPasswordValid)
                    {
                        await context.Response.WriteAsync("Successful login");
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Invalid login");

                    }
                }

            }
            else
            {
                await next(context);
            }


        }
    }

    public static class UseLonginMiddleware
    {
        public static IApplicationBuilder UseCustomLogin(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomLoginMiddleware>();
        }
    }
}
