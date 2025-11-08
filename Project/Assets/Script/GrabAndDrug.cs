using UnityEngine;

public class GrabAndDrag : MonoBehaviour
{
    private bool isDragging = false;
    private bool attached = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private FoodType foodType;

    private Attach currentZone;

    [HideInInspector]
    public bool IsCorrectlyAttached = false;

    private PyramidManager manager;
    private SpriteRenderer sr;
    private Color originalColor;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foodType = GetComponent<FoodType>();
        manager = FindFirstObjectByType<PyramidManager>();

        sr = GetComponent<SpriteRenderer>();
        sr.material = new Material(sr.material); // evita colore condiviso
        originalColor = sr.color;
    }

    void OnMouseDown()
    {
        if (attached)
            return;

        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (!isDragging || attached)
            return;
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        if (attached)
            return;
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (attached)
            return;

        Attach zone = other.GetComponent<Attach>();
        if (zone != null)
        {
            if (zone.CanAccept(foodType))
            {
                transform.position = zone.transform.position;
                rb.bodyType = RigidbodyType2D.Static;
                attached = true;
                IsCorrectlyAttached = true;

                // registra la zona dove è stato attaccato
                currentZone = zone;
                currentZone.SetCurrentFood(this);

                // notifica il manager
                if (manager != null)
                    manager.CheckCompletion();
            }
            else
            {
                StartCoroutine(WrongPlacementFeedback());
            }
        }
    }

    // Se mai vorrai rimuoverlo manualmente (detach)
    public void Detach()
    {
        if (attached)
        {
            attached = false;
            IsCorrectlyAttached = false;
            rb.bodyType = RigidbodyType2D.Dynamic;

            if (currentZone != null)
            {
                currentZone.ClearCurrentFood();
                currentZone = null;
            }
        }
    }

    private System.Collections.IEnumerator WrongPlacementFeedback()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color original = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = originalColor;
    }
}
