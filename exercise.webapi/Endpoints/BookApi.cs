using exercise.webapi.DTOs;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            app.MapGet("/books", GetBooks);
            app.MapGet("/books/{id}", GetBookById);
            app.MapPut("/books/{id}", UpdateBook);
        }

        private static async Task<IResult> UpdateBook(IBookRepository repository, int id, int authorId)
        {
            var entity = await repository.UpdateBook(id, authorId);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            var authorGet = new AuthorGet();
            authorGet.FirstName = entity.Author.FirstName;
            authorGet.LastName = entity.Author.LastName;
            authorGet.Email = entity.Author.Email;

            var bookGet = new BookGet();
            bookGet.Title = entity.Title;
            bookGet.Author = authorGet;

            return TypedResults.Ok(bookGet);
        }

        private static async Task<IResult> GetBookById(IBookRepository repository, int id)
        {
            var entity = await repository.GetBookById(id);

            var authorGet = new AuthorGet();
            authorGet.FirstName = entity.Author.FirstName;
            authorGet.LastName = entity.Author.LastName;
            authorGet.Email = entity.Author.Email;

            var bookGet = new BookGet();
            bookGet.Title = entity.Title;
            bookGet.Author = authorGet;

            return TypedResults.Ok(bookGet);
        }

        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            List<Object> response = new List<Object>();
            var results = await bookRepository.GetAllBooks();
            foreach (Book book in results)
            {
                var authorGet = new AuthorGet();
                authorGet.FirstName = book.Author.FirstName;
                authorGet.LastName= book.Author.LastName;
                authorGet.Email = book.Author.Email;

                var bookGet = new BookGet();
                bookGet.Title = book.Title;
                bookGet.Author = authorGet;

                response.Add(bookGet);
            }

            return TypedResults.Ok(response);
        }
    }
}

