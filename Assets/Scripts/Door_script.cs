using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Door_script : MonoBehaviour
{
    public Transform exit;
    public TextMeshProUGUI text;

    public float alpha = 0f;
    public float fadeSpeed = 0.5f;
    public bool fading;

    // Start is called before the first frame update
    void Start()
    {
        fading = true;
    }

    // Update is called once per frame
    void Update()
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
        }
    }
}
