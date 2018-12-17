using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
	public GameObject prefab;
	
	public int size = 4;
		
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				var clone = Instantiate(prefab, new Vector3(i, j, 0), Quaternion.identity);
				clone.name = "tile x" + i + " y" + j;
			}				
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
