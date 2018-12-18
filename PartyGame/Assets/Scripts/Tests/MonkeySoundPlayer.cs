using Assets.Scripts.AudioManagement;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class MonkeySoundPlayer : MonoBehaviour
    {
        public SoundsManager soundsManager;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                soundsManager.Play(SoundName.TakeTile);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                soundsManager.Play(SoundName.DropTile);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                soundsManager.Play(SoundName.TileSpawn);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                soundsManager.Play(SoundName.ScorePoint);
            }
        }
    }
}