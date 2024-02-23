using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.Server.Models;

namespace ASP.Server.ViewModels;

public class EditAuthorViewModel
{
    public int Id { get; set; }
    [Required (ErrorMessage = "You need to add a FirstName to the Author.")]
    public string FirstName { get; set; }
    
    [Required (ErrorMessage = "You need to add a LastName to the Author.")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "You need to select at least 1 book")]
    [MinLength(1, ErrorMessage = "You need to select at least 1 book")]
    public IEnumerable<int> Books { get; set; }
    public IEnumerable<Book> AllBooks { get; init; }
}