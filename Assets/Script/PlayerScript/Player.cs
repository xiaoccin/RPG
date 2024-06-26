using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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
    protected override void  Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine,"Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
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









}
