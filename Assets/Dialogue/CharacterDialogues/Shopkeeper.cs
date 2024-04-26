using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialogue;

public class Shopkeeper : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float alpha = 0f;
    public float fadeSpeed = 0.5f;
    public bool fading;
    public bool player = false;
    public bool saidYes = false;
    public bool saidNo = false;
    public Player_movement playerScript;
    public ChoicesMade choices;
    private void Start()
    {
        player = false;
        fading = true;
        choices = GameObject.Find("ChoicesObj").GetComponent<ChoicesMade>();
    }

    private void Update()
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

        if (player && Input.GetKeyDown(playerScript.talk))
        {
            Activate();
        }
    }

    private DialogueSection FuncTester1()
    {
        string localname = "Shopkeeper";
        
        Monologue eugh = new Monologue(localname, "blah");

        Monologue fuck = new Monologue(localname, "blah");

        Choices b = new Choices(localname, "blah!", ChoiceList(Choice("blah", fuck), Choice("eugh", eugh)));

        Monologue a = new Monologue(localname, "blahbalgvlag", b);

        return a;
    }

    private DialogueSection FuncTester2()
    {
        string localname = "Gabby";

        Monologue a = new Monologue(localname, "I have nothing else to say");

        return a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fading = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fading = true;
            playerScript = null;
            player = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = true;
            playerScript = collision.gameObject.GetComponent<Player_movement>();
        }
    }

    private void Activate()
    {
        if (choices.talkedToGabby == false)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(FuncTester1());
            choices.talkedToGabby = true;
        }
        else
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(FuncTester2());
        }
    }

}