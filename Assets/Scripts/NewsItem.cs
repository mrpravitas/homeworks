using System;

[Serializable]
public class NewsItem
{
    public string? title;
    public string? content;
    public string timestamp;

    public string? Title => title;
    public string? Content => content;
    public DateTime Timestamp => DateTime.Parse(timestamp);
}
