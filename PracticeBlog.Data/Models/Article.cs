using System.ComponentModel.DataAnnotations;

namespace PracticeBlog.Data.Models
{
    public class Article
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Заголовок обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название статьи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Содержание обязательно для заполнения")]
        [DataType(DataType.Text)]
        [StringLength(1000, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 50)]
        [Display(Name = "Текст", Prompt = "Введите текст статьи")]
        public string Text { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
