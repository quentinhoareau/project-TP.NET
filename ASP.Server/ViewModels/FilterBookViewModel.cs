using System.Collections.Generic;
using ASP.Server.Models;

namespace ASP.Server.ViewModels;

public class FilterBookViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public string FilterBy { get; set; }
}