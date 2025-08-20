using exercise.webapi.DTOs;
using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task<Book> UpdateBook(int id, int authorId);
        public Task<Book> DeleteBook(int id);
        public Task<Book> CreateBook(BookPost book);
    }
}
