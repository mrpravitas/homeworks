using System.Collections;
using System.Collections.Generic;
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

    private NewsLoader _newsLoader;
    private string _newsSource;
    private List<NewsItem> _news;

    private Coroutine _showNewsCoroutine;

    private async Task Start()
    {
        _newsSource = Application.streamingAssetsPath + "/news.json";
        _newsLoader = new NewsLoader(_newsSource);

        _news = await _newsLoader.LoadNewsAsync();
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

        _news = await _newsLoader.LoadNewsAsync();

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
