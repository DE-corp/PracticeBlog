using System.ComponentModel.DataAnnotations;

namespace PracticeBlog.Data.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Текст обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст", Prompt = "Введите текст комментария")]
        public string Text { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int ArticleID { get; set; }
        public Article Article { get; set; }
    }
}
