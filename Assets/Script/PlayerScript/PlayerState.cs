using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float stateTimer;//��ʱ��

    protected float xInput;
    private string animBoolName;
    protected bool triggerCalled;//���������Ʊ�־

    public PlayerState(Player _player,PlayerStateMachine _stateMachine,string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
        
    }

    public virtual void Enter()
    {
        this.rb = player.rb;
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Update()
    {

        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");//���÷����������
        player.anim.SetFloat("yVelocity", rb.velocity.y);//������Ծ���䶯��������־λ�󶨽�ɫ�������ٶ�
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    /*
     * �ڶ�������ʱ������Ϊ��
     */

    public virtual void AnimationFinishedTrigger()
    {
        triggerCalled = true;
    }
}
