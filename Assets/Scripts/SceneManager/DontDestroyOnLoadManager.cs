using UnityEngine;
using System.Collections.Generic;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    public List<GameObject> objectsToPreserve; // Lista de objetos para não destruir

    void Awake()
    {
        foreach (GameObject obj in objectsToPreserve)
        {
            DontDestroyOnLoad(obj);
        }
    }
}