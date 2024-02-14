using Coravel.Invocable;
using Isbn.Providers.Common;

namespace Isbn.Console.Workers;

public class FakeTask : IInvocable
{
    private readonly ILogger<FakeTask> _logger;
    private readonly IIsbnService _isbnService;

    public FakeTask([FromKeyedServices("fake")] IIsbnService isbnService, ILogger<FakeTask> logger)
    {
        _isbnService = isbnService;
        _logger = logger;
    }

    public async Task Invoke()
    {
        var books = await _isbnService.GetRecentAsync();
        foreach (var book in books)
            _logger.LogInformation("book: {@Book}", book);
    }
}
