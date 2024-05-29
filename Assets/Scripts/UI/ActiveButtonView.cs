using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveButtonView : MonoBehaviour
{
    [SerializeField] private Image _eButtonImage;

    public void SetActiveEImage(bool active)
    {
        _eButtonImage.gameObject.SetActive(active);
    }
}
