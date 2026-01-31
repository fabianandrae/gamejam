using TMPro;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Dialog Settings")]
    public string npcName;
    public DialogNode StartNode;
    public DialogNode DefaultNode;
    public bool isDone = false;
}
