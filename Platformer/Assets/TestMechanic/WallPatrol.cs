using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private Transform LeftWallCheck;
    [SerializeField] private Transform RightWallCheck;
    private bool moveLeft = true;
    private bool canGo = true;
    private Animator AnimationRino;

    private void Start()
    {
        AnimationRino = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit2D LeftWallInfo = Physics2D.Raycast(LeftWallCheck.position, Vector2.left, 0.1f);
        RaycastHit2D RightWallInfo = Physics2D.Raycast(RightWallCheck.position, Vector2.right, 0.1f);

        if (moveLeft && canGo)
        {
            AnimationRino.SetInteger("State", 2);
            transform.Translate(speed * Time.deltaTime * Vector2.left);

            if (LeftWallInfo.collider.CompareTag("WallGround"))
            {
                moveLeft = false;
                StartCoroutine(WaitGo());
            }
        }

        if (!moveLeft && canGo)
        {
            AnimationRino.SetInteger("State", 2);
            transform.Translate(speed * Time.deltaTime * Vector2.right);
            
            if (RightWallInfo.collider.CompareTag("WallGround"))
            {
                moveLeft = true;
                StartCoroutine(WaitGo());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 32f, ForceMode2D.Impulse);
        }
    }

    IEnumerator WaitGo()
    {
        canGo = false;
        AnimationRino.SetInteger("State", 3);
        yield return new WaitForSeconds(1f);
        AnimationRino.SetInteger("State", 1);
        if (moveLeft == false)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(2f);
        canGo = true;
    }
}
