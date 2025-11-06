using UnityEngine;

public class PropLife : MonoBehaviour
{
    public float lifetime = 5f; // Durata in secondi

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
