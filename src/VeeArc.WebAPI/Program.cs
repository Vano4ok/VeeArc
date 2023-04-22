using System.Security.Claims;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VeeArc.Application;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Application.Common.Settings;
using VeeArc.Domain.Entities;
using VeeArc.Infrastructure;
using VeeArc.WebAPI.Filter;
using VeeArc.WebAPI.Middleware;
using VeeArc.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.AllowEmptyInputInBodyModelBinding = true;
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddFluentValidation(opt =>
{
    opt.DisableDataAnnotationsValidation = true;
    opt.LocalizationEnabled = false;
});

builder.Services.AddOptions<JwtOptions>()
                .Bind(builder.Configuration.GetSection(JwtOptions.SectionName));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(
                        new JwtTokenValidator(builder.Configuration.GetValue<string>("Jwt:SecretKey")));
                });

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
