using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Teams
{
    public class Team : MonoBehaviour
    {
        public TeamSide TeamSide;

        public Text ScoreLabel;

        public int Score { get; set; }
    }
}