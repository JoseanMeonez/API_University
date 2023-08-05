using API_University.Data;
using API_University.Repositories;
using API_University.Repositories.Verifiers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiUniversityContext>(options => {
    // Here we're getting the connection string from the appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});

builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IProfessorRepository, ProfessorRepository>();
builder.Services.AddTransient<IClassRepository, ClassRepository>();
builder.Services.AddTransient<IClassroomRepository, ClassroomRepository>();
builder.Services.AddTransient<IProfessorClassRepository, ProfessorClassRepository>();
builder.Services.AddTransient<IStudentRegistrationRepository, StudentRegistrationRepository>();
builder.Services.AddTransient<INameChecker, StudentChecker>();
builder.Services.AddTransient<IEmailChecker, StudentChecker>();
builder.Services.AddTransient<IPhoneNumberChecker, StudentChecker>();
builder.Services.AddTransient<INameChecker, ProfessorChecker>();
builder.Services.AddTransient<IEmailChecker, ProfessorChecker>();
builder.Services.AddTransient<IPhoneNumberChecker, ProfessorChecker>();
builder.Services.AddTransient<INameChecker, ClassChecker>();
builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();

var app = builder.Build();

/* // DataBase Migration:
using (var scope = app.Services.CreateScope())
{
  var context = scope.ServiceProvider.GetRequiredService<ApiUniversityContext>();
  context.Database.Migrate();
}
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
