using CookieAuth.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddControllers();

builder
    .Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        const string RootPath = "/";
        const string CookieAuthenticationOptionsConfigurationKey =
            $"Authentication:Schemes:{CookieAuthenticationDefaults.AuthenticationScheme}";

        builder.Configuration.Bind(CookieAuthenticationOptionsConfigurationKey, options);

        options.Cookie.Domain = null;
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.Name = CookieAuthenticationHelper.SetAuthenticationCookieNameWithHostPrefix(options.Cookie.Name);
        options.Cookie.Path = RootPath;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        options.ForwardAuthenticate = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ForwardChallenge = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ForwardForbid = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ForwardSignIn = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ForwardSignOut = CookieAuthenticationDefaults.AuthenticationScheme;

        options.LoginPath = new PathString();
        options.LogoutPath = new PathString();

        options.Validate();
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region Configure HTTP Pipeline

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

#endregion

app.Run();
