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
            var sideLength = cellPrefab.Collider.bounds.size.x;
            Debug.Log(sideLength);

            for (int rowIndex = 0; rowIndex < size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < size; columnIndex++)
                {
                    var position = new Vector3(rowIndex * sideLength, columnIndex * sideLength, 0);

                    var clone = Instantiate(cellPrefab, position, Quaternion.identity);
                    clone.name = "Cell x" + rowIndex + " y" + columnIndex;
                    clone.Row = rowIndex;
                    clone.Column = columnIndex;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
