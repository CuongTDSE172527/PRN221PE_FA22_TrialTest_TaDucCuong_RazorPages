using Candidate_DAO;
using Candidate_Repositories;
using Candidate_Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IHRCandidateService, HRCandidateService>();
builder.Services.AddScoped<IHRCandidateRepo, HRCandidateRepo>();
builder.Services.AddScoped<IJobpostService, JobPostService>();
builder.Services.AddScoped<IHRJobPost, HRJobPostRepo>();
builder.Services.AddScoped<IHRAccountService, HRAccountService>();
builder.Services.AddDbContext<CandidateManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnect"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

