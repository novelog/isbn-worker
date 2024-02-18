using Novelog.BookService.Api;

namespace Isbn.Console.Services;

public interface IBookService
{
    void CreateBook(CreateBookRequest req);
    void CreateBooks(IEnumerable<CreateBookRequest> req);
}
