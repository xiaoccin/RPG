using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    public MonsterStateMachine stateMachine { get; private set; }
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected float playerCheckDistance;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new MonsterStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer);

    protected override void  OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+  playerCheckDistance* facingDir, wallCheck.position.y));
    }
}
