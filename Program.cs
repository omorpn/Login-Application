using Login_Application;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseCustomLogin();
app.UseEndpoints(endpoint =>
{
    _=endpoint.MapPost("/login", async context =>
    {
        string userEmail = "admin@example.com";
        string userPassword = "admin1234";

        List<string> errors = [];

            string userQueryEmail= context.Request.Query["email"].ToString(); 
        string userQueryPassword= context.Request.Query["password"].ToString();

        if (string.IsNullOrEmpty(userQueryEmail))
        {
            errors.Add("Invalid input for 'email'");
        }
        if (string.IsNullOrEmpty(userQueryPassword))
        {
            errors.Add("Invalid input for 'password'");
        }

        if(errors.Count > 0)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(string.Join("\n",errors));
            return;
        }

        bool isEmailMatch = string.Equals(userQueryEmail, userEmail, StringComparison.OrdinalIgnoreCase);
        bool isPasswordMatch = string.Equals(userQueryPassword, userPassword);
        if(isEmailMatch && isPasswordMatch)
        {
            await context.Response.WriteAsync("Successful login");

        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid login");

        }

       
    });
});
app.MapFallback(async context =>
{
    await context.Response.WriteAsync("No response!"); 
});
app.Run();
