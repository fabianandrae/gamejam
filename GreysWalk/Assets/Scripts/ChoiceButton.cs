using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public void Setup(string choiceText, System.Action onClickAction)
    {
        var textComponent = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textComponent.text = choiceText;

        var button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() => onClickAction());
    }
}
