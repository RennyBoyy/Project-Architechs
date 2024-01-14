using UnityEngine;
using System.Collections.Generic;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    public List<GameObject> objectsToPreserve; // Lista de objetos para n�o destruir

    void Awake()
    {
        foreach (GameObject obj in objectsToPreserve)
        {
            DontDestroyOnLoad(obj);
        }
    }
}