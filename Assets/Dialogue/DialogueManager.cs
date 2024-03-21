using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Dialogue;
using JetBrains.Annotations;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public DialogueSection CurrentSection;

    [Header("TExt Components")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentsText;

    [Header("Dialogue Choice")]
    public GameObject proceedConversationObject;
    public GameObject dialogueChoiceObject;
    public Transform parentChoicesTo;

    [Header("Fade")]
    public float canvasGroupFadeTime = 5f;
    public bool canvasGroupDisplaying;
    public CanvasGroup dialogueCanvasGroup;

    private void Start()
    {
        InitializePanel();
    }

    private void InitializePanel()
    {
        dialogueCanvasGroup.alpha = 0f;
    }

    private void Update()
    {
        UpdateCanvasOpacity();
        PrepareForOptionDisplay();
        DisplayDialogueOptions();
    }

    private void UpdateCanvasOpacity()
    {
        dialogueCanvasGroup.alpha = Mathf.Lerp(dialogueCanvasGroup.alpha, canvasGroupDisplaying ? 1f : 0f, Time.deltaTime * canvasGroupFadeTime);
    }

    private void PrepareForOptionDisplay()
    {
        if (optionsBeenDisplayed)
        {
            return;
        }

        if (typeof(Choices).IsInstanceOfType(CurrentSection))
        {
            ResetDisplayOptionsFlags();
        }
    }

    public void StartDialogue(DialogueSection start)
    {
        canvasGroupDisplaying = true;
        ClearAllOptions();
        CurrentSection = start;
        DisplayDialogue();
    }

    public void ProceedToNext()
    {
        if (displayingChoices)
        {
            return;
        }

        if(CurrentSection.GetNextSection() != null)
        {
            CurrentSection = CurrentSection.GetNextSection();
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void DisplayDialogue()
    {
        if (CurrentSection == null)
        {
            EndDialogue();
            return;
        }

        bool isMonologue = typeof(Monologue).IsInstanceOfType(CurrentSection);

        proceedConversationObject.SetActive(isMonologue);

        ClearAllOptions();

        DisplayText();
    }

    private void DisplayText()
    {
        optionsBeenDisplayed = false;

        nameText.text = CurrentSection.GetSpeakerName();
        contentsText.text = CurrentSection.GetSpeechContents();
    }

    private void EndDialogue()
    {
        canvasGroupDisplaying = false;
        ClearAllOptions();
    }

    private void ClearAllOptions()
    {
        GameObject[] currentDialogueOptions = GameObject.FindGameObjectsWithTag("DialogueChoice");

        foreach (var entry in currentDialogueOptions)
        {
            Destroy(entry);
        }
    }

    int indexOfCurrentChoice = 0;
    [HideInInspector] public bool displayingChoices;
    private bool optionsBeenDisplayed;

    public void ResetDisplayOptionsFlags()
    {
        optionsBeenDisplayed = true;
        displayingChoices = true;
        indexOfCurrentChoice = 0;

    }

    public void DisplayDialogueOptions()
    {
        if (!typeof(Choices).IsInstanceOfType(CurrentSection))
        {
            return;
        }

        Choices choices = (Choices)CurrentSection;

        if (displayingChoices)
        {
            if(indexOfCurrentChoice <  choices.choices.Count)
            {
                Tuple<string, DialogueSection> option = choices.choices[indexOfCurrentChoice];

                GameObject s = Instantiate(dialogueChoiceObject, Vector3.zero, Quaternion.identity);
                s.transform.SetParent(parentChoicesTo);
                s.GetComponent<RectTransform>().localScale = Vector3.one;

                DialogueOptionDisplay optionDisplayBehavior = s.GetComponent<DialogueOptionDisplay>();

                optionDisplayBehavior.SetDisplay(option.Item1, option.Item2);

                indexOfCurrentChoice++;
            }
            else
            {
                displayingChoices = false;
            }
        }
    }
}
