using UnityEngine;

public class GrabAndDrug : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        // Verifica se la sprite è fissata
        if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
            return;

        // Calcola l'offset tra la posizione del mouse e la posizione della sprite
        isDragging = true;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, 0);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Aggiorna la posizione della sprite in base alla posizione del mouse
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
