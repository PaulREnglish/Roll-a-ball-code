using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Blocker;
    public GameObject VictoryCube;


    private const int sizeOfMap = 60;
    const int startingLocation = sizeOfMap / 2;
    // private int count;

    private void CreateWallsAroundStartingLocation(List<Vector2> walls)
    {
        walls.Add(new Vector2(startingLocation + 1, startingLocation));
        walls.Add(new Vector2(startingLocation, startingLocation + 1));
        walls.Add(new Vector2(startingLocation, startingLocation - 1));
        walls.Add(new Vector2(startingLocation - 1, startingLocation));
    }

    // Start is called before the first frame update
    void Start()
    {

        var map = GetMap();

        for (int i = 0; i <= sizeOfMap - 1; i++)
        {
            for (int j = 0; j <= sizeOfMap - 1; j++)
            {
                if (map[i, j] == false)
                {
                    GameObject testcube = Object.Instantiate(Blocker);
                    testcube.transform.position = new Vector3(i + 0.5f, 0.255f, j + 0.5f);
                } 
                else
                {
                    GameObject aVictoryCube = Object.Instantiate(VictoryCube);
                    aVictoryCube.transform.position = new Vector3(i + 0.5f, 0.5f, j + 0.5f);
                }
            }
        }
    }

    private bool[,] GetMap()
    {
        var map = new bool[sizeOfMap, sizeOfMap];
        map[startingLocation, startingLocation] = true;

        List<Vector2> walls = new List<Vector2>();
        List<Vector2> emptyCells = new List<Vector2>();
        List<Vector2> finishedCells = new List<Vector2>();

        CreateWallsAroundStartingLocation(walls);

        emptyCells.Add(new Vector2(startingLocation, startingLocation));
        finishedCells.Add(new Vector2(startingLocation, startingLocation));

        while (walls.Count >= 1)
        {
            int randomIndex = Random.Range(0, walls.Count);                 //  0 <= randomIndex < walls.Count
            var wall = walls[randomIndex];
            int numberOfAdjacentEmptyCells = GetNumberOfAdjacentCells(emptyCells, wall);

            if (numberOfAdjacentEmptyCells == 1)                             //if number of adjacent empty cells = 1, places a blocker at the location
            {
                map[(int)wall.x, (int)wall.y] = true;
                emptyCells.Add(new Vector2(wall.x, wall.y));


                if ((!finishedCells.Exists(A => A.x == wall.x && A.y == wall.y + 1)) && checkMapBoundary((int)wall.x, (int)wall.y + 1))              //adds adjacent unfinished cells to the walls list
                {
                    walls.Add(new Vector2(wall.x, wall.y + 1));
                }

                if ((!finishedCells.Exists(A => A[0] == wall.x + 1 && A[1] == wall.y)) && checkMapBoundary((int)wall.x + 1, (int)wall.y))
                {
                    walls.Add(new Vector2(wall.x + 1, wall.y));
                }

                if ((!finishedCells.Exists(A => A[0] == wall.x && A[1] == wall.y - 1)) && checkMapBoundary((int)wall.x, (int)wall.y - 1))
                {
                    walls.Add(new Vector2(wall.x, wall.y - 1));
                }

                if ((!finishedCells.Exists(A => A[0] == wall.x - 1 && A[1] == wall.y)) && checkMapBoundary((int)wall.x - 1, (int)wall.y))
                {
                    walls.Add(new Vector2(wall.x - 1, wall.y));
                }



            }

            finishedCells.Add(new Vector2(wall.x, wall.y));
            walls.RemoveAt(randomIndex);
            numberOfAdjacentEmptyCells = 0;

        }
        AddBoundariesToMap(map);

        return map;
    }

    private void AddBoundariesToMap(bool[,] map)
    {
        for(int i = 0; i < sizeOfMap; i++)
        {
            map[0, i] = false;
            map[sizeOfMap-1, i] = false;
            map[i, 0] = false;
            map[i, sizeOfMap-1] = false;
        }
    }

    private int GetNumberOfAdjacentCells(List<Vector2> emptyCells, Vector2 wall)
    {

        int numberOfAdjacentEmptyCells = 0;                                 //tracks the number of empty cells adjacent to the current cell as we go through the algorithm.

        if (emptyCells.Exists(ec => ec[0] == wall.x && ec[1] == wall.y + 1))              //counts the number of empty adjacent cells and sees if it is equal to 1 (should always be at least 1)
        {
            numberOfAdjacentEmptyCells++;
        }

        if (emptyCells.Exists(ec => ec[0] == wall.x + 1 && ec[1] == wall.y))
        {
            numberOfAdjacentEmptyCells++;
        }

        if (emptyCells.Exists(ec => ec[0] == wall.x && ec[1] == wall.y - 1))
        {
            numberOfAdjacentEmptyCells++;
        }

        if (emptyCells.Exists(ec => ec[0] == wall.x - 1 && ec[1] == wall.y))
        {
            numberOfAdjacentEmptyCells++;
        }
        return numberOfAdjacentEmptyCells;
    }



    private bool checkMapBoundary(int x, int y)
    {
        if (1 <= x && x < sizeOfMap-1 && 1 <= y && y < sizeOfMap-1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    









}












/*
        Example code:
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 1.5f, 0);

        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(2, 1, 0);

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 1, 0);

*/




