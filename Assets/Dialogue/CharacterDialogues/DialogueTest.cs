using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialogue;

public class DialogueTest : MonoBehaviour
{
    private void Start()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(FuncTester());
    }

    private DialogueSection FuncTester()
    {
        string localname = "Gabby";

        Monologue yes = new Monologue(localname, "Well then let's get goin'. I can't wait to shoot someone in the face");

        Monologue no = new Monologue(localname, "Aww c'mon, Boss. You are way too cautious");

        Choices b = new Choices(localname, "We gonna take on the Job?", ChoiceList(Choice("Hell yes!", yes), Choice("No, it's way too dangerous", no)));

        Monologue a = new Monologue(localname, "Hey pipsqueak", b);

        return a;
    }
}
