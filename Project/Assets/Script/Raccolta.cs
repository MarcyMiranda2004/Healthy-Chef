using System;
using TMPro;
using UnityEngine;

public class Raccolta : MonoBehaviour
{
    public static Raccolta Instance;

    public float effectDuration = 5f;
    public static event Action<string> OnEventiChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            Destroy(other.gameObject);
            PointManager.Instance.AddPoint();
            OnEventiChanged?.Invoke("Hai raccolto un frutto!");
        }
        else if (other.CompareTag("JunkFood"))
        {
            Destroy(other.gameObject);
            PointManager.Instance.menusPoints();
            OnEventiChanged?.Invoke("Hai raccolto del cibo spazzatura!");
        }
        else if (other.CompareTag("Malus"))
        {
            Destroy(other.gameObject);

            Movimento2D movimento = GetComponent<Movimento2D>();
            if (movimento != null)
                movimento.InvertControls(effectDuration);

            OnEventiChanged?.Invoke("Hai bevuto alcool! Comandi invertiti per 5 secondi!");
        }
        else if (other.CompareTag("Bonus"))
        {
            Destroy(other.gameObject);
            PointManager.Instance.AddPointX2();
            OnEventiChanged?.Invoke("Hai mangiato del cibo molto sano! Punti x2!");
        }
        else if (other.CompareTag("SpeedUp"))
        {
            Destroy(other.gameObject);
            Movimento2D movimento = GetComponent<Movimento2D>();
            if (movimento != null)
                movimento.moveSpeed *= 2;
            PointManager.Instance.menusPoints();
            OnEventiChanged?.Invoke("Hai raccolto qualcosa di sbagliato!");
        }
    }
}
