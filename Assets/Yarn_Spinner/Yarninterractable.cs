using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Yarninterractable : MonoBehaviour
{
    // this file is attached to every character in the scene and so will affect only
    // the targeted character object when functions are called
    [SerializeField] private string conversationStartNode;

    private DialogueRunner dialogueRunner;
    private bool interactable = true;
    private bool isCurrentConversation = false;

    public void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    public void OnMouseDown()
    {
        if (interactable && !dialogueRunner.IsDialogueRunning)
        {
            StartConversation();
        }
    }

    // disable scene interaction, activate speaker indicator, and
    // run dialogue from {conversationStartNode}
    private void StartConversation()
    {
        Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        // TODO *begin animation or turn on speaker indicator or whatever* HERE
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    // reverse StartConversation's changes: 
    // re-enable scene interaction, deactivate indicator, etc.
    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            Debug.Log($"Ending Conversation with {name}.");
            // TODO *stop animation or turn off indicator or whatever* HERE
            isCurrentConversation = false;
        }
    }

    // make character not able to be clicked on
    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

}
