namespace ASP.Server.Dtos
{
    public class BookDto : BookWithoutContentDto
    {
        public string Content { get; set; }
    }
}
