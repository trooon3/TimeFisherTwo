using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfCatchViewer : MonoBehaviour
{
    [SerializeField] private FieldOfView _field;
    [SerializeField] private Image _image;
    [SerializeField] private Player _player;

    private void Start()
    {
    }
    private void Update()
    {
        _image.fillAmount = _field.Angle/180;
        //transform.Rotate(transform.)
    }
}
