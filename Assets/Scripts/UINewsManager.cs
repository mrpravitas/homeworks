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
    [SerializeField] private Transform _viewportContent;
    [SerializeField] private TMP_Text _TMPNewsPrefab;

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
    }

    private void OnDisable()
    {
        _showNewsButton.onClick.RemoveListener(ShowNews);
        _reloadButton.onClick.RemoveListener(ReloadNews);
    }

    private void ShowNews()
    {
        if (_showNewsCoroutine != null)
        {
            return;
        }

        _showNewsCoroutine = StartCoroutine(ShowNewsCoroutine());
    }

    private async void ReloadNews()
    {
        if (_showNewsCoroutine != null)
        {
            StopCoroutine(_showNewsCoroutine);
            _showNewsCoroutine = null;
        }

        foreach (Transform child in _viewportContent)
        {
            Destroy(child.gameObject);
        }

        _news = await _newsLoader.LoadNewsAsync();

        ShowNews();
    }

    private IEnumerator ShowNewsCoroutine()
    {
        foreach (var item in _news)
        {
            TMP_Text news = Instantiate(_TMPNewsPrefab, _viewportContent);
            news.text = NewsToString(item);

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

        return $"{timestamp}: {title}\n{content}";
    }
}
