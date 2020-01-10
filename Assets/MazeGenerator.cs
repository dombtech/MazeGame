using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    public static Coordinates[,] map;
    public static int dimension = 10;
    public static bool collectGems = true;
    public GameObject wall;
    public GameObject floor;
    public GameObject man;
    public GameObject gem;
    public GameObject finishLine;
    public static System.Random random = new System.Random();

    public void Init()
    {
        map = new Coordinates[dimension, dimension];

        var gameObjects = GameObject.FindGameObjectsWithTag("Wall");
        Destroy(GameObject.FindGameObjectWithTag("Finish"));

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }

        for (int i = 0; i < dimension; i++)
            for (int j = 0; j < dimension; j++)
                map[i, j] = new Coordinates(i, j);

        GenerateMaze();

        InstantiateWalls();

        floor.transform.localPosition = new Vector3((dimension * 0.5F) - 0.5F, -0.4F, (dimension * 0.5F) - 0.5F);
        floor.transform.localScale += new Vector3(dimension - 1, 0, dimension - 1);

        man.transform.localPosition = new Vector3(0, 1F, 0);
        if (collectGems)
            InstantiateGems();
        else
            Instantiate(finishLine, new Vector3(dimension - 1F, 0, dimension - 1F), Quaternion.identity);
    }

    public void SetDimension(int value)
    {
        dimension = value;
    }

    public void SetLevelType(bool value)
    {
        collectGems = value;
    }

    public void InstantiateGems()
    {
        int NumOfGems = dimension / 2;
        List<Vector2> points = new List<Vector2>();
        while (NumOfGems > 0)
        {
            int x = random.Next(dimension - 1);
            int z = random.Next(dimension - 1);
            if (!points.Contains(new Vector2(x, z)) &&
               (x != 0 || z != 0))
            {

                var tempGem = Instantiate(gem, new Vector3(x, -.1F, z), Quaternion.identity);
                tempGem.transform.localScale -= new Vector3(.6F, .6F, .6F);
                points.Add(new Vector2(x, z));
                NumOfGems--;
            }
        }
    }

    public void InstantiateWalls()
    {
        for (int i = 0; i < dimension; i++)
            for (int j = 0; j < dimension; j++)
            {
                if (map[i, j].West)
                {
                    var tempWall = Instantiate(wall, new Vector3((i * 1.0F) - 0.5F, 0, j * 1.0F), Quaternion.identity);
                    tempWall.name = string.Format("West: ({0},{1})", i, j);
                    if (i - 1 >= 0)
                        map[i - 1, j].East = false;
                }
                if (map[i, j].East)
                {
                    var tempWall = Instantiate(wall, new Vector3((i * 1.0F) + 0.5F, 0, j * 1.0F), Quaternion.identity);
                    tempWall.name = string.Format("East: ({0},{1})", i, j);
                    if (i + 1 < dimension)
                        map[i + 1, j].West = false;
                }
                if (map[i, j].North)
                {
                    var tempWall = Instantiate(wall, new Vector3(i * 1.0F, 0, (j * 1.0F) - 0.5F), Quaternion.Euler(0, 90, 0));
                    tempWall.name = string.Format("North: ({0},{1})", i, j);
                    if (j - 1 >= 0)
                        map[i, j - 1].South = false;

                }
                if (map[i, j].South)
                {
                    var tempWall = Instantiate(wall, new Vector3(i * 1.0F, 0, (j * 1.0F) + 0.5F), Quaternion.Euler(0, 90, 0));
                    tempWall.name = string.Format("South: ({0},{1})", i, j);
                    if (j + 1 < dimension)
                        map[i, j + 1].North = false;

                }
            }
    }

    public void GenerateMaze()
    {
        Coordinates current = map[0, 0];
        Stack<Coordinates> path = new Stack<Coordinates>();
        path.Push(current);
        int stepsTaken = 0;
        do
        {
            map[current.X, current.Y].Visited = true;
            Coordinates nextStep = TakeStep(current);

            if (nextStep == null)
            {
                current = path.Pop();
            }
            else
            {
                stepsTaken++;
                path.Push(nextStep);
                current = nextStep;
            }
        } while (!current.isOrigin() && stepsTaken != (dimension * dimension));

    }

    public static Coordinates TakeStep(Coordinates current)
    {
        List<Directions> possible = new List<Directions>();
        //North
        if (current.Y - 1 >= 0)
            if (!map[current.X, current.Y - 1].Visited)
                possible.Add(Directions.North);

        //East
        if (current.X + 1 < dimension)
            if (!map[current.X + 1, current.Y].Visited)
                possible.Add(Directions.East);

        //South
        if (current.Y + 1 < dimension)
            if (!map[current.X, current.Y + 1].Visited)
                possible.Add(Directions.South);

        //West
        if (current.X - 1 >= 0)
            if (!map[current.X - 1, current.Y].Visited)
                possible.Add(Directions.West);


        if (possible.Count > 0)
        {
            int index = random.Next(possible.Count);
            switch (possible[index])
            {

                case Directions.North:
                    map[current.X, current.Y].North = false;
                    map[current.X, current.Y - 1].South = false;
                    return map[current.X, current.Y - 1];

                case Directions.South:
                    map[current.X, current.Y].South = false;
                    map[current.X, current.Y + 1].North = false;
                    return map[current.X, current.Y + 1];

                case Directions.East:
                    map[current.X, current.Y].East = false;
                    map[current.X + 1, current.Y].West = false;
                    return map[current.X + 1, current.Y];

                case Directions.West:
                    map[current.X, current.Y].West = false;
                    map[current.X - 1, current.Y].East = false;
                    return map[current.X - 1, current.Y];
            }
        }
        return null;
    }
}


public class Coordinates
{
    public int X;
    public int Y;
    public bool South = true;
    public bool North = true;
    public bool East = true;
    public bool West = true;
    public bool Visited = false;

    public Coordinates(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }
    public bool isOrigin()
    {
        return (X == 0 && Y == 0);
    }
}

public enum Directions
{
    North = 1,
    South = 2,
    East = 3,
    West = 4
}