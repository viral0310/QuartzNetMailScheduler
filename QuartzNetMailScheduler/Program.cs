using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddQuartz(q =>
{
            q.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = new JobKey("DailyMailJob");
            q.AddJob<DailyMailJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("DailyMailJob-trigger")
                .WithCronSchedule("*/30 * * * * ?")); // Every 30 seconds
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
