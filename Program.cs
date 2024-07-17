using System.Net.Mime;
using System.Threading;
using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;
using System.Net;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Text;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System;
using System.Collections.Immutable;
using BookShare;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using BookShare.Entities;

var builder = WebApplication.CreateBuilder(args);


var polityUserAuthentifition = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();


builder.Services.AddControllersWithViews(
    opc => opc.Filters.Add(new AuthorizeFilter(polityUserAuthentifition))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(opc => opc.UseSqlServer("name=BookShareSqlConexion"));

builder.Services.AddAuthentication();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    opc => {opc.SignIn.RequireConfirmedAccount = false;}
).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(
    IdentityConstants.ApplicationScheme, opc => 
    {
        opc.LoginPath = "/User/login";
        opc.AccessDeniedPath = "/User/login";
    }
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
