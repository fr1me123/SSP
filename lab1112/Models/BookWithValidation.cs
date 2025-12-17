using System.ComponentModel.DataAnnotations;

namespace lab1112.Models
{
    public class BookWithValidation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно")]
        [Display(Name = "Название книги")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Автор обязателен")]
        [Display(Name = "Автор")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Год издания обязателен")]
        [Display(Name = "Год издания")]
        [Range(1000, 2024, ErrorMessage = "Год издания должен быть между 1000 и 2024")]
        public int PublicationYear { get; set; }

        [Display(Name = "Адрес издательства")]
        [StringLength(200, ErrorMessage = "Адрес не должен превышать 200 символов")]
        public string PublisherAddress { get; set; }

        [Required(ErrorMessage = "Цена обязательна")]
        [Display(Name = "Цена")]
        [Range(0.01, 100000, ErrorMessage = "Цена должна быть между 0.01 и 100000")]
        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Цена должна быть числом с максимум 2 знаками после запятой")]
        public decimal Price { get; set; }

        [Display(Name = "Книготорговая фирма")]
        [StringLength(100, ErrorMessage = "Название фирмы не должно превышать 100 символов")]
        public string BookStore { get; set; }

        public virtual Author Author { get; set; }
    }
}