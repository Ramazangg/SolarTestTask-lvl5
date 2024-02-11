using System.ComponentModel.DataAnnotations;

namespace SolarTestTask_lvl5.Domain
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Адрес электронной почты пользователя 
        /// </summary>
        [EmailAddress]
        public string email { get; set; }

        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Фотография юзера
        /// </summary>
        public byte[] Photo { get; set; }


    }
}
