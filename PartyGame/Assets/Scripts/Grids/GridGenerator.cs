using UnityEngine;

namespace Assets.Scripts.Grids
{
    public class GridGenerator : MonoBehaviour
    {
        public Cell cellPrefab;
	
        public int size = 4;
		
        // Start is called before the first frame update
        void Start()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var clone = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    clone.name = "Cell x" + x + " y" + y;
                    clone.Row = x;
                    clone.Column = y;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
