using System;

[Serializable]
public class Price_m2
{
    public int PropertyID { get; set; }
    public int Decade { get; set; }
    public string PriceValue { get; set; }
    public string LinkExtractedText { get; set; }
    public string OriginalURL { get; set; }
}