using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class TileGenerator : MonoBehaviour
    {
        private Random _random = new Random();

        public Tile sunPrefab;
        public Tile moonPrefab;

        public Grid grid;

        public int period = 1;

        void Start()
        {
            if (grid == null)
            {
                Debug.LogError($"You forgot to assing the {nameof(Grid)} to the {nameof(TileGenerator)}.");
                return;
            }

            if (sunPrefab == null)
            {
                Debug.LogError($"You forgot to assing the {nameof(sunPrefab)} to the {nameof(TileGenerator)}.");
                return;
            }

            if (moonPrefab == null)
            {
                Debug.LogError($"You forgot to assing the {nameof(moonPrefab)} to the {nameof(TileGenerator)}.");
                return;
            }

            StartCoroutine(GenerateTiles());
        }

        private IEnumerator GenerateTiles()
        {
            while (true)
            {
                yield return new WaitForSeconds(period);
                Tile selectedTile = _random.Next(1) == 0 
                    ? moonPrefab 
                    : sunPrefab;

                var position = UnityEngine.Random.insideUnitCircle + Vector2.left * 2;
                var clone = Instantiate(selectedTile, position, Quaternion.identity);
                clone.SetGrid(grid);
            }
        }
    }
}