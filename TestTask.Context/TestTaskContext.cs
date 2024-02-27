using Microsoft.EntityFrameworkCore;
using TestTask.Context.Models;

namespace TestTask.Context
{
    public class TestTaskContext : DbContext
    {
        /// <summary>
        /// Таблица справочника сотрудников
        /// </summary>
        public DbSet<Staff> Staffs { get; set; }

        /// <summary>
        /// Проверяет создана ли база данных.
        /// True - база данных только что создана. 
        /// False - база данных уже существует.
        /// </summary>
        public bool baseCreated { get; set; }

        public TestTaskContext()
        {
            baseCreated = Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Database=TestTaskDatabase;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}