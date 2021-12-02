using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using bowen.Saving;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Volume volume;
    MotionBlur motionBlur;
    public float rayCastHeightOffset;
    public static PlayerController instance;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Movement")]
    public float moveSpeed = 20f;
    public bool hitZero;
    public bool canMove;
    Rigidbody rb;

    //public LayerMask raycastMask;

    [Header("Dash")]
    public float dashSpeed = 100;
    public float dashDuration;
    [SerializeField] bool isDashing;

    [Header("Interactables")]
    bool byElevator;
    GameObject elevator; //nearest elevator
    bool byInteractable;
    public GameObject interactable;

    [Header("Other")]
    public Stamina_Bar stamina_Bar;
    public GameObject flashlight;
    Vector3 movementDirection;


    private bool hasLoaded = false;

    void Awake()
    {
        //upperRay.transform.position = new Vector3(upperRay.transform.position.x, stepHeight, upperRay.transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        volume = FindObjectOfType<Volume>();
        stamina_Bar = FindObjectOfType<Stamina_Bar>();
        GetComponent<GhostTrail>().enabled = false;
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(Load());

        if (volume.profile.TryGet(out MotionBlur blur))
        {
            motionBlur = blur;
        }

        //DO NOT REMOVE!
        SaveLoadSystem.instance.Load();
    }

    // Update is called once per frame
    void Update()
    {
        /// Doesn't make sense why this works, but without it the game doesn't load correctly.
        /// Also you can't get rid of the load in the start method otherwise it doesn't work.
        /// No clue wtf is happening.
        if (hasLoaded == false)
        {
            SaveLoadSystem.instance.Load();
            hasLoaded = true;
        }

        if (MenuControl.instance.isPaused)
        {
            return;
        }

        ProcessInputs();

        if (Input.GetButtonDown("Flashlight"))
        {
            if (flashlight.activeSelf)
                flashlight.SetActive(false);
            else
                flashlight.SetActive(true);
        }

        if (stamina_Bar == null)
        {
            stamina_Bar = FindObjectOfType<Stamina_Bar>();
        }

        //Quick Punch

        if (stamina_Bar.MAX_STAMINA < 20)
        {
            stamina_Bar.MAX_STAMINA = 20;
        }

        if (PlayerStats.instance.health > 100)
        {
            PlayerStats.instance.health = 100;
        }

        //Stamina and Dash Control
        if (PlayerStats.instance.stamina <= 0)
        {
            hitZero = true;
        }
        else if (hitZero == false)
        {
            //Dash
            if (Input.GetButtonDown("Dash") && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && canMove)
            {
                isDashing = true;
                StartCoroutine(PlayerStats.instance.IFrames());
                GetComponent<GhostTrail>().enabled = true;
                PlayerStats.instance.stamina -= 20f;
            }
            else
            {
                if (GameManager.overclocked)
                    moveSpeed = 45f;
                else
                    moveSpeed = 20;
                //Stamina Regen
                if (PlayerStats.instance.stamina < stamina_Bar.MAX_STAMINA)
                {
                    PlayerStats.instance.stamina += 20f * Time.deltaTime;
                }
            }
        }

        if (hitZero == true)
        {
            moveSpeed = 5;
            PlayerStats.instance.stamina += 10f * Time.deltaTime;

            if (PlayerStats.instance.stamina >= stamina_Bar.MAX_STAMINA)
            {
                hitZero = false;
            }
        }

        ElevatorControl();
    }
    private void FixedUpdate()
    {
        Move();
        RotatePlayer();

        if (isDashing)
        {
            StartCoroutine(MotionBlurLerp(0, 1, 0.15f));
            StartCoroutine(DashTimer(dashDuration));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Energy"))
        {
            if (stamina_Bar.MAX_STAMINA != 100)
            {
                stamina_Bar.MAX_STAMINA = 100;
                other.GetComponentInParent<ObjectManager>().DisableObject();
            }
        }

        if (other.CompareTag("Health"))
        {
            if (PlayerStats.instance.health != 100)
            {
                PlayerStats.instance.health += 20;
                other.GetComponentInParent<ObjectManager>().DisableObject();
            }
        }

        if (other.tag.Equals("Elevator"))
        {
            elevator = other.gameObject;
            byElevator = true;
        }

        if (other.tag.Equals("Interactable"))
        {
            interactable = other.gameObject;
            byInteractable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Elevator"))
            byElevator = false;

        if (other.tag.Equals("Interactable"))
            byInteractable = false;
    }

    void ElevatorControl()
    {
        if (byElevator)
        {
            if (Input.GetButtonDown("Interact"))
            {
                elevator.GetComponent<Elevator>().Move();
            }
        }
    }

    void RotatePlayer()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    IEnumerator DashTimer(float t)
    {
        isDashing = true;
        rb.velocity = movementDirection.normalized * dashSpeed;
        Physics.SyncTransforms();
        yield return new WaitForSeconds(t);
        isDashing = false;
        GetComponent<GhostTrail>().enabled = false;
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        StartCoroutine(MotionBlurLerp(1, 0, t));
    }

    IEnumerator MotionBlurLerp(float a, float b, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            motionBlur.intensity.value = Mathf.Lerp(a, b, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        motionBlur.intensity.value = b;
    }

    void ProcessInputs()
    {
        float mH = Input.GetAxisRaw("Horizontal");
        float mV = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector3(mH, 0, mV).normalized;
    }

    void Move()
    {
        if (canMove)
            rb.velocity = new Vector3(movementDirection.x * moveSpeed, rb.velocity.y, movementDirection.z * moveSpeed);
    }
}
