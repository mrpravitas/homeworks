using UnityEngine;

public class JumpButton : MonoBehaviour
{
    [SerializeField] private FirstPersonController _controller;

    public void OnJumpButtonPressed()
    {
        _controller.Jump();
    }
}
