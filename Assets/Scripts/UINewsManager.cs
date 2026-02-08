using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UINewsManager : MonoBehaviour
{
    private NewsLoader _newsLoader;
    private string _newsSource;
    private List<NewsItem> _news;

    private async Task Start()
    {
        _newsSource = Application.streamingAssetsPath + "/news.json";
        _newsLoader = new NewsLoader(_newsSource);

        _news = await _newsLoader.LoadNewsAsync();
        StartCoroutine(ShowNewsCoroutine());
    }

    private IEnumerator ShowNewsCoroutine()
    {
        foreach (var item in _news)
        {
            Debug.Log(NewsToString(item));
            yield return new WaitForSeconds(2f);
        }
    }
    private string NewsToString(NewsItem item)
    {
        string itemTitle = item.Title;
        string title = string.IsNullOrEmpty(itemTitle) ? "no title" : itemTitle;

        string itemContent = item.Content;
        string content = string.IsNullOrEmpty(itemContent) ? "no content" : itemContent;

        string timestamp = item.Timestamp.ToString("yyyy-MM-dd");

        return $"{timestamp}: {title} - {content}";
    }
}
