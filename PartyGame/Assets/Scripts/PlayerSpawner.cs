﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private const int MaxPlayerCount = 4;

    public MoveController PlayerPrefab;

    void Start()
    {
        string[] joysticks = Input.GetJoystickNames();

        for (int i = 0; i < joysticks.Length; i++)
        {
            Debug.Log(i + " => " + joysticks[i]);
        }

        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        string[] joysticks = Input.GetJoystickNames();

        for (int i = 0; i < joysticks.Length; i++)
        {
            if (joysticks[i].Length == 0) continue;

            MoveController player = Instantiate(PlayerPrefab);

            player.JoystickNumber = i;
            player.InitAxis();

            Debug.Log("Spawning player with joystick " + player.JoystickNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
