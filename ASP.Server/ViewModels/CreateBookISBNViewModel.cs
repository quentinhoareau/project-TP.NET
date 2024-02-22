using System.ComponentModel.DataAnnotations;

namespace ASP.Server.ViewModels
{
    public class CreateBookISBNViewModel
    {
        [Required (ErrorMessage = "You need to add a ISBN for this book.")]
        public string Isbn { get; set; }
    }
}
