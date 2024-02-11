using System.ComponentModel.DataAnnotations;

namespace SolarTestTask_lvl5.Contracts.User
{
    /// <summary>
    /// Модель для создания пользователя
    /// </summary>
    public class CreateUserRequest
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
        public string BirthDate { get; set; }

        
    }
}
