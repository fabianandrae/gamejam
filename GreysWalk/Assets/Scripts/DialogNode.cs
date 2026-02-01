using UnityEngine;

[CreateAssetMenu(fileName = "DialogNode", menuName = "Dialog/Dialog Node")]
public class DialogNode : ScriptableObject
{
    [TextArea(3, 10)]
    public string text;
    public DialogChoice[] choices; // Wenn leer -> einfach weiter
    public DialogNode nextNode; // Für lineare Dialoge
}
[System.Serializable]
public class DialogChoice
{
    public string choiceText;
    public DialogNode nextNode;
}
