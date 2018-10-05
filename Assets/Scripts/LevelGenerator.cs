using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {

	// change these in the inspector to increase the region on spawning walls.
	public int Rows;
	public int Columns;

	internal int coinCount;
	// these public fields are needee to be set at only in GameManager component inspector
	// and not to be set in PlayerController component inspector
	public GameObject Wall;
	public GameObject Coin;

	private List<Vector3> gridPositions = new List<Vector3>();


	void InitializeList()
	{
		gridPositions.Clear();
		int ends = (Rows/2);		// beacause all wall prefabs are 2 units in size in all directions.
		// create positions to spawn walls
		for (int x = -ends; x <= ends; x+=2)
		{
			for (int y = -ends; y <= ends; y+=2)
			{
				gridPositions.Add(new Vector3(x, 0f, y));
			}			
		}
	}

	Vector3 RandomPosition()
	{	
		// take any randomly selected Vector 3 position from gridPosition list
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		// do not spawn item in the same position, remove the position from list
		gridPositions.RemoveAt(randomIndex);
		// atlast return that position for furthur use
		return randomPosition;
	}

	public void SetupScene()
	{
		// get set up all gridPositions
		InitializeList();

		// spawn wall prefabs at randomly chosen positions, from gridPositions list
		for (int i = 0; i < Random.Range(15, 30); i++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject wall = Instantiate(Wall, randomPosition, Quaternion.identity) as GameObject;
		}

		// spawn coin prefabs at randomly chosen positions, from gridPositions list
		coinCount = Random.Range(5, 10);
		for (int i = 0; i < coinCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject wall = Instantiate(Coin, randomPosition, Quaternion.Euler(90, 0, 45)) as GameObject;
		}


	}
}
