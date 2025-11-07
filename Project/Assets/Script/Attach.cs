
using UnityEngine;

public class Attach : MonoBehaviour
{
    private bool attached = false;
    private Transform targetPoint;
    private TipiDiCibi foodType;
    private Rigidbody2D rb;

    private void Start()
    {
        foodType = GetComponent<TipiDiCibi>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attached) return;

        ZonaDiAttracco zone = other.GetComponent<ZonaDiAttracco>();
        if (zone != null)
        {
            if (zone.CanAccept(foodType))
            {
                targetPoint = other.transform;
                SnapToTarget();
            }
            else
            {
                StartCoroutine(WrongPlacementFeedback());
            }
        }
    }

    private void SnapToTarget()
    {
        transform.position = targetPoint.position;
        rb.bodyType = RigidbodyType2D.Static;
        attached = true;
    }

    public void Detach()
    {
        if (attached)
        {
            attached = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private System.Collections.IEnumerator WrongPlacementFeedback()
    {
        // Breve feedback visivo
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color original = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = original;
    }
    
}
