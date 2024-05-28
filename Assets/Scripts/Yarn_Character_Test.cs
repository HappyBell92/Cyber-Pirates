using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarn_Character_Test : MonoBehaviour
{

    public GameObject talker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            talker.SetActive(true);
        }
    }
}
