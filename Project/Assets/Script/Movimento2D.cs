using UnityEngine;
using System.Collections;

public class Movimento2D : MonoBehaviour
{
    public float moveSpeed = 40f;
    private float moveInput;
    private float directionMultiplier = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Camera mainCamera;

    private float halfWidth;
    private float halfHeight;
    private readonly int isWalkingHash = Animator.StringToHash("isWalking");
    private readonly int isStoppingHash = Animator.StringToHash("isStopping");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        halfHeight = mainCamera.orthographicSize;
        halfWidth = halfHeight * mainCamera.aspect;
    }

    void Update()
    {
        // Se il gioco è fermo, non fa nulla
        if (Time.timeScale == 0f)return;
        // Input con inversione
        moveInput = Input.GetAxisRaw("Horizontal") * directionMultiplier;

        // Flip sprite
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        HandleAnimations();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleAnimations()
    {
        if (Mathf.Abs(moveInput) > 0.01f)
        {
            animator.ResetTrigger(isStoppingHash);
            animator.SetBool(isWalkingHash, true);
        }
        else
        {
            if (animator.GetBool(isWalkingHash))
                animator.SetTrigger(isStoppingHash);

            animator.SetBool(isWalkingHash, false);
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, mainCamera.transform.position.x - halfWidth + 0.5f,
                                        mainCamera.transform.position.x + halfWidth - 0.5f);
        transform.position = pos;
    }


    public void InvertControls(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(InvertTemporarily(duration));
    }

    private IEnumerator InvertTemporarily(float duration)
    {
        directionMultiplier = -1f;
        Debug.Log("Controlli invertiti per " + duration + " secondi!");
        yield return new WaitForSeconds(duration);
        directionMultiplier = 1f;
        Debug.Log("Controlli tornati normali!");
    }
}
