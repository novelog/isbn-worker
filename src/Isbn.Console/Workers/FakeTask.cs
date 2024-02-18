using Coravel.Invocable;
using Isbn.Console.Services;
using Isbn.Providers.Common;
using Novelog.BookService.Api;

namespace Isbn.Console.Workers;

public class FakeTask : IInvocable
{
    private readonly IIsbnService _isbnService;
    private readonly IBookService _bookService;

    public FakeTask([FromKeyedServices("fake")] IIsbnService isbnService, IBookService bookService)
    {
        _isbnService = isbnService;
        _bookService = bookService;
    }

    public async Task Invoke()
    {
        var books = await _isbnService.GetRecentAsync();

        _bookService.CreateBooks(
            books.Select(book => new CreateBookRequest { Title = book.Title }));
    }
}
