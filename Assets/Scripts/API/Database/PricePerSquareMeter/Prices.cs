using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Prices
{
    public int PropertyID;
    public int Decade;
    public string PriceValue;
    public string LinkExtractedText;
    public string OriginalURL;
}

[Serializable]
public class PricesList
{
    public List<Price_m2> prices;
}
