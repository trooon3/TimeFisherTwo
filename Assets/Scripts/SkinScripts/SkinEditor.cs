using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Saves;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SkinScripts
{
    public class SkinEditor : MonoBehaviour
    {
        [SerializeField] private Skin _defaultSkin;
        [SerializeField] private List<Skin> _skins;
        [SerializeField] private PlayerAnimationController _controller;
        [SerializeField] private DataSaver _saver;

        private string _chosenSkinName;
        private readonly string _chosenSkinKey = "defaultSkinKey";

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

            string chosenSkinName = _saver.LoadChosenSkin(_chosenSkinKey);

            if (chosenSkinName != null)
            {
                _chosenSkinName = chosenSkinName;
            }
            else
            {
                _chosenSkinName = null;
            }

            foreach (var skin in _skins)
            {
                if (skin.Name == _chosenSkinName)
                {
                    SetSkin(skin);
                }
            }

            if (_chosenSkinName == null || _chosenSkinName == "")
            {
                SetSkin(_defaultSkin);
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

            _saver.SaveChosenSkin(_chosenSkinKey, _chosenSkinName);
        }
    }
}

