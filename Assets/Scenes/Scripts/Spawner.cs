using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform background;
    [SerializeField] GameObject prefab;
    [SerializeField] Slider TimeSpeed;
    [HideInInspector] public static List<GameObject> creatures = new List<GameObject>();
    public static int cellsNumber;
    int d;

    void Start()
    {
        cellsNumber = 10 * d;
        creatures.Clear();
    }
    public void spawnShit()
    {
        d = ((int)background.localScale.x - 4)/2;
        cellsNumber = 20 * d;
        for (int i = 0; i < cellsNumber; i++)
        {
            Vector2 initialPos = new Vector2((int)Random.Range(-d,d), (int)Random.Range(-d, d));
            GameObject x = Instantiate(prefab, initialPos, Quaternion.identity);
            creatures.Add(x);
        }
    }
    public void changeTime()
    {
        Time.timeScale = TimeSpeed.value;
    }
}
