using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class NewsLoader
{
    private string _filePath;
    private List<NewsItem> _news;

    public NewsLoader(string filePath)
    { 
        _filePath = filePath;
    }

    public async Task<List<NewsItem>> LoadNewsAsync()
    {
        string json = await File.ReadAllTextAsync(_filePath);
        string wrapped = "{\"Items\":" + json + "}";

        NewsWrapper wrappedNews = JsonUtility.FromJson<NewsWrapper>(wrapped);

        return wrappedNews.Items;
    }

    [Serializable]
    private class NewsWrapper
    {
        public List<NewsItem> Items;
    }
}
