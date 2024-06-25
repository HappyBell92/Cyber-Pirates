using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Yarn.Unity;

public class Yarninterractable : MonoBehaviour
{
    // this file is attached to every character in the scene and so will affect only
    // the targeted character object when functions are called
    [SerializeField] private string conversationStartNode;

    //public Rigidbody rb;
    private DialogueRunner dialogueRunner;
    public bool interactable = true;
    private bool isCurrentConversation = false;

    public Transform exit;
    public TextMeshProUGUI text;
    public Player_movement playerScript;

    public float alpha = 0f;
    public float fadeSpeed = 0.5f;
    public bool fading;

    public KeyCode talk = KeyCode.E;

    public void Start()
    {
        fading = true;
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    //public void OnMouseDown()
    //{
    //    if (interactable && !dialogueRunner.IsDialogueRunning)
    //    {
    //        StartConversation();
    //    }
    //}

    public void Update()
    {
        if (!fading)
        {
            alpha = 1f;
            text.color = new Color(1f, 1f, 1f, alpha);
        }

        if (fading)
        {
            alpha = 0f;
            text.color = new Color(1f, 1f, 1f, alpha);
        }
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (interactable && !dialogueRunner.IsDialogueRunning && Input.GetKey(talk))
    //        {
    //            StartConversation();
    //        }
    //    }
        
    //}

    // disable scene interaction, activate speaker indicator, and
    // run dialogue from {conversationStartNode}
    public void StartConversation()
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
            playerScript.talking = false;
        }
    }

    // make character not able to be clicked on
    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript = collision.gameObject.GetComponent<Player_movement>();
            fading = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript = null;
            fading = true;
        }
    }

}
