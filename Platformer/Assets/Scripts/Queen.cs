using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float power = 48f;
    [SerializeField] private Transform LeftWallCheck;
    [SerializeField] private Transform RightWallCheck;
    private bool moveLeft = true;
    private bool canGo = true;
    private Animator AnimationQueen;
    private int health = 4;

    private void Start()
    {
        AnimationQueen = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit2D LeftWallInfo = Physics2D.Raycast(LeftWallCheck.position, Vector2.left, 0.1f);
        RaycastHit2D RightWallInfo = Physics2D.Raycast(RightWallCheck.position, Vector2.right, 0.1f);

        if (moveLeft && canGo)
        {
            AnimationQueen.SetInteger("State", 2);
            transform.Translate(speed * Time.deltaTime * Vector2.left);

            if (LeftWallInfo.collider.CompareTag("WallGround"))
            {
                moveLeft = false;
                StartCoroutine(WaitGo());
            }
        }

        if (!moveLeft && canGo)
        {
            AnimationQueen.SetInteger("State", 2);
            transform.Translate(speed * Time.deltaTime * Vector2.right);
            
            if (RightWallInfo.collider.CompareTag("WallGround"))
            {
                moveLeft = true;
                StartCoroutine(WaitGo());
            }
        }

        if (health <= 0)
        {
            canGo = false;
            AnimationQueen.SetInteger("State", 3);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * power, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("DamageStone"))
        {
            canGo = false;
            health--;
            Destroy(collision.gameObject);

            if (health > 0)
            {
                StartCoroutine(WaitGo());
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            }
        }
    }

    IEnumerator WaitGo()
    {
        canGo = false;
        AnimationQueen.SetInteger("State", 1);
        if (moveLeft == false)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(3f);
        canGo = true;
    }
}
