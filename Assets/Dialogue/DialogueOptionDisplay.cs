using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Dialogue;

public class DialogueOptionDisplay : MonoBehaviour
{
    public TextMeshProUGUI contentText;
    public DialogueSection leadsTo;

    private DialogueManager manager;

    private void Start()
    {
        manager = FindAnyObjectByType<DialogueManager>();
    }

    public void SetDisplay(string optionText, DialogueSection nextDialogueSection)
    {
        contentText.text = optionText;
        leadsTo = nextDialogueSection;
    }

    public void ProceedOnClick()
    {
        if (manager.displayingChoices)
        {
            return;
        }

        manager.CurrentSection = leadsTo;
        manager.DisplayDialogue();
    }
}
