using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _handle;
    [SerializeField] private FirstPersonController _controller;

    private Vector2 _inputVector;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, eventData.position, eventData.pressEventCamera, out localPos))
        {
            float radius = _background.sizeDelta.x * 0.5f;
            if (radius <= 0f)
            {
                return;
            }

            localPos /= radius;

            if (localPos.magnitude > 1f)
            {
                localPos = localPos.normalized;
            }

            _inputVector = localPos;
            _handle.anchoredPosition = _inputVector * radius;

            _controller.SetMoveInput(_inputVector);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;

        _controller.SetMoveInput(Vector2.zero);
    }
}
