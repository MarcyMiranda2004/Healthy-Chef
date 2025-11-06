using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int points = 0;

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

    public int GetPoints()
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
