using UnityEngine;

public class Raccolta : MonoBehaviour
{
    public float effectDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            Destroy(other.gameObject);
            PointManager.Instance.AddPoint();
        }
        else if (other.CompareTag("JunkFood"))
        {
            Debug.Log("Hai Bevuto del cibo spazzature!");
            Destroy(other.gameObject);
            PointManager.Instance.menusPoints();
        }
        else if (other.CompareTag("Malus"))
        {
            Debug.Log("Hai Bevuto del alcool!");

            Destroy(other.gameObject);

            Movimento2D movimento = GetComponent<Movimento2D>();

            if (movimento != null)
            {
                movimento.InvertControls(effectDuration);
            }
            else
            {
                Debug.LogWarning("Movimento2D non trovato sul player!");
            }
        }
        else if (other.CompareTag("Bonus"))
        {
            Debug.Log("Hai mangiato del cibo molto sano!");

            Destroy(other.gameObject);
            PointManager.Instance.AddPointX2();
        }
    }
}
