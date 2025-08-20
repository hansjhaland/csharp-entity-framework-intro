using exercise.webapi.Models;

namespace exercise.webapi.DTOs
{
    public class BookGet
    {
        public string Title { get; set; }
        public BookAuthorGet Author { get; set; }
    }
}
