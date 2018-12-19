using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Teams;
using UnityEngine;

public class ScoreRecap : MonoBehaviour
{
    public SpriteRenderer hotWinnerSprite;
    public SpriteRenderer coldWinnerSprite;
    public SpriteRenderer tieSprite;

    // Start is called before the first frame update
    void Start()
    {
        var winner = PlayerPrefs.GetString("Winner");
        
        hotWinnerSprite.gameObject.SetActive(winner == TeamSide.Hot.ToString());
        coldWinnerSprite.gameObject.SetActive(winner == TeamSide.Cold.ToString());
        tieSprite.gameObject.SetActive(winner == "Tie");
    }
}