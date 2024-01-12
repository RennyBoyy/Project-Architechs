using System;

[Serializable]
public class Government_Policy
{
    public int PolicyID { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public string LinkExtractedText { get; set; }
    public string Impact { get; set; }
    public string OriginalURL { get; set; }
}
