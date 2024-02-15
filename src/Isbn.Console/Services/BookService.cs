
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
        UnaryTest(client, req);
    }

    private void UnaryTest(BookServiceDefinition.BookServiceDefinitionClient client, CreateBookRequest req)
    {
        var resp = client.CreateBookUnary(req);
        _logger.LogInformation("grpc server createbookrequest response: {@Response}", resp);
    }
}
