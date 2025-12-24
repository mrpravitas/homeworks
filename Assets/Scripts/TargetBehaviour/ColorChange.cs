using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        InvokeRepeating(nameof(ChangeColor), 0f, 1f);
    }

    private void ChangeColor()
    {
        _renderer.material.color = new Color(
            Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
