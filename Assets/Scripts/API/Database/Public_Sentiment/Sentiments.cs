using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sentiments
{
    public int SentimentID;
    public int Year;
    public string Comments;

    public decimal SentimentScore;

    public string LinkExtractedText;

    public string OriginalURL;
}

[Serializable]
public class SentimentsList
{
    public List<Public_Sentiment> sentiments;
}
