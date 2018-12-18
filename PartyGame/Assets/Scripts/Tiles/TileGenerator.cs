using System.Collections;
using System.Linq;
using Assets.Scripts.AudioManagement;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;
using Random = System.Random;

namespace Assets.Scripts.Tiles
{
    public class TileGenerator : MonoBehaviour
    {
        private readonly Random _random = new Random();

        public Tile sunPrefab;
        public Tile moonPrefab;

        public Grid grid;

        public TileSpawnSlot[] spwanSlots;

        public float period = 2;

        public SoundsManager soundsManager;

        void Start()
        {
            if (grid == null)
            {
                Debug.LogError($"You forgot to assign the {nameof(Grid)} to the {nameof(TileGenerator)}.");
                return;
            }

            if (sunPrefab == null)
            {
                Debug.LogError($"You forgot to assign the {nameof(sunPrefab)} to the {nameof(TileGenerator)}.");
                return;
            }
            
            if (moonPrefab == null)
            {
                Debug.LogError($"You forgot to assign the {nameof(moonPrefab)} to the {nameof(TileGenerator)}.");
                return;
            }

            StartCoroutine(GenerateTiles());
        }

        private IEnumerator GenerateTiles()
        {
            while (true)
            {
                yield return new WaitForSeconds(period);

                var availableSpawnSlots = spwanSlots.Where(s => s.IsAvailable).ToList();
                if (availableSpawnSlots.Count == 0)
                {
                    continue;
                }

                // select slot
                int spawnSlotIndex = _random.Next(availableSpawnSlots.Count);
                var selectedSpawnSlot = availableSpawnSlots[spawnSlotIndex];

                // select prefab
                Tile selectedPrefab;
                if (_random.Next(0, 2) == 0)
                {
                    selectedPrefab = sunPrefab;
                }
                else
                {
                    selectedPrefab = moonPrefab;
                }

                // instantiate
                var instantiatedTile = Instantiate(selectedPrefab, selectedSpawnSlot.SpawnLocation.position, Quaternion.identity);
                instantiatedTile.Initialize(grid);
                instantiatedTile.gameObject.SetActive(true);
                selectedSpawnSlot.SetTile(instantiatedTile);

                //soundsManager.Play(SoundName.TileSpawn);
            }
        }
    }
}