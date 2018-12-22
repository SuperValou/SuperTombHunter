using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Teams;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;

public class PlayerSpawner : MonoBehaviour
{
    private const int MaxPlayerCount = 4;
    private int playerCount = 0;

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

        for (int i = 0; i < joysticks.Length && playerCount < MaxPlayerCount; i++)
        {
            if (joysticks[i].Length == 0) continue;

            SpawnPlayer(ControllerType.Pad, i);
            Debug.Log("Spawning player " + playerCount + " with joystick " + i);
        }

        if (playerCount < MaxPlayerCount)
        {
            SpawnPlayer(ControllerType.Keyboard);
            Debug.Log("Spawning player " + playerCount + " with Keyboard");
        }

        teamManager.SetTeamPositions();
    }

    private void SpawnPlayer(ControllerType type, int joystickNumber = 0)
    {
        MoveController player = Instantiate(PlayersPrefab[playerCount]);

        player.InitAxis(type, joystickNumber);
        player.player.Initialize(grid, soundManager);
        teamManager.AddPlayer(playerCount, player.player);

        playerCount++;
    }
}

