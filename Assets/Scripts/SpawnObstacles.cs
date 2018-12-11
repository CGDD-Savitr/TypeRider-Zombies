using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{

	public Transform obstacleParent;
    public Transform powerUpParent;
	public GameObject tableSidePrefab;
	public GameObject zombiePrefab;
	public GameObject topObstaclePrefab;
    public GameObject powerUpOne;
    public GameObject powerUpTwo;
    public GameObject powerUpThree;
    public float floorLength = 40f;
	public int numberOfObstacles = 40;
	public int spaceBetweenObstacles = 1;
    public float chanceOfPowerUp = 0.125f;


	bool[] lastOpenSpots;
	bool[] currentOpenSpots;
	bool left = false;
	bool right = false;
	bool middle = false;

	private Vector3 startOfFloor;
	private bool[,] placedObstacles;

	private void Start()
	{
		placedObstacles = new bool[3, (int)floorLength];
		lastOpenSpots = new bool[6];
		for (int i = 0; i < 6; i++) // making all spots open
		{
			lastOpenSpots[i] = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "FloorTrigger":
				startOfFloor = other.transform.position;
				startOfFloor.x -= 1;
				//SpawnObjects();
				SpawnObjectsConsistent2Floors();

				break;
		}
	}

	string createStringFromBoolArr(bool[] obstacles)
	{
		string toRet = "";
		for (int i = 0; i < obstacles.Length; i++)
		{
			if (obstacles[i] == true)
			{
				toRet = toRet + "O";
			}
			else
			{
				toRet = toRet + "X";
			}
		}
		return toRet;
	}

	void SpawnObjectsConsistent2Floors()
	{
		currentOpenSpots = new bool[6];
		for (int i = 0; i < 6; i++) // making all spots open
		{
			currentOpenSpots[i] = true;
		}

        for (int i = (int) (spaceBetweenObstacles / 2f); i < floorLength; i+= spaceBetweenObstacles)
        {
            // see if we want to spawn a power up
            if (Random.Range(0f, 1.00f) < chanceOfPowerUp)
            {
                // which powerup
                float whichPowerUp = Random.Range(0f, 1.00f);
                int whichX = (int)Random.Range(0f, 2.99f);
                // spawning powerup1
                if (whichPowerUp < 0.33)
                {
                    InstantiatePowerUp(whichX, i, 0);
                }
                // spawning powerup2
                else if (whichPowerUp < 0.66)
                {
                    InstantiatePowerUp(whichX, i, 1);
                }
                // spawning powerup3
                else
                {
                    InstantiatePowerUp(whichX, i, 2);
                }
            }
        }

		for (int i = spaceBetweenObstacles; i < floorLength; i += spaceBetweenObstacles)
		{
			//string tempString = createStringFromBoolArr(lastOpenSpots);
			int randomManyObstacles = (int)(Random.Range(1f, 3.99f));
			int spawningObstaclesAt = (int)(Random.Range(0f, 2.99f));
			for (int j = 0; j < randomManyObstacles; j++)
			{
				trySpawnObstalce(spawningObstaclesAt, i);
				if (spawningObstaclesAt == 2)
				{
					spawningObstaclesAt = 0;
				}
				else
				{
					spawningObstaclesAt++;
				}
			}
			lastOpenSpots = (bool[])currentOpenSpots.Clone();
			for (int k = 0; k < 6; k++)
			{
				currentOpenSpots[k] = true;
			}
		}
	}

	// function that checks that if we place an obstacle (either lower, upper or both depending on whichObstacle
	// that the player can still pass the next obstacles in only one move
	bool canSpawnObstacleHere(int xLocation, int whichObstacle)
	{
		bool[] tempOpenSpots = (bool[])currentOpenSpots.Clone();

		if (whichObstacle == 0) //making both upper and lower not passable
		{
			tempOpenSpots[xLocation] = false;
			tempOpenSpots[xLocation + 3] = false;
		}
		else if (whichObstacle == 1) // making lower not passable
		{
			tempOpenSpots[xLocation] = false;
		}
		else if (whichObstacle == 2) // making upper not passable
		{
			tempOpenSpots[xLocation + 3] = false;
		}
		else
		{
			Debug.Log("whichObstacle is not 0, 1, 2");
			return false;
		}
		for (int i = 0; i < 6; i++)
		{
			if (lastOpenSpots[i] == true) //if the player passed through some obstacle he needs to be able to make it through this one in only one move
			{
				// doing first the corners
				if (i == 0) // left lower
				{
					if (!(tempOpenSpots[i] == true || tempOpenSpots[i + 1] == true || tempOpenSpots[i + 3] == true))
					{
						return false;
					}
				}
				else if (i == 2) // right lower
				{
					if (!(tempOpenSpots[i] == true || tempOpenSpots[i - 1] == true || tempOpenSpots[i + 3] == true))
					{
						return false;
					}
				}
				else if (i == 5) // right upper
				{
					if (!(tempOpenSpots[i] == true || tempOpenSpots[i - 1] == true || tempOpenSpots[i - 3] == true))
					{
						return false;
					}
				}
				else if (i == 3) // left upper
				{
					if (!(tempOpenSpots[i] == true || tempOpenSpots[i + 1] == true || tempOpenSpots[i - 3] == true))
					{
						return false;
					}
				}
				// doing the middle
				else if (i == 1) // lower middle
				{
					if (!(tempOpenSpots[i] == true || tempOpenSpots[i + 1] == true || tempOpenSpots[i - 1] == true || tempOpenSpots[i + 3] == true))
					{
						return false;
					}
				}
				else if (i == 4)
				{
					if (!(tempOpenSpots[i] == true || tempOpenSpots[i + 1] == true || tempOpenSpots[i - 1] == true || tempOpenSpots[i - 3] == true))
					{
						return false;
					}
				}
			}
		}


		return true;
	}

    void InstantiatePowerUp(int xLocation, int yLocation, int whichPowerUp)
    {
        GameObject whichPrefab;
        switch(whichPowerUp)
        {
            case 0:
                whichPrefab = powerUpOne;
                break;
            case 1:
                whichPrefab = powerUpTwo;
                break;
            case 2:
                whichPrefab = powerUpThree;
                break;
            default:
                return;
        }

        Instantiate(whichPrefab,
                    new Vector3(startOfFloor.x + xLocation, 0f, startOfFloor.z + yLocation),
                    whichPrefab.transform.localRotation,
                    powerUpParent);
    }

	void InstantiateAnObstacle(int xLocation, int yLocation, int typeOfObstacle)
	{
		if (typeOfObstacle == 0)//spawning zombie
		{
			Instantiate(zombiePrefab,
					new Vector3(startOfFloor.x + xLocation, -0.5f, startOfFloor.z + yLocation),
					zombiePrefab.transform.localRotation,
					obstacleParent);
		}
		else if (typeOfObstacle == 1) // spawning Lower obstacle
		{
			Instantiate(tableSidePrefab,
					new Vector3(startOfFloor.x + xLocation, 0f, startOfFloor.z + yLocation + 0.5f),
					tableSidePrefab.transform.localRotation,
					obstacleParent);
		}
		else if (typeOfObstacle == 2) // spawning UpperObstacle
		{
			Instantiate(topObstaclePrefab,
					new Vector3(startOfFloor.x + xLocation, 1.0f, startOfFloor.z + yLocation),
					topObstaclePrefab.transform.localRotation,
					obstacleParent);
		}
	}

	// tries to spawn an obstacle, sees if it is possible for the player to pass the current obstacle in only one move
	void trySpawnObstalce(int xLocation, int yLocation)
	{
		if (Random.Range(0f, 1f) < 0.2f) // spawning zombie
		{
			if (canSpawnObstacleHere(xLocation, 0)) // see if we can spawn a zombie here
			{
				currentOpenSpots[xLocation] = false;
				currentOpenSpots[xLocation + 3] = false;
				InstantiateAnObstacle(xLocation, yLocation, 0); // 0 == zombie
			}
			else // if we can't spawn zombie we still want to spawn either lower or upper obstacle
			{
				if (Random.Range(0f, 1f) > 0.5f)
				{   //spawning lower
					if (canSpawnObstacleHere(xLocation, 1))
					{
						currentOpenSpots[xLocation] = false;
						InstantiateAnObstacle(xLocation, yLocation, 1);
					}
				}
				else //spawning upper
				{
					if (canSpawnObstacleHere(xLocation, 2))
					{ //if we can spawn upper
						currentOpenSpots[xLocation + 3] = false;
						InstantiateAnObstacle(xLocation, yLocation, 2);
					}

				}
			}


		}
		else if (Random.Range(0f, 1f) > 0.5f) // lower
		{
			if (canSpawnObstacleHere(xLocation, 1)) // try spawning lower
			{
				currentOpenSpots[xLocation] = false;
				InstantiateAnObstacle(xLocation, yLocation, 1);
			}
			else if (canSpawnObstacleHere(xLocation, 2)) // if we cant spawn lower we try spawning upper
			{
				currentOpenSpots[xLocation + 3] = false;
				InstantiateAnObstacle(xLocation, yLocation, 2);
			}

		}
		else //spawning upper
		{
			if (canSpawnObstacleHere(xLocation, 2)) // try spawning upper
			{
				currentOpenSpots[xLocation + 3] = false;
				InstantiateAnObstacle(xLocation, yLocation, 2);
			}
			else if (canSpawnObstacleHere(xLocation, 1)) // if we cant spawn upper we try spawning lower
			{
				currentOpenSpots[xLocation] = false;
				InstantiateAnObstacle(xLocation, yLocation, 1);
			}
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
