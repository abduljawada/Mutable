using System;
using UnityEngine;

public class EnemyFlying : Enemy
{
    [Header("Specific Attributes")]
    [SerializeField] private Vector2 flyPoint;
    [SerializeField] private float flyDistance = 3f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private float attackRate = 5f;
    [SerializeField] private Vector2 eggOffset;
    private float _timeToNextAttack;
    private Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
    //private Animator animator;
    private void Start()
    {
        _timeToNextAttack = attackRate;
    }

    private void Update()
    {
        switch (State)
        {
            case States.Idle:
                CheckPlayerInRange();
                break;
            case States.Chase:
                if (Rigidbody2D.velocity.y != 0f)
                {
                    if (!(transform.position.y >= flyPoint.y)) return;
                    Rigidbody2D.velocity = Vector2.right * speed;
                    OnChangeDir(new EntityEventArgs {Dir = Rigidbody2D.velocity.x});

                    return;
                }
                
                if (transform.position.x > flyPoint.x + flyDistance)
                {
                    Rigidbody2D.velocity = Vector2.left * speed;
                    OnChangeDir(new EntityEventArgs {Dir = Rigidbody2D.velocity.x});
                }
                else if (transform.position.x < flyPoint.x - flyDistance)
                {
                    Rigidbody2D.velocity = Vector2.right * speed;
                    OnChangeDir(new EntityEventArgs {Dir = Rigidbody2D.velocity.x});
                }

                _timeToNextAttack -= Time.deltaTime;

                if (_timeToNextAttack <= 0)
                {
                    _timeToNextAttack = attackRate;
                    Instantiate(eggPrefab, transform.position + (Vector3) eggOffset, Quaternion.identity);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void TransitionToChase(Collider2D player)
    {
        base.TransitionToChase(player);

        Vector2 flyingDir = ((Vector3)flyPoint - transform.position).normalized;
        Rigidbody2D.velocity = flyingDir * speed;
        OnChangeDir(new EntityEventArgs {Dir = Rigidbody2D.velocity.x});
    }
}
