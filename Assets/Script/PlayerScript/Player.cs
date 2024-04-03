using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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



}
