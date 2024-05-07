using UnityEngine;

public class TimeViewRotator : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        gameObject.transform.LookAt(_camera.transform.position);
    }
}
