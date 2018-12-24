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

    void Start()
    {
        var winner = PlayerPrefs.GetString("Winner");
        
        hotWinnerSprite.gameObject.SetActive(winner == TeamSide.Hot.ToString());
        coldWinnerSprite.gameObject.SetActive(winner == TeamSide.Cold.ToString());
        tieSprite.gameObject.SetActive(winner == "Tie");
    }

    void Update()
    {
        if (Input.GetButtonDown("MenuValid"))
        {
            SceneManager.LoadScene(1);
        }
    }
}