using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    private bool facingRight = true;
    public AudioClip footStep;
    public AudioClip jumpSound;

    // Use this for initialization
    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayFootStep()
    {
        // ejecutar el sonido de un paso.
        audioSource.clip = footStep;
        audioSource.Play();
    }

    public void PlayJump()
    {
        // ejecutar el sonido de un paso.
        audioSource.clip = jumpSound;
        audioSource.Play();
    }

    private void FlipPlayer()
    {
        // rota el sprite
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
        facingRight = !facingRight;
    }

    // Update is called once per frame
    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        // como no es para calculos
        anim.SetFloat("Speed", Mathf.Abs(movement));

        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

        if ((movement < 0 && facingRight) || (movement > 0 && !facingRight))
            FlipPlayer();

        // solo si este es el frame en que se apreto la tecla
        if (Input.GetKeyDown(KeyCode.UpArrow) && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Jump")
        {
            rb.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jump");
        }
    }
}
