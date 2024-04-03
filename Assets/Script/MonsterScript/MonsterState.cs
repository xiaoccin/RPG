using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState
{
    protected MonsterStateMachine stateMachine;
    protected Monster monster;
    protected Rigidbody2D rb;

    protected float stateTimer;//计时器
    private string animBoolName;
    protected bool triggerCalled;//动画触发器控制标志

    public MonsterState(MonsterStateMachine stateMachine, Monster monster, string _animBoolName)
    {
        this.stateMachine = stateMachine;
        this.monster = monster;
        this.animBoolName = _animBoolName;
    }

    public float GetStateTimer()
    {
        return stateTimer;
    }

    public virtual void Enter()
    {
        this.rb = monster.rb;
        monster.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }


    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }



    public virtual void Exit()
    {
        monster.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishedTrigger()
    {
        triggerCalled = true;
    }
}
