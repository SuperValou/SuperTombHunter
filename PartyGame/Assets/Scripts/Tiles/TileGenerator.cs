using System.Collections;
using UnityEngine;
using Grid = Assets.Scripts.Grids.Grid;
using Random = System.Random;

namespace Assets.Scripts.Tiles
{
    public class TileGenerator : MonoBehaviour
    {
        private Random _random = new Random();

        public Tile sunPrefab;
        public Tile moonPrefab;

        public Grid grid;

        public int period = 3;

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

                var position = UnityEngine.Random.insideUnitCircle * 1.5f + Vector2.left * 4;
                Tile selectedPrefab;

                if (_random.Next(0, 2) == 0)
                {
                    selectedPrefab = sunPrefab;
                }
                else
                {
                    selectedPrefab = moonPrefab;
                }

                var clone = Instantiate(selectedPrefab, position, Quaternion.identity);
                clone.Initialize(grid);
                clone.gameObject.SetActive(true);
            }
        }
    }
}