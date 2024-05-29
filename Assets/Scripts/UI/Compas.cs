using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compas : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _arrow;
    [SerializeField] private SpriteRenderer _shipIcon;
    [SerializeField] private Transform _ship;
    [SerializeField] private float _distance;

    private void Update()
    {
        _arrow.gameObject.SetActive(false);
        _shipIcon.gameObject.SetActive(false);

        if (Vector3.Distance(_ship.transform.position, transform.position) >= _distance)
        {
            _arrow.gameObject.SetActive(true);
            _shipIcon.gameObject.SetActive(true);
            RotateIndicator();
        }
    }

    private void RotateIndicator()
    {
        Vector3 target = new Vector3(_ship.position.x, transform.position.y, _ship.position.z);
        Vector3 direction = target - transform.position;
        if (direction == Vector3.zero)
            return;
        Quaternion targetQuternion = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = targetQuternion;
    }
}
