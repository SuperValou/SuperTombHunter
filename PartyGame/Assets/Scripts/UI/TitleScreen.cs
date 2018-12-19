using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public SpriteRenderer tutoSprite;
    
    void Update()
    {
        if (Input.GetButtonDown("MenuValid"))
        {
            if (tutoSprite.isVisible == false)
            {
                tutoSprite.gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
