using exercise.webapi.DTOs;
using exercise.webapi.Repository;
using System.Runtime.CompilerServices;

namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorsApi(this WebApplication app)
        {
            app.MapGet("/authors", GetAuthors);
            app.MapGet("/authors/{id}", GetAuthorsById);
        }

        private static async Task<IResult> GetAuthorsById(IAuthorRepository repository, int id)
        {
            var entity = await repository.GetAuthorById(id);
            if (entity == null) return TypedResults.NotFound();

            var author = new AuthorGet();
            author.FirstName = entity.FirstName;
            author.LastName = entity.LastName;

            foreach (var book in entity.Books) 
            {
                author.Books.Add(new AuthorBookGet() { Title = book.Title } );
            }
            return TypedResults.Ok(author);
        }

        private static async Task<IResult> GetAuthors(IAuthorRepository repository)
        {
            List<Object> response = new List<Object>();
            var results = await repository.GetAllAuthors();
            foreach (var result in results)
            {
                List<AuthorBookGet> books = new List<AuthorBookGet>();
                foreach (var authorBook in result.Books)
                {
                    var book = new AuthorBookGet();
                    book.Title = authorBook.Title;
                    books.Add(book);
                }
                var author = new AuthorGet();
                author.FirstName = result.FirstName;
                author.LastName = result.LastName;
                author.Books = books;

                response.Add(author);
            }
            return TypedResults.Ok(response);
        }
    }
}
