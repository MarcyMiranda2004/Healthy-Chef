using UnityEngine;

public class Ridimensionamento : MonoBehaviour
{
    private SpriteRenderer sr;
    public float referenceHeight = 1f; // Altezza visiva desiderata in Unity (in unità mondo)

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Normalize();
    }

    void Normalize()
    {
        if (sr.sprite == null) return;

        float spriteHeight = sr.sprite.bounds.size.y;
        float scale = referenceHeight / spriteHeight;
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
