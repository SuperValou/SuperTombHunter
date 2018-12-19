using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private readonly string[] _buttons = new []{ "P1_Grab", "P2_Grab", "P3_Grab", "P4_Grab"} ;

    void Start()
    {
        
    }

    void Update()
    {
        foreach (var button in _buttons)
        {
            if (Input.GetButton(button))
            {
                Debug.Log(button);
                SceneManager.LoadScene(1);
            }
        }
    }
}
