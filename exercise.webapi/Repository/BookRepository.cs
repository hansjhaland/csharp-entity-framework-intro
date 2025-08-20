using exercise.webapi.Data;
using exercise.webapi.DTOs;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository: IBookRepository
    {
        DataContext _db;
        
        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _db.Books.Where(book => book.Id == id).Include(book => book.Author).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author).ToListAsync();

        }

        public async Task<Book> UpdateBook(int id, int authorId)
        {
            Book book = await _db.Books.Where(b => b.Id == id).Include(book => book.Author).FirstOrDefaultAsync();
            Author? author = _db.Authors.FirstOrDefault(author => author.Id == authorId);
            if (author == null) 
            {
                return null;
            }

            book.AuthorId = authorId;
            book.Author = author;

            _db.SaveChangesAsync();

            return book;

        }

        public async Task<Book> DeleteBook(int id)
        {
            var entity = await _db.Books.FindAsync(id);
            if (entity == null)
            {
                return null;    
            }
            _db.Books.Remove(entity);
            _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Book> CreateBook(BookPost book)
        {
            var author = _db.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
            if (author == null)
            {
                return null;
            }
            var newBook = new Book();
            newBook.Title = book.Title;
            newBook.AuthorId = book.AuthorId;
            newBook.Author = author;

            _db.Books.Add(newBook);
            await _db.SaveChangesAsync();

            return newBook;
        }
    }
}
