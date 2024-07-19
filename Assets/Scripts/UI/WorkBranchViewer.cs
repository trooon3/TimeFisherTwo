using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkBranchViewer : MonoBehaviour
{
    [SerializeField] private Button _rodUpgradeButton;
    [SerializeField] private Button _bagUpgradeButton;

    [SerializeField] private Rod _rod;
    [SerializeField] private Bag _bag;

    [SerializeField] private TMP_Text _bagLevel;
    [SerializeField] private TMP_Text _rodLevel;

    [SerializeField] private TMP_Text _bagNextLevel;
    [SerializeField] private TMP_Text _rodNextLevel;

    [SerializeField] private TMP_Text _rodUpgradeCostText;
    [SerializeField] private TMP_Text _bagUpgradeCostText;

    [SerializeField] private WorkBrench _workBrench;

    private void Start()
    {
        OnRodUpgrade();
        OnBagUpgrade();
    }

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
        _rodLevel.text = $"LVL {_rod.Level}";
        _rodUpgradeCostText.text = $"{_rod.CountResourseToUpgrade}";
        _rodNextLevel.text = $"{_rod.NextLevel} Lvl";
    }

    private void OnBagUpgrade()
    {
        _bagLevel.text = $"LVL {_bag.Level}";
        _bagUpgradeCostText.text = $"{_bag.CountResourseToUpgrade}";
        _bagNextLevel.text = $"{_bag.NextLevel} Lvl";
    }

    public void UpgardeRod()
    {
        _workBrench.TryUpgrade(_rod);
        OnRodUpgrade();
    }

    public void UpgardeBag()
    {
        _workBrench.TryUpgrade(_bag);
        OnBagUpgrade();
    }
}
