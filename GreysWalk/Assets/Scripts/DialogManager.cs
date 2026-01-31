using System;
using System.Collections;
using StarterAssets;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public GameObject dialogPanel;
    public GameObject choiceButtonPrefab;
    public Transform choiceParent;

    private DialogNode currentNode;
    private DialogNode nextNode;
    private bool isTyping = false;
    private StarterAssetsInputs _input;

    void Awake() => Instance = this;

    void Start()
    {
        _input = FindFirstObjectByType<StarterAssetsInputs>();
    }

    void Update()
    {   
        if(currentNode == null) return;
        
        if(_input.interact && !isTyping && !_input.isUIMode)
        {
                ContinueDialogue(nextNode);
        }
    }

    public void StartDialog(NPCInteraction npc) {
        _input.isInDialog = true;
        dialogPanel.SetActive(true);
        nameText.text = npc.npcName;
        if(npc.isDone)
        {
            DisplayNode(npc.DefaultNode);
        } else {
            npc.isDone = true;
            DisplayNode(npc.StartNode);
        }
    }

    private void ContinueDialogue(DialogNode node) {
        if(node != null){
            DisplayNode(node);
        } else {
            FinishDialogue();
        }
        
    }

    public void DisplayNode(DialogNode node) {
        currentNode = node;
        nextNode = node.nextNode;
        StopAllCoroutines();
        StartCoroutine(TypeText(node.text));
        
        // Buttons aufräumen
        foreach (Transform child in choiceParent) Destroy(child.gameObject);
        choiceParent.gameObject.SetActive(false);
        _input.SetUIMode(false);
    }

    IEnumerator TypeText(string text) {
        isTyping = true;
        dialogText.text = "";
        foreach (char c in text.ToCharArray()) {
            dialogText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
        // Wenn fertig getippt, zeige Auswahlmöglichkeiten (falls vorhanden)
        if (currentNode.choices.Length > 0) {
            ShowChoices();
        }
    }

    void ShowChoices() {
        choiceParent.gameObject.SetActive(true);
        _input.isUIMode = true;
        _input.SetUIMode(true);
        
        foreach (var choice in currentNode.choices) {
            var btn = Instantiate(choiceButtonPrefab, choiceParent).GetComponent<ChoiceButton>();
            btn.Setup(choice.choiceText, () => DisplayNode(choice.nextNode) );
        }
    }

    public void FinishDialogue()
    {
        dialogPanel.SetActive(false);
        _input.isInDialog = false;
        _input.SetUIMode(false);
        _input.interact = false;
        currentNode = null;
        nextNode = null;
    }
}
