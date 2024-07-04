using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private SkinView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private SkinEditor _skinEditor;
    [SerializeField] private Closet _closet;

    private List<SkinView> _views;

    private void Start()
    {
        _views = new List<SkinView>();

        foreach (var item in _skinEditor.Skins)
        {
            var skinview = Instantiate(_template, _container);

            skinview.Init(_skinEditor, item, _closet);
            skinview.gameObject.SetActive(true);

            _views.Add(skinview);
        }
    }
}
