using MAUI.Reader.Model;
using Newtonsoft.Json;

namespace MAUI.Reader.Mapper;

public abstract class Mapper
{
    public static Book ToBook(string json)
    {
        if (json.Length == 0)
        {
            return new Book();
        }
        return JsonConvert.DeserializeObject<Book>(json);
    }

    public static List<Book> ToBooks(string json)
    {
        if (json.Length == 0)
        {
            return new List<Book>();
        }
        return JsonConvert.DeserializeObject<List<Book>>(json);
    }
}