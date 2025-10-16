using Exam_backend;
using Exam_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//Conect to SQL Server
builder.Services.AddDbContext<ExamDbContext>(options => options.UseInMemoryDatabase("ExamDB"));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors();

app.UseDefaultFiles(new DefaultFilesOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "browser")
    )
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "browser")
    )
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

#region Endpoints

//mock Data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExamDbContext>();
    if (!db.Questions.Any())
    {
        db.Questions.AddRange(
            new Question { Text = "1 + 1 = ?", AnswerA = "1", AnswerB = "2", AnswerC = "3", AnswerD = "4", CorrectAnswer = "2" },
            new Question { Text = "5 - 2 = ?", AnswerA = "2", AnswerB = "3", AnswerC = "4", AnswerD = "5", CorrectAnswer = "3" }
        );
        db.SaveChanges();
    }
}

//get questions
app.MapGet("/questions", async (ExamDbContext db) =>
{
    return await db.Questions.ToListAsync();
});

//submit
app.MapPost("/submit", async (ExamDbContext db, ExamSubmitRequest request) =>
{
    //Calculate
    int score = 0;
    foreach (var a in request.Answers)
    {
        var q = await db.Questions.FindAsync(a.QuestionId);
        if (q != null && q.CorrectAnswer == a.UserAnswer)
        {
            score++;
        }
    }

    //save result
    var result = new ExamResult { ExaminerName = request.Name, Score = score };
    db.ExamResults.Add(result);
    await db.SaveChangesAsync();

    return Results.Ok(new { request.Name, score }); ;
});

#endregion

//app.UseHttpsRedirection();

app.Run();


