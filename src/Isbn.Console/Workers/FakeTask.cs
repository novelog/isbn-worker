using Coravel.Invocable;
using Grpc.Net.Client;
using Isbn.Console.Services;
using Isbn.Console.Transports;
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

        foreach (var book in books)
            _bookService.CreateBook(new CreateBookRequest { Title = book.Title });
    }
}
