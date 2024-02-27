using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace TestTask.Context.Models
{
    /// <summary>
    /// Сущность сотрудника
    /// </summary>
    public class Staff
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronomyc { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Пол (Female, Male)
        /// </summary>
        [Required]
        public string Gender { get; set;}
    }
}
