using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance;

    private float points = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint()
    {
        points++;
        Debug.Log("Punti totali: " + points);
    }

    public float GetPoints()
    {
        return points;
    }

    public void menusPoints()
    {
        points--;
        Debug.Log("Punti totali: " + points);
    }

    public void AddPointX2()
    {
        points += 2;
        Debug.Log("Punti totali: " + points);
    }
}
