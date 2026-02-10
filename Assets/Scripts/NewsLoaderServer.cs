using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NewsLoaderServer : INewsLoader
{
    private string _filePath;

    public NewsLoaderServer(string filePath)
    { 
        _filePath = filePath; 
    }

    public async Task<List<NewsItem>> LoadNewsAsync()
    {
        await Task.Delay(2000);

        try
        {
            TextAsset jsonAsset =  Resources.Load<TextAsset>(_filePath);
            string wrapped = "{\"Items\":" + jsonAsset.text + "}";

            NewsWrapper wrappedNews = JsonUtility.FromJson<NewsWrapper>(wrapped);

            await Task.Delay(1000);

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
