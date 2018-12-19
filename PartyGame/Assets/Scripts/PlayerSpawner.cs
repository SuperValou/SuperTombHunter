using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Teams;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;

public class PlayerSpawner : MonoBehaviour
{
    private const int MaxPlayerCount = 4;

    public MoveController[] PlayersPrefab = new MoveController[MaxPlayerCount];

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
        int playerCount = 0;

        for (int i = 0; i < joysticks.Length; i++)
        {
            if (joysticks[i].Length == 0) continue;

            MoveController player = Instantiate(PlayersPrefab[playerCount]);

            player.JoystickNumber = i;
            player.InitAxis();

            Debug.Log("Spawning player " + playerCount + "with joystick " + player.JoystickNumber);
            playerCount++;

            player.player.Initialize(grid, soundManager);
            teamManager.AddPlayer(i, player.player);
        }

        teamManager.SetTeamPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

