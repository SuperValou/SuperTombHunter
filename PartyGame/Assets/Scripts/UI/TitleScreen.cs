using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private readonly string[] _buttons = new []{ "P0_Grab", "P1_Grab", "P2_Grab", "P3_Grab"} ;
    
    void Update()
    {
        foreach (var button in _buttons)
        {
            if (Input.GetButtonDown(button))
            {
                Debug.Log(button);
                SceneManager.LoadScene(1);
            }
        }
    }
}
