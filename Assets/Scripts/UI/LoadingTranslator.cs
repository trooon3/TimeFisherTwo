using UnityEngine;
using TMPro;

public class LoadingTranslator : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingText;

    private void Start()
    {
        _loadingText.text = Lean.Localization.LeanLocalization.GetTranslationText("Loading");
    }
}
