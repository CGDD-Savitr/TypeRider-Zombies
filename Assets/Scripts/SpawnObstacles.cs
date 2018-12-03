using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour {

	public Transform obstacleParent;
	public GameObject tablePrefab;
	public GameObject zombiePrefab;
	public float floorLength = 40f;
	public int numberOfObstacles = 40;

	private Vector3 startOfFloor;
    private bool[,] placedObstacles;

    private void Start()
    {
        placedObstacles = new bool[3, (int)floorLength];
    }

    void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "FloorTrigger":
				startOfFloor = other.transform.position;
                startOfFloor.x -= 1;
				SpawnObjects();
				break;
		}
	}

	void SpawnObjects()
	{
        // setting all values to false
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < floorLength; j++)
            {
                placedObstacles[i, j] = false;
            }
        }
        clearPath();

        for (int i = 0; i < numberOfObstacles; i++)
        {
            int randx = (int)(Random.Range(0f, 2.99f));
            int randy = (int)(Random.Range(3f, floorLength - 0.01f));
            // to prevent 2 objects being added on the same square
            if (placedObstacles[randx,randy] == false)
            {
                placedObstacles[randx, randy] = true;
				
				//20% chance of zombie, 80% chance of table
				if (Random.Range(0f, 1f) < 0.2f)
				{
					Instantiate(zombiePrefab,
							new Vector3(startOfFloor.x + randx, -0.5f, startOfFloor.z + randy),
							zombiePrefab.transform.localRotation,
							obstacleParent);
				}
				else
				{
					Instantiate(tablePrefab,
							new Vector3(startOfFloor.x + randx, -0.5f, startOfFloor.z + randy),
							tablePrefab.transform.localRotation,
							obstacleParent);
				}
				
            }
            
        }
	}

    void clearPath()
    {
        int lane = 1;

        for (int i = 0; i < floorLength; i++)
        {
            placedObstacles[lane, i] = true;
            if (i%2 == 0)
            {
                // 50-50 chance that he takes action
                if ((int)(Random.Range(0f, 1.99f)) == 1)
                {
                    // see which action he takes
                    int action = (int)Random.Range(0f, 1.99f);
                    // if he picks 1 means right
                    if (action == 1)
                    {
                        // make sure he can't go outside of rightmos lane
                        if (lane < 2)
                        {
                            lane++;
                        }
                    }
                    else //else left
                    {
                        if (lane > 0)
                        {
                            lane--;
                        }
                    }
                    placedObstacles[lane, i] = true;
                }
            }
        }
    }
}
