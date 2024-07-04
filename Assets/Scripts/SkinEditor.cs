using System.Collections.Generic;
using UnityEngine;

public class SkinEditor : MonoBehaviour
{
    [SerializeField] private Skin _defaultSkin;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private PlayerAnimationController _controller;

    public List<Skin> Skins => _skins;
 
    private void Awake()
    {
        if (!_skins.Contains(_defaultSkin))
        {
            _skins.Add(_defaultSkin);
        }
        
        foreach (var skin in _skins)
        {
            skin.gameObject.SetActive(false);
        }

        _defaultSkin.gameObject.SetActive(true);
    }

    public void SetSkin(Skin skinToChoose)
    {
        foreach (var skin in _skins)
        {
            skin.gameObject.SetActive(false);

            if (skin == skinToChoose)
            {
                skinToChoose.gameObject.SetActive(true);
                _controller.SetAnimator(skinToChoose.Animator);
                
            }
        }
    }
}
