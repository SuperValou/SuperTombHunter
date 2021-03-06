﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Teams
{
    public class TeamManager : MonoBehaviour
    {
        public Team hotTeam;
        public Team coldTeam;

        public Transform hotTeamStartLocation;
        public Transform coldTeamStartLocation;

        private List<Player> _hotTeam = new List<Player>();
        private List<Player> _coldTeam = new List<Player>();

        public void AddPlayer(int playerNumber, Player player)
        {
            if (playerNumber % 2 == 0)
            {
                player.Team = coldTeam;
                _coldTeam.Add(player);
            }
            else
            {
                player.Team = hotTeam;
                _hotTeam.Add(player);
            }
        }

        public void SetTeamPositions()
        {
            for (int i = 0; i < _hotTeam.Count; i++)
            {
                _hotTeam[i].transform.position = hotTeamStartLocation.transform.position + Vector3.up * i;
            }

            for (int i = 0; i < _coldTeam.Count; i++)
            {
                _coldTeam[i].transform.position = coldTeam.transform.position - Vector3.down * i;
            }
        }

        public void SetWinner()
        {
            if (hotTeam.Score > coldTeam.Score)
            {
                PlayerPrefs.SetString("Winner", TeamSide.Hot.ToString());
            }
            else if (hotTeam.Score < coldTeam.Score)
            {
                PlayerPrefs.SetString("Winner", TeamSide.Cold.ToString());
            }
            else
            {
                PlayerPrefs.SetString("Winner", "Tie");
            }
        }
    }
}