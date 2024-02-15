using Bogus;
using Isbn.Providers.Common;
using Isbn.Providers.Helpers;
using Isbn.Providers.Models;

namespace Isbn.Providers.Services.Mock;

public class FakeService : IIsbnService
{
    public async Task<IEnumerable<IsbnDetails>> GetRecentAsync()
    {
        await Task.Delay(100); // simulate external api 

        return new Faker<IsbnDetails>()
            .RuleFor(i => i.Title, f => f.Lorem.Word())
            .RuleFor(i => i.Isbn10, f =>
                IsbnCalculator.GenerateIsbn10(f.Random.ReplaceNumbers("#########")))
            .RuleFor(i => i.Isbn13, (f, i) => i.Isbn10.AsIsbn13())
            .GenerateBetween(3, 5);
    }
}
