using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using System.Collections.Generic;

public class CraneControls : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _craneBody;
    [SerializeField]
    private Rigidbody _cranePlatform;
    [SerializeField]
    private Rigidbody _rightClaw;
    [SerializeField]
    private float _movementSpeed = 1f;
    [SerializeField]
    private GameObject empty;
    [SerializeField]
    private GameObject empty2;

    private bool _canMove = true;
    private bool _isLowering = false;
    private bool _isPulling = false;
    private bool _isClosing = false;

    private Vector3 _movement;
    private Vector3 downwardVector;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _lowerAction;

    public delegate void CraneLowered();
    public static event CraneLowered onCraneLowered;
    public delegate void CranePulled();
    public static event CranePulled onCranePulled;
    public delegate void CraneOpened();
    public static event CraneOpened onCraneOpened;
    public delegate void CraneClosed();
    public static event CraneClosed onCraneClosed;

    Animator[] animators;
    CraneScoreManager scoreManager;

    public static CraneControls instance;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _lowerAction = _playerInput.actions["Lower"];
        downwardVector = new Vector3(0, -1.0f, 0);

        animators = GetComponentsInChildren<Animator>();
        scoreManager = GetComponentInChildren<CraneScoreManager>();
        StartOpening();
    }

    private void OnEnable()
    {
        _lowerAction.performed += _ => StartLowering();

        EventManager.StartListening("onCraneLowered", StartClosing);
        EventManager.StartListening("onCraneClosed", StartPulling);
        EventManager.StartListening("onCranePulled", StartOpening);
    }

    private void OnDisable()
    {
        _lowerAction.performed -= _ => StartLowering();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        _movement = new Vector3(input.x, 0, input.y);
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            MoveCrane();
        }
        else
        {
            if (_isLowering)
            {
                //Debug.Log("a");
                StartCoroutine(MoveClaw(_cranePlatform, downwardVector, empty.transform.localPosition, "onCraneLowered"));
            }

            if (_isClosing)
            {
                //Debug.Log("b");
                //StartClosing();
                //StartCoroutine(MoveClaw("Move", _cranePlatform, downwardVector, empty.transform.localPosition, "onCraneLowered"));
                //StartCoroutine(MoveClaw("Rotate", _rightClaw, openVector * -1, empty2.transform.localEulerAngles, "onCraneClosed"));
            }
            if (_isPulling)
            {
                //Debug.Log("c");
                StartCoroutine(MoveClaw(_cranePlatform, downwardVector * -1, empty2.transform.localPosition, "onCranePulled"));
            }

/*            if (_isOpening)
            {
                Debug.Log("d");
                StartOpening();
                //StartCoroutine(MoveClaw("Rotate", _rightClaw, openVector, empty.transform.localEulerAngles, "onCraneOpened"));
            }*/
        }
    }

    void MoveRB(Vector3 direction, Rigidbody rb)
    {
        // Convert direction into Rigidbody space.
        direction = rb.rotation * direction;
        rb.MovePosition(rb.position + direction * _movementSpeed * Time.fixedDeltaTime);
    }

    void MoveCrane()
    {
        MoveRB(_movement, _craneBody);
    }


    IEnumerator MoveClaw(Rigidbody rb, Vector3 dir, Vector3 endPos, string eventToInvoke)
    {
        Vector3 curPos = new Vector3();
        rb.isKinematic = true;
        yield return new WaitForFixedUpdate();
        while (curPos != endPos)
        {
            MoveRB(dir, rb);
            curPos = rb.transform.localPosition;

            yield return new WaitForFixedUpdate();
            if (curPos == endPos)
            {
                EventManager.TriggerEvent(eventToInvoke);
                break;
            }
        }
        rb.transform.localPosition = endPos;
        //rb.isKinematic = false;
    }

    void StartLowering()
    {
        _canMove = false;
        _isLowering = true;
        //Debug.Log("lowering");
    }

    void StartPulling()
    {
        _isPulling = true;
        _isLowering = false;
        _isClosing = false;
        //Debug.Log("pulling");
    }


    void StartOpening()
    {
        _isPulling = false;
        _isLowering = false;
        _isClosing = false;


        foreach (Animator anim in animators)
        {
            anim.SetTrigger("ClawOpen");
        }

        _canMove = true;
        scoreManager.CalculateScore();
        //Debug.Log("opening");
    }

    void StartClosing()
    {
        _isPulling = false;
        _isLowering = false;
        _isClosing = true;

        foreach (Animator anim in animators)
        {
            anim.SetTrigger("ClawClose");
        }

        //Debug.Log("closing");
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        StopClosing();
    }

    void StopClosing()
    {
        _isPulling = false;
        _isLowering = false;
        _isClosing = false;

        EventManager.TriggerEvent("onCraneClosed");
    }
}
