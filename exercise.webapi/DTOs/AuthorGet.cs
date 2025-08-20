using exercise.webapi.Models;

namespace exercise.webapi.DTOs
{
    public class AuthorGet
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AuthorBookGet> Books { get; set; } = new List<AuthorBookGet>();
    }
}
