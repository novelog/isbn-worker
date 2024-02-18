
using Isbn.Console.Transports;
using Novelog.BookService.Api;

namespace Isbn.Console.Services;

public class BookService : IBookService
{
    private readonly GrpcTransport _transport;
    private readonly ILogger<BookService> _logger;

    public BookService(GrpcTransport transport, ILogger<BookService> logger)
    {
        _transport = transport;
        _logger = logger;
    }

    public void CreateBook(CreateBookRequest req)
    {
        var client = new BookServiceDefinition.BookServiceDefinitionClient(_transport.Channel);
        Unary(client, req);
    }
    public async void CreateBooks(IEnumerable<CreateBookRequest> req)
    {
        var client = new BookServiceDefinition.BookServiceDefinitionClient(_transport.Channel);

        await ClientStreaming(client, req);
    }

    private void Unary(BookServiceDefinition.BookServiceDefinitionClient client, CreateBookRequest req)
    {
        var resp = client.CreateBook(req);
        _logger.LogInformation("grpc server createbookrequest response: {@Response}", resp);
    }
    private async Task ClientStreaming(BookServiceDefinition.BookServiceDefinitionClient client, IEnumerable<CreateBookRequest> books)
    {
        using var call = client.CreateBooks();
        foreach (var book in books)
        {
            await call.RequestStream.WriteAsync(book);
        }
        await call.RequestStream.CompleteAsync();

        var resp = await call;
        _logger.LogInformation("book-service response: {@Response}", resp);
    }
}
