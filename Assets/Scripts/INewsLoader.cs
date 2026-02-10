using System.Collections.Generic;
using System.Threading.Tasks;

public interface INewsLoader
{
    Task<List<NewsItem>> LoadNewsAsync();
}
