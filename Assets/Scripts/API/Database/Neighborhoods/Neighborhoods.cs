using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;

[Serializable]
public class Neighborhoods
{
    public int NeighborhoodID;
    public string Name;
}

[Serializable]
public class NeighborhoodsList
{
    public List<Neighborhood> policies;
}