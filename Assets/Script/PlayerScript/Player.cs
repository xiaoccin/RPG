using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isBusy { get; private set; } = false;//玩家此时动作繁忙不能切换其他动作(实现僵直)


    [Header("Move info")]
    public float moveSpeed = 15f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;//冷却cd
    private float dashCooldownTimer;//冷却时间计时间器
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
     * 阻塞second秒
     */
    public IEnumerable  BusyFor(float _second)
    {
        isBusy = true;
        yield return new WaitForSeconds(_second);
        isBusy = false;
    }




    /*
     * 调用当前的状态的动画结束触发器,将动画结束触发器设置为真
     */
    public void AnimationTragger() =>stateMachine.currentState.AnimationFinishedTrigger();



    /**
     * 直接检测是否冲刺，实现在任何情况下都可以冲刺，而非只有在idle状态可以冲刺
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
     * 设置角色速度
     */
    public void SetVelocity(float _xVelocity,float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    /*
     * 判断是否位于地面上
     */
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    /**
     * 画线判断距离地面的距离与墙壁的距离
     */
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y ));
    }

    /**
     * 角色翻转
     */
    public void Flip()
    {
        facingDir = -facingDir;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    //当朝向与速度不同时翻转
    public void FlipController(float x)
    {
        if(x > 0 && !facingRight ||
            x <0 && facingRight) 
        {
            Flip();
        }
    }
}
