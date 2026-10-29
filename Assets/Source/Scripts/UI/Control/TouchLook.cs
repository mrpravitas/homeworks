using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLook : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private FirstPersonController _controller;

    private int _fingerId = -1;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_fingerId == -1)
        {
            _fingerId = eventData.pointerId;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_fingerId != eventData.pointerId)
        {
            return;
        }

        if (_controller != null)
        {
            _controller.Look(eventData.delta);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_fingerId == eventData.pointerId)
        {
            _fingerId = -1;
        }
    }
}
