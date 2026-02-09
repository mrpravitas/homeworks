using UnityEngine;

[CreateAssetMenu(menuName = "News/Display Settings")]
public class NewsDisplaySettings : ScriptableObject
{
    [SerializeField] private float _delayBetweenNews;
    [SerializeField] private string _noTitleText;
    [SerializeField] private string _noContentText;
    [SerializeField] private Color _noTitleContentTextColor;

    public float DelayBetweenNews => _delayBetweenNews;
    public string NoTitleText => _noTitleText;
    public string NoContentText => _noContentText;
    public Color NoTitleContentTextColor => _noTitleContentTextColor;
}
