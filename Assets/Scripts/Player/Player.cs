using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;


    private Touch touch;
    public float maxRunSpeed;
    private float currentRunSpeed;
    public float xAxisSpeed;
    [SerializeField] ParticleSystem swirlParticle;






    [Header("Stages")]
    public int levelValue;
    public Slider levelSlider;
    public GameObject[] stage0;
    public GameObject[] stage1;
    public GameObject[] stage2;
    public GameObject[] stage3;
    public GameObject[] stage4;
    public Image levelFillImage;
    public Sprite levelFill0;
    public Sprite levelFill1;
    public Sprite levelFill2;
    public Sprite levelFill3;
    public Sprite levelFill4;
    public Animator[] playerAnimators;
    public UnityEvent onLevelUp;
    public UnityEvent onLevelDown;

    private Rigidbody rb;
    public bool stop = false;

    private void Awake()
    {
        currentRunSpeed = maxRunSpeed;
        Instance = this;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isStarted) { return; }
        Movement();
        GroundCheck();
    }

    float touchXDelta;
    float newX;
    private void Movement()
    {
        if (stop) { return; }
        if (!GameManager.Instance.isStarted) { return; }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                touchXDelta = touch.deltaPosition.x;
                transform.position += transform.right * xAxisSpeed * touchXDelta * Time.deltaTime;
            }
        }
        Vector3 clampX = transform.position;
        clampX.x = Mathf.Clamp(clampX.x, -4.65f, 4.65f);
        transform.position = clampX;
        AnimPlay(PlayerStatus.Run);


        transform.position += transform.forward * currentRunSpeed * Time.deltaTime;
    }


    private int _stage;
    private int _prevStage;
    public void StageManager()
    {
        if (levelValue < 0) { levelValue = 0; }
        if (levelValue > 100) { levelValue = 100; }

        //Stages
        if (levelValue < 25)
        {
            _stage = 0;
        }
        else if (levelValue >= 25 && levelValue < 50)
        {
            _stage = 1;
        }
        else if (levelValue >= 50 && levelValue < 70)
        {
            _stage = 2;
        }
        else if (levelValue >= 70 && levelValue < 90)
        {
            _stage = 3;
        }
        else if (levelValue >= 90 && levelValue <= 100)
        {
            _stage = 4;
        }

        if (_prevStage != _stage)
        {
            if (_prevStage < _stage)
            {
                onLevelUp?.Invoke();
                _prevStage = _stage;
            }
            else if (_prevStage > _stage)
            {
                onLevelDown?.Invoke();
                _prevStage = _stage;
            }
        }


        foreach (var item in stage0)
        {
            item.SetActive(_stage == 0);
        }
        foreach (var item in stage1)
        {
            item.SetActive(_stage == 1);
        }
        foreach (var item in stage2)
        {
            item.SetActive(_stage == 2);
        }
        foreach (var item in stage3)
        {
            item.SetActive(_stage == 3);
        }
        foreach (var item in stage4)
        {
            item.SetActive(_stage == 4);
        }
    }


    public enum PlayerStatus
    {
        Idle,
        Run,
        Sad,
        Dance,
        Falling,
        Cheer,
        Spin
    }

    public static readonly int Status = Animator.StringToHash("Status");
    public void AnimPlay(PlayerStatus status)
    {
        switch (status)
        {
            case PlayerStatus.Idle:
                foreach (var item in playerAnimators)
                {
                    item.SetInteger(Status, 0);
                }
                break;
            case PlayerStatus.Run:
                foreach (var item in playerAnimators)
                {
                    item.SetInteger(Status, 1);
                    item.Update(0); // force to quick transition to run. (on stage change, there is a delay to entry>1sec play idle> run);
                }
                break;
            case PlayerStatus.Sad:
                foreach (var item in playerAnimators)
                {
                    item.SetInteger(Status, 2);
                }
                break;
            case PlayerStatus.Dance:
                foreach (var item in playerAnimators)
                {
                    item.SetInteger(Status, 3);
                }
                break;
            case PlayerStatus.Falling:
                foreach (var item in playerAnimators)
                {
                    item.SetInteger(Status, 4);
                }
                break;
            case PlayerStatus.Cheer:
                foreach (var item in playerAnimators)
                {
                    item.SetInteger(Status, 5);
                }
                break;
            case PlayerStatus.Spin:
                foreach (var item in playerAnimators)
                {
                    item.SetInteger(Status, 6);
                }
                break;
        }
    }



    public RaycastHit groundHit;
    public LayerMask groundLayerMask;
    public float groundCheckRange;
    public bool isGrounded;
    public void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.localPosition, -transform.up, out groundHit, groundCheckRange, groundLayerMask);

        if (!isGrounded)
        {
            swirlParticle.Play();
            rb.drag = 3f;
            AnimPlay(PlayerStatus.Falling);
        }
        else if (isGrounded)
        {
            swirlParticle.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.localPosition, -transform.up * groundCheckRange);
    }


}
