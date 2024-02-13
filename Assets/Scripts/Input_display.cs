using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Input_display : MonoBehaviour
{

    public Image wImage;
    public Image aImage;
    public Image sImage;
    public Image dImage;
    public TextMeshProUGUI wText;
    public TextMeshProUGUI aText;
    public TextMeshProUGUI sText;
    public TextMeshProUGUI dText;
    public float wAlpha;
    public float aAlpha;
    public float sAlpha;
    public float dAlpha;
    // Start is called before the first frame update
    void Start()
    {
        wAlpha = 0.3f;
        aAlpha = 0.3f;
        sAlpha = 0.3f;
        dAlpha = 0.3f;

        wImage.color = new Color(1f, 1f, 1f, wAlpha);
        wText.color = new Color(0f, 0f, 0f, wAlpha);

        aImage.color = new Color(1f, 1f, 1f, aAlpha);
        aText.color = new Color(0f, 0f, 0f, aAlpha);

        sImage.color = new Color(1f, 1f, 1f, sAlpha);
        sText.color = new Color(0f, 0f, 0f, sAlpha);

        dImage.color = new Color(1f, 1f, 1f, dAlpha);
        dText.color = new Color(0f, 0f, 0f, dAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wAlpha = 1f;
            wImage.color = new Color(1f, 1f, 1f, wAlpha);
            wText.color = new Color(0f, 0f, 0f, wAlpha);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            wAlpha = 0.3f;
            wImage.color = new Color(1f, 1f, 1f, wAlpha);
            wText.color = new Color(0f, 0f, 0f, wAlpha);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            aAlpha = 1f;
            aImage.color = new Color(1f, 1f, 1f, aAlpha);
            aText.color = new Color(0f, 0f, 0f, aAlpha);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            aAlpha = 0.3f;
            aImage.color = new Color(1f, 1f, 1f, aAlpha);
            aText.color = new Color(0f, 0f, 0f, aAlpha);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sAlpha = 1f;
            sImage.color = new Color(1f, 1f, 1f, sAlpha);
            sText.color = new Color(0f, 0f, 0f, sAlpha);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            sAlpha = 0.3f;
            sImage.color = new Color(1f, 1f, 1f, sAlpha);
            sText.color = new Color(0f, 0f, 0f, sAlpha);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dAlpha = 1f;
            dImage.color = new Color(1f, 1f, 1f, dAlpha);
            dText.color = new Color(0f, 0f, 0f, dAlpha);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            dAlpha = 0.3f;
            dImage.color = new Color(1f, 1f, 1f, dAlpha);
            dText.color = new Color(0f, 0f, 0f, dAlpha);
        }
    }
}
