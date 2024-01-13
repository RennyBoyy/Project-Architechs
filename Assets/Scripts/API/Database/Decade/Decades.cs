using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;

[Serializable]
public class Decades
{
    public int DecadeValue;
}

[Serializable]
public class DecadeList
{
    public List<Decade> policies;
}