using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] oggettiDaSpawnare;  // array di prefab da inizializzare in Unity

    public float intervalloSpawn = 3f;      // tempo tra uno spawn e l'altro

    private float timer;
    public Collider2D areaSpawn; // Il tuo oggetto lungo la scena

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalloSpawn)
        {
            SpawnOggetto();
            timer = 0f;
        }
    }

    void SpawnOggetto()
    {
        if (oggettiDaSpawnare.Length == 0 || areaSpawn == null)
        {
            Debug.LogWarning("Prefab o area di spawn non assegnati!");
            return;
        }

        // Ottieni i limiti dell'area (bounding box)
        Bounds bounds = areaSpawn.bounds;

        // Genera una posizione casuale dentro i limiti
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 posizioneCasuale = new Vector2(randomX, randomY);

        // Scegli un prefab casuale
        int index = Random.Range(0, oggettiDaSpawnare.Length);

        // Instanzia
        Instantiate(oggettiDaSpawnare[index], posizioneCasuale, Quaternion.identity);
    }
}

