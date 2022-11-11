using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo;
using NovaLite.ToDo.Configuration;
using NovaLite.ToDo.Queries.Assignments;
using NovaLite.ToDo.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(o => o.EnableEndpointRouting = false);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToDoContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoContext")), ServiceLifetime.Scoped);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.Audience = builder.Configuration["JWTSettings:Audience"];
        o.Authority = builder.Configuration["JWTSettings:Authority"];
    })
    .AddJwtBearer("School", o =>
    {
        o.Audience = builder.Configuration["JWTSettings:Audience"];
        o.Authority = builder.Configuration["JWTSettings:AuthoritySchool"];
    });

builder.Services.AddAuthorization(options =>
{
    var policyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme,
        "School");

    policyBuilder =policyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = policyBuilder.Build();
});


builder.Services.AddMediatR(typeof(GetAllAssignmentQueryHandler).Assembly);

builder.Services.AddTransient<EmailService>();
builder.Services.AddHostedService<ReminderService>();
builder.Services.AddTransient<AttachmentSasUriGenerator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<BlobStorageConfiguration>(builder.Configuration.GetSection("BlobStorageSettings"));

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration);
builder.Logging.AddSerilog(logger.CreateLogger());

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
app.UseMvc();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ToDoContext>();
    context.Database.Migrate();
    //context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
