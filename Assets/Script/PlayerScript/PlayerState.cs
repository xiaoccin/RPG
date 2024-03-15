using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float stateTimer;//计时器

    protected float xInput;
    private string animBoolName;
    protected bool triggerCalled;//触发器控制标志

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

        xInput = Input.GetAxisRaw("Horizontal");//设置方向键的输入
        player.anim.SetFloat("yVelocity", rb.velocity.y);//设置跳跃下落动画，将标志位绑定角色的下落速度
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    /*
     * 在动画结束时触发器为真
     */

    public virtual void AnimationFinishedTrigger()
    {
        triggerCalled = true;
    }
}
