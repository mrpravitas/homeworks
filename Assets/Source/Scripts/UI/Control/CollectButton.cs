using UnityEngine;

public class CollectButton : MonoBehaviour
{
    [SerializeField] private PlayerCollect _playerCollect;

    public void OnPressed()
    {
        if (_playerCollect != null)
        {
            _playerCollect.Collect();
        }
    }
}
