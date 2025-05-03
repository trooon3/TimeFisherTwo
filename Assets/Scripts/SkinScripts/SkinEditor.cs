using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Saves;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Assets.Scripts.SkinScripts
{
    public class SkinEditor : MonoBehaviour
    {
        [SerializeField] private Skin _defaultSkin;
        [SerializeField] private List<Skin> _skins;
        [SerializeField] private PlayerAnimationController _controller;

        private SkinNames _chosenSkinName;

        public List<Skin> Skins => _skins;

        private void Awake()
        {
           _chosenSkinName = YandexGame.savesData.LoadChosenSkin();

            if (!_skins.Contains(_defaultSkin))
            {
                _skins.Add(_defaultSkin);
            }

            foreach (var skin in _skins)
            {
                skin.gameObject.SetActive(false);
            }

            foreach (var skin in _skins)
            {
                if (skin.Name == _chosenSkinName)
                {
                    SetSkin(skin);
                }
            }
        }

        public void SetSkin(Skin skinToChoose)
        {
            foreach (var skin in _skins)
            {
                skin.gameObject.SetActive(false);

                if (skin == skinToChoose)
                {
                    skinToChoose.gameObject.SetActive(true);
                    _chosenSkinName = skin.Name;
                    _controller.SetAnimator(skinToChoose.Animator);
                }
            }

            YandexGame.savesData.SaveChosenSkin(_chosenSkinName);
        }
    }
}

