using UnityEngine;

public class GattoMovimento : MonoBehaviour
{
    public float velocita = 5f;
    public float forzaSalto = 7f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool aTerra = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float movimento = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movimento * velocita, rb.linearVelocity.y);

        // aggiorna parametri Animator
        anim.SetFloat("Velocità", Mathf.Abs(movimento));
        anim.SetBool("aTerra", aTerra);

        // salto
        if (Input.GetKeyDown(KeyCode.Space) && aTerra)
        {
            rb.AddForce(new Vector2(0, forzaSalto), ForceMode2D.Impulse);
        }

        // gira il gatto
        if (movimento > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movimento < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piano"))
            aTerra = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piano"))
            aTerra = false;
    }
}
