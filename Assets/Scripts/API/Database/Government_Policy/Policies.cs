using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;

[Serializable]
public class Policies
{
    public int PolicyID;
    public int Year;
    public string Description;
    public string LinkExtractedText;
    public string Impact;
    public string OriginalURL;
}

[Serializable]
public class PoliciesList
{
    public List<Government_Policy> policies;
}