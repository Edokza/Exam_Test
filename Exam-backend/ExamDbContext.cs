using Exam_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_backend
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) { }

        public DbSet<Question> Questions => Set<Question>();
        public DbSet<ExamResult> ExamResults => Set<ExamResult>();

    }
}
