using UnityEngine;

public class PlayerCameraFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        Vector3 temp = gameObject.transform.position;

        temp.z = _player.transform.position.z;
        temp.x = _player.transform.position.x;
        temp.y = _player.transform.position.y;
        
        transform.position = temp + _offset;
    }
}
