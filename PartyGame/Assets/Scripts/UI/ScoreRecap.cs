using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Teams;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreRecap : MonoBehaviour
{
    public SpriteRenderer hotWinnerSprite;
    public SpriteRenderer coldWinnerSprite;
    public SpriteRenderer tieSprite;
    public SpriteRenderer tutoSprite;

    private readonly string[] _buttons = new[] { "P0_Grab", "P1_Grab", "P2_Grab", "P3_Grab" };

    // Start is called before the first frame update
    void Start()
    {
        var winner = PlayerPrefs.GetString("Winner");
        
        hotWinnerSprite.gameObject.SetActive(winner == TeamSide.Hot.ToString());
        coldWinnerSprite.gameObject.SetActive(winner == TeamSide.Cold.ToString());
        tieSprite.gameObject.SetActive(winner == "Tie");
    }

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