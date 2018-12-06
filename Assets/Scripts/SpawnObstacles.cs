using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{

    public Transform obstacleParent;
    public GameObject tableSidePrefab;
    public GameObject zombiePrefab;
    public float floorLength = 40f;
    public int numberOfObstacles = 40;
    public int spaceBetweenObstacles = 1;

    bool left = false;
    bool right = false;
    bool middle = false;

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
                //SpawnObjects();
                SpawnObjectsConsistent();

                break;
        }
    }

    void SpawnObjectsConsistent()
    {

        for (int i = spaceBetweenObstacles; i < floorLength; i += spaceBetweenObstacles)
        {
            int randomManyObstacles = (int)(Random.Range(1f, 2.99f));
            int spawningObstaclesAt = (int)(Random.Range(0f, 2.99f));

            for (int j = 0; j < 3; j++)
            {
                switch (spawningObstaclesAt)
                {
                    case 0:
                        if (left == false)
                        {
                            if (randomManyObstacles > 0)
                            {
                                left = true;
                                randomManyObstacles--;
                                InstantiateSomething(spawningObstaclesAt, i);
                            }
                        }
                        else if (j == 2)
                        {
                            if (randomManyObstacles > 0)
                            {
                                left = true;
                                randomManyObstacles--;
                                InstantiateSomething(spawningObstaclesAt, i);
                            }
                        }
                        else
                        {
                            left = false;
                        }
                        spawningObstaclesAt++;
                        break;
                    case 1:
                        if (middle == false)
                        {
                            if (randomManyObstacles > 0)
                            {
                                middle = true;
                                randomManyObstacles--;
                                InstantiateSomething(spawningObstaclesAt, i);
                            }

                        }
                        else if (j == 2)
                        {
                            if (randomManyObstacles > 0)
                            {
                                middle = true;
                                randomManyObstacles--;
                                InstantiateSomething(spawningObstaclesAt, i);
                            }
                        }
                        else
                        {
                            middle = false;
                        }
                        spawningObstaclesAt++;
                        break;
                    case 2:
                        if (right == false)
                        {
                            if (randomManyObstacles > 0)
                            {
                                right = true;
                                randomManyObstacles--;
                                InstantiateSomething(spawningObstaclesAt, i);
                            }
                        }
                        else if (j == 2)
                        {
                            if (randomManyObstacles > 0)
                            {
                                right = true;
                                randomManyObstacles--;
                                InstantiateSomething(spawningObstaclesAt, i);
                            }
                        }
                        else
                        {
                            right = false;
                        }
                        spawningObstaclesAt = 0;
                        break;
                    default:
                        break;
                }
            }

        }

    }

    void InstantiateSomething(float xLocation, float yLocation)
    {
        if (Random.Range(0f, 1f) < 0.2f)
        {
            Instantiate(zombiePrefab,
                    new Vector3(startOfFloor.x + xLocation, -0.5f, startOfFloor.z + yLocation),
                    zombiePrefab.transform.localRotation,
                    obstacleParent);
        }
        else
        {
            Instantiate(tableSidePrefab,
                    new Vector3(startOfFloor.x + xLocation, 0f, startOfFloor.z + yLocation + 0.5f),
                    tableSidePrefab.transform.localRotation,
                    obstacleParent);
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
            if (placedObstacles[randx, randy] == false)
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
                    Instantiate(tableSidePrefab,
                            new Vector3(startOfFloor.x + randx, -0.5f, startOfFloor.z + randy),
                            tableSidePrefab.transform.localRotation,
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
            if (i % 2 == 0)
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
