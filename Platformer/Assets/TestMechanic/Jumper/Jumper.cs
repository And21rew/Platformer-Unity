using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpHeight = 50f;
    [SerializeField] private Transform LeftCheck;
    [SerializeField] private Transform RightCheck;
    [SerializeField] private Player player;
    [SerializeField] private Transform MediumDistance;
    private bool moveLeft = true;
    private bool canGo = true;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D LeftPlayerInfo = Physics2D.Raycast(LeftCheck.position, Vector2.left, 5f);
        RaycastHit2D RightPlayerInfo = Physics2D.Raycast(RightCheck.position, Vector2.right, 5f);
        RaycastHit2D LeftWallInfo = Physics2D.Raycast(LeftCheck.position, Vector2.left, 0.2f);
        RaycastHit2D RightWallInfo = Physics2D.Raycast(RightCheck.position, Vector2.right, 0.2f);

        if (moveLeft && canGo)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);

            if (LeftPlayerInfo.collider.CompareTag("Player"))
            {
                StartCoroutine(JumpOnPlayer());
            }

            if (LeftWallInfo.collider.CompareTag("WallGround"))
            {
                moveLeft = false;
            }
        }

        if (!moveLeft && canGo)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.right);

            if (RightPlayerInfo.collider.CompareTag("Player"))
            {
                StartCoroutine(JumpOnPlayer());
            }

            if (RightWallInfo.collider.CompareTag("WallGround"))
            {
                moveLeft = true;
            }
        }
    }

    IEnumerator JumpOnPlayer()
    {
        canGo = false;
        var posX = (transform.position.x + player.transform.position.x) / 2;
        var posY = player.transform.position.y + 1;
        MediumDistance.position = new Vector3(posX, posY, transform.position.z);
        rb.AddForce(MediumDistance.position * jumpHeight, ForceMode2D.Impulse);
        yield return new WaitForSeconds(3f);
        canGo = true;
    }
}
