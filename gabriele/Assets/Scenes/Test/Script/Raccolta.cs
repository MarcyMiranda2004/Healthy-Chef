using UnityEngine;

public class Raccolta : MonoBehaviour
{
    public float effectDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.AddPoint();
        }
        else if (other.CompareTag("JunkFood"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.menusPoints();
        }
        else if (other.CompareTag("Malus"))
        {
            Debug.Log("Hai raccolto un Malus!");

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
            Debug.Log("Hai raccolto un Bonus!");

            Destroy(other.gameObject);
            GameManager.Instance.AddPointX2();
        }
    }
}
