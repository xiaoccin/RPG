using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isBusy { get; private set; } = false;//��Ҵ�ʱ������æ�����л���������(ʵ�ֽ�ֱ)


    [Header("Move info")]
    public float moveSpeed = 15f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;//��ȴcd
    private float dashCooldownTimer;//��ȴʱ���ʱ����
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    #region State
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerWallSlideState wallSlideState { get; private set; }

    public PlayerPrimaryAttackState primaryAttackState { get; private set; }

    #endregion
    private void Awake()
    {
        
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine,"Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }


    /*
     * ����second��
     */
    public IEnumerable  BusyFor(float _second)
    {
        isBusy = true;
        yield return new WaitForSeconds(_second);
        isBusy = false;
    }




    /*
     * ���õ�ǰ��״̬�Ķ�������������,��������������������Ϊ��
     */
    public void AnimationTragger() =>stateMachine.currentState.AnimationFinishedTrigger();



    /**
     * ֱ�Ӽ���Ƿ��̣�ʵ�����κ�����¶����Գ�̣�����ֻ����idle״̬���Գ��
     */
    private void CheckForDashInput()
    {
        dashCooldownTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer<0)
        {
            dashCooldownTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if(dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangState(dashState);
        }
    }


    /**
     * ���ý�ɫ�ٶ�
     */
    public void SetVelocity(float _xVelocity,float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    /*
     * �ж��Ƿ�λ�ڵ�����
     */
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    /**
     * �����жϾ������ľ�����ǽ�ڵľ���
     */
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y ));
    }

    /**
     * ��ɫ��ת
     */
    public void Flip()
    {
        facingDir = -facingDir;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    //���������ٶȲ�ͬʱ��ת
    public void FlipController(float x)
    {
        if(x > 0 && !facingRight ||
            x <0 && facingRight) 
        {
            Flip();
        }
    }
}
