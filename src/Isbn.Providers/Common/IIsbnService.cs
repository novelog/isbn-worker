using Isbn.Providers.Models;

namespace Isbn.Providers.Common;

public interface IIsbnService
{
    Task<IEnumerable<IsbnDetails>> GetRecentAsync();
}
