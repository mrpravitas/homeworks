using UnityEngine;

public class Rotate : MonoBehaviour
{
    private int _xAngle = 0;
    private int _yAngle = 0;
    private int _zAngle = 0;

    private void Start()
    {
        int axis = Random.Range(0, 3);

        switch (axis)
        {
            case 0:
                _xAngle = 90;
                break;
            case 1:
                _yAngle = 90;
                break;
            case 2:
                _zAngle = 90;
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(_xAngle, _yAngle, _zAngle) * Time.deltaTime);
    }
}
