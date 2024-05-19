using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorckBranchViewer : MonoBehaviour
{
    [SerializeField] private Button RodUpgradeButton;
    [SerializeField] private Button BagUpgradeButton;
    [SerializeField] private Rod _rod;
    [SerializeField] private Bag _bag;
    [SerializeField] private TMP_Text _bagLevel;
    [SerializeField] private TMP_Text _rodLevel;
    [SerializeField] private WorkBrench _workBrench;

    private void OnEnable()
    {
        _bag.Upgraded += OnBagUpgrade;
        _rod.Upgraded += OnRodUpgrade;
    }

    private void OnDisable()
    {
        _bag.Upgraded -= OnBagUpgrade;
        _rod.Upgraded -= OnRodUpgrade;
    }

    private void OnRodUpgrade()
    {
        _rodLevel.text = _rod.Level.ToString();
    }

    private void OnBagUpgrade()
    {
        _bagLevel.text = _bag.Level.ToString();
    }

    public void UpgardeRod()
    {
        _workBrench.TryUpgrade(_rod);
    }
    public void UpgardeBag()
    {
        _workBrench.TryUpgrade(_bag);
    }
}
