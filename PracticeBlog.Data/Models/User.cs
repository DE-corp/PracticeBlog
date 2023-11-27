using System.ComponentModel.DataAnnotations;

namespace PracticeBlog.Data.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле Login обязательно для заполнения")]
        [Display(Name = "Login", Prompt = "YourLogin")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 3)]
        public string Password { get; set; }
        public string Role { get; set; } = "Admin";
        public int Age { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
