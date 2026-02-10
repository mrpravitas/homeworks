using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINewsManager : MonoBehaviour
{
    [SerializeField] private Button _showNewsButton;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Button _showNewsImmediately;
    [SerializeField] private Transform _viewportContent;
    [SerializeField] private TMP_Text _TMPNewsPrefab;
    [SerializeField] private GameObject _loadingSpinner;

    [SerializeField] private NewsDisplaySettings _settings;

    [SerializeField] private bool _simulateServer;

    private INewsLoader _newsLoader;
    private List<NewsItem> _news = new List<NewsItem>();
    private bool _newsLoaded;

    private Coroutine _showNewsCoroutine;

    private async Task Start()
    {
        if (_simulateServer)
        {
            _newsLoader = new NewsLoaderServer(NewsPath.ResourcesNews);
        }
        else
        {
            string path = Path.Combine(Application.streamingAssetsPath, NewsPath.StreamingAssetsNews);
            _newsLoader = new NewsLoader(path);
        }

        try
        {
            _news = await _newsLoader.LoadNewsAsync();
        }
        catch (Exception exception)
        {
            Debug.LogError($"Filed to load news: {exception.Message}\n News list is empty");
            _news = new List<NewsItem>();
        }
        finally
        {
            _newsLoaded = true;
        }
    }

    private void OnEnable()
    {
        _showNewsButton.onClick.AddListener(ShowNews);
        _reloadButton.onClick.AddListener(ReloadNews);
        _showNewsImmediately.onClick.AddListener(ShowNewsImmediately);
    }

    private void OnDisable()
    {
        _showNewsButton.onClick.RemoveListener(ShowNews);
        _reloadButton.onClick.RemoveListener(ReloadNews);
        _showNewsImmediately.onClick.RemoveListener(ShowNewsImmediately);
    }

    private void ShowNews()
    {
        if (_showNewsCoroutine != null)
        {
            return;
        }

        _loadingSpinner.SetActive(true);
        _showNewsCoroutine = StartCoroutine(ShowNewsCoroutine());
    }

    private async void ReloadNews()
    {
        if (_showNewsCoroutine != null)
        {
            StopCoroutine(_showNewsCoroutine);
            _showNewsCoroutine = null;
        }

        ClearContent();
        _loadingSpinner.SetActive(true);

        try
        {
            _news = await _newsLoader.LoadNewsAsync();
        }
        catch (Exception exception)
        {
            Debug.LogError($"Filed to load news: {exception.Message}\n News list is empty");
            _news = new List<NewsItem>();
        }

        ShowNews();
    }

    private void ClearContent()
    {
        foreach (Transform child in _viewportContent)
        {
            if (child.gameObject != _loadingSpinner)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator ShowNewsCoroutine()
    {
        while (!_newsLoaded)
        {
            yield return null;
        }

        int newsCount = _news.Count;

        yield return new WaitForSeconds(_settings.DelayBetweenNews);

        for (int i = 0; i < newsCount; i++) 
        {
            yield return new WaitForSeconds(_settings.DelayBetweenNews);

            var item = _news[i];

            TMP_Text news = Instantiate(_TMPNewsPrefab, _viewportContent);
            news.text = NewsToString(item);
            _loadingSpinner.transform.SetAsLastSibling();
        }

        _loadingSpinner.SetActive(false);
    }

    private void ShowNewsImmediately()
    {
        if (_showNewsCoroutine != null)
        {
            StopCoroutine(_showNewsCoroutine);
            _showNewsCoroutine = null;
        }

        _loadingSpinner.SetActive(false);

        ClearContent();

        foreach (var item in _news)
        {
            TMP_Text news = Instantiate(_TMPNewsPrefab, _viewportContent);
            news.text = NewsToString(item);
        }
    }

    private string NewsToString(NewsItem item)
    {
        string itemTitle = item.Title;

        if (string.IsNullOrEmpty(itemTitle))
        {
            string color = ColorUtility.ToHtmlStringRGB(_settings.NoTitleContentTextColor);
            itemTitle = $"<color=#{color}>{_settings.NoTitleText}</color>";
        }

        string itemContent = item.Content;
        if (string.IsNullOrEmpty(itemContent))
        {
            string color = ColorUtility.ToHtmlStringRGB(_settings.NoTitleContentTextColor);
            itemContent = $"<color=#{color}>{_settings.NoContentText}</color>";
        }

        string timestamp = item.Timestamp.ToString("yyyy-MM-dd");

        return $"{timestamp}: {itemTitle}\n{itemContent}";
    }
}
