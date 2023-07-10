using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed;
    [SerializeField] float sprintModifier;
    [SerializeField] float acceleration;
    Rigidbody rb;
    float moveX;
    float moveZ;
    Vector3 addedForce;
    float forceToAddX;
    float forceToAddZ;
    float speedDifX;
    float speedDifZ;

    [Space]

    [Header("Jumping")]
    [SerializeField] float jumpPower;
    [SerializeField] GameObject jumpParticles;
    bool jumpPressed;

    [Space]

    [Header("Respawning")]
    [SerializeField] float respawnDelay;
    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject deathEffect;
    bool dead;

    [Space]

    [Header("Sounds")]
    [SerializeField] UnityEvent[] sounds;

    [Space]

    [Header("Misc")]
    [SerializeField] float lerpSpeed;
    [SerializeField] UnityEvent[] cameraFollowToggle;
    SphereCollider col;
    ConstantForce grav;
    AudioSource rollSound;
    bool grounded;
    float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        grav = GetComponent<ConstantForce>();
        rollSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (dead)
        {
            return;
        }

        // Movement
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveX *= sprintModifier;
            moveZ *= sprintModifier;
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (dead)
        {
            return;
        }

        // Movement
        speedDifX = maxSpeed - Mathf.Abs(rb.velocity.x);
        speedDifZ = maxSpeed - Mathf.Abs(rb.velocity.z);

        forceToAddX = speedDifX * acceleration;
        forceToAddZ = speedDifZ * acceleration;

        addedForce = new Vector3 (forceToAddX * moveX, rb.velocity.y, forceToAddZ * moveZ);
        rb.AddForce(addedForce, ForceMode.Force);

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.z))
        {
            speed = rb.velocity.x;
        }
        else
        {
            speed = rb.velocity.z;
        }

        if (grounded)
        {
            rollSound.volume = Mathf.Abs(speed) / 10;
        }

        if (rollSound.volume < 0.2f || dead || !grounded)
        {
            rollSound.volume = 0;
        }

        // Jumping
        if (jumpPressed)
        {
            rb.velocity = new Vector3 (rb.velocity.x, jumpPower, rb.velocity.z);
            GameObject pls = Instantiate(jumpParticles, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(pls, 1.1f);
            sounds[0].Invoke();
            jumpPressed = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Harmful"))
        {
            StartCoroutine(DieAndRespawn());
        }

        if (collision.gameObject.layer == 7)
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Ring"))
        {
            FinishLevel();
            transform.position = trigger.gameObject.transform.position;
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    IEnumerator DieAndRespawn()
    {
        dead = true;
        rollSound.volume = 0;
        sounds[1].Invoke();
        col.enabled = false;
        grav.force = new Vector3(0, 0.5f, 0);
        rb.angularDrag = 1;
        rb.velocity = new Vector3(rb.velocity.x * 1.5f, 5, rb.velocity.z * 1.5f);
        // play death effect
        cameraFollowToggle[0].Invoke();
        // destroy death effect
        yield return new WaitForSeconds(respawnDelay);
        transform.position = respawnPoint.position;
        cameraFollowToggle[1].Invoke();
        grav.force = new Vector3(0, -17.5f, 0);
        rb.angularDrag = 0.5f;
        col.enabled = true; 
        dead = false;
    }

    void FinishLevel()
    {
        dead = true;
        // Show win screen
        rb.angularDrag = 1;
        grav.force = Vector3.zero;
        cameraFollowToggle[2].Invoke();
    }

    void StartLevel()
    {
        dead = false;
        rb.angularDrag = 0.5f;
        grav.force = new Vector3(0, -17.5f, 0);
        cameraFollowToggle[1].Invoke();
    }
}
