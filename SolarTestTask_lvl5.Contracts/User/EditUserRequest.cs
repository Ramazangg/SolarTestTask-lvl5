using System.ComponentModel.DataAnnotations;

namespace SolarTestTask_lvl5.Contracts.User
{
    /// <summary>
    /// Модель для изменения данных о пользователе
    /// </summary>
    public class EditUserRequest
    {
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
    }
}
