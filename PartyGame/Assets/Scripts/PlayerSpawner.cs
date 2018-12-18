using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Teams;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;

public class PlayerSpawner : MonoBehaviour
{
    private const int MaxPlayerCount = 4;

    public MoveController PlayerPrefab;

    public Grid grid;
    public SoundsManager soundManager;
    public TeamManager teamManager;

    void Start()
    {
        if (grid == null)
        {
            Debug.LogError("Grid is not set to " + nameof(PlayerSpawner));
        }

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

            player.player.Initialize(grid, soundManager);
            teamManager.AddPlayer(i, player.player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

