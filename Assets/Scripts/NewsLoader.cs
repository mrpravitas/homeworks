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
        try
        {
            string json = await File.ReadAllTextAsync(_filePath);
            string wrapped = "{\"Items\":" + json + "}";

            NewsWrapper wrappedNews = JsonUtility.FromJson<NewsWrapper>(wrapped);

            if (wrappedNews?.Items == null)
            {
                return new List<NewsItem>();
            }

            return wrappedNews.Items;
        }
        catch (Exception exception)
        {
            Debug.LogError($"Failed to load json: {exception.Message}");
            throw exception;
        }
    }

    [Serializable]
    private class NewsWrapper
    {
        public List<NewsItem> Items;
    }
}
