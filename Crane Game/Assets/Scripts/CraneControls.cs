using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class CraneControls : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _craneBody;
    [SerializeField]
    private Rigidbody _cranePlatform;
    [SerializeField]
    private float _movementSpeed = 1f;
    [SerializeField]
    private GameObject empty;
    [SerializeField]
    private GameObject empty2;
    [SerializeField]
    private PhotonView _photonView;
    [SerializeField]
    CraneScoreManager _scoreManager;

    private bool _canMove = true;
    private bool _isLowering = false;
    private bool _isPulling = false;
    private bool _isClosing = false;

    private Vector3 _movement;
    private Vector3 downwardVector;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _lowerAction;

    public delegate void CraneEvents();
    public CraneEvents craneEvents;
    public delegate void CraneLowered();
    public static event CraneLowered onCraneLowered;
    public delegate void CranePulled();
    public static event CranePulled onCranePulled;
    public delegate void CraneOpened();
    public static event CraneOpened onCraneOpened;
    public delegate void CraneClosed();
    public static event CraneClosed onCraneClosed;

    Animator[] animators;
    private Vector3 _networkPosition;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _lowerAction = _playerInput.actions["Lower"];
        downwardVector = new Vector3(0, -1.0f, 0);

        animators = GetComponentsInChildren<Animator>();
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

    void Update()
    {
        if (_photonView.IsMine)
        {
            Vector2 input = _moveAction.ReadValue<Vector2>();
            _movement = new Vector3(input.x, 0, input.y);
        }
    }

    void FixedUpdate()
    {
        if (_photonView.IsMine)
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
        _isLowering = true;
        DeactivateMove();

        foreach (Animator anim in animators)
        {
            anim.speed = 0;
        }
        //tempRPC();
        _photonView.RPC("tempRPC", RpcTarget.All);
        //Debug.Log("lowering");
    }

    [PunRPC]
    void tempRPC()
    {
        _scoreManager.Temp();
    }

    void StartPulling()
    {
        _isPulling = true;
        _isLowering = false;
        _isClosing = false;

        ChangeAnimSpeed(0);
        //Debug.Log("pulling");
    }

    void StartOpening()
    {
        _isPulling = false;
        _isLowering = false;
        _isClosing = false;

        //photonView.RPC("ChangeAnim", RpcTarget.Others, "ClawOpen");

        ChangeAnimSpeed(1);

        craneEvents = ActivateMove;
        StartCoroutine(Delay(craneEvents, "ClawOpen"));
        //Debug.Log("opening");
    }

    void ActivateMove()
    {
        _canMove = true;
        _playerInput.ActivateInput();

        UpdateScore();
    }

    void DeactivateMove()
    {
        _canMove = false;
        _playerInput.DeactivateInput();
    }

    void StartClosing()
    {
        _isPulling = false;
        _isLowering = false;
        _isClosing = true;

        //photonView.RPC("ChangeAnim", RpcTarget.Others, "ClawClose");
        craneEvents = StopClosing;
        ChangeAnimSpeed(1);
        ChangeAnim("ClawClose");
        StartCoroutine(Delay(craneEvents, string.Empty));

        //Debug.Log("closing");
    }

    void ChangeAnimSpeed(int speed)
    {
        if (_photonView.IsMine)
        {
            foreach (Animator anim in animators)
            {
                anim.speed = speed;
            }
        }
    }

    IEnumerator Delay(CraneEvents craneEvents, string animName)
    {
        if (animName != string.Empty)
            ChangeAnim(animName);
        yield return new WaitForSecondsRealtime(1.5f);
        craneEvents?.Invoke();
    }

    void StopClosing()
    {
        _isPulling = false;
        _isLowering = false;
        _isClosing = false;

        EventManager.TriggerEvent("onCraneClosed");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.localPosition);

        }
        else
        {
            _networkPosition = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            _networkPosition += (this._cranePlatform.velocity * lag);
        }
    }


    private void ChangeAnim(string triggerName)
    {
        if (animators != null)
        {
            foreach (Animator anim in animators)
            {
                anim.SetTrigger(triggerName);
            }
        }
    }


    [PunRPC]
    private void ChangeAnimRPC(string triggerName)
    {
        if (animators != null)
        {
            foreach (Animator anim in animators)
            {
                anim.SetTrigger(triggerName);
            }
        }
    }

    public void UpdateScore()
    {
        _photonView.RPC("UpdateScoreRPC", RpcTarget.All);
        _scoreManager.UpdateScoreText();
    }

    void RegisterPlayer()
    {
        _photonView.RPC("RegisterPlayerRPC", RpcTarget.All);
    }

    [PunRPC]
    void UpdateScoreRPC()
    {
        _scoreManager.CalculateScore();
        _scoreManager.UpdateScoreText();
    }

    [PunRPC]
    void UpdateScoreTextRPC()
    {
        _scoreManager.UpdateScoreText();
    }

    [PunRPC]
    void RegisterPlayerRPC()
    {
        //GameManager.Instance.RegisterPlayer(PhotonNetwork.LocalPlayer, this);
    }
}
