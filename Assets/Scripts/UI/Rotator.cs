using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        gameObject.transform.LookAt(_camera.transform.position);
    }
}
