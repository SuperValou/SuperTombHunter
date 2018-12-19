using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Teams;
using UnityEngine;

public class ScoreRecap : MonoBehaviour
{
    public SpriteRenderer hotWinnerSprite;
    public SpriteRenderer coldWinnerSprite;

    // Start is called before the first frame update
    void Start()
    {
        var winner = PlayerPrefs.GetString("Winner");
        if (winner == TeamSide.Hot.ToString())
        {
            coldWinnerSprite.gameObject.SetActive(false);
        }
        else
        {
            hotWinnerSprite.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}