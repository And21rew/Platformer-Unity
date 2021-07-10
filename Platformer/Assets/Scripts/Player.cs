using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    public Transform checkGround;
    bool isGrounded;
    Animator anim;
    int curHp;
    int maxHP = 3;
    bool isHit = false;
    public Main main;
    public bool key = false;
    bool canTP = true;
    public bool inWater = false;
    public bool inLava = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (inLava)
        {
            anim.SetInteger("State", 4);
            isGrounded = true;
            if (Input.GetAxis("Horizontal") != 0)
                Flip();
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
        else if (inWater)
        {
            anim.SetInteger("State", 4);
            isGrounded = true;
            if (Input.GetAxis("Horizontal") != 0)
                Flip();
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
        else
        {
            GroundCheck();
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);

            if (Input.GetAxis("Horizontal") == 0 && (isGrounded))
            {
                anim.SetInteger("State", 1);
            }
            else
            {
                Flip();
                if (isGrounded)
                    anim.SetInteger("State", 2);
            }
        }
    }

    // Движение влево или вправо
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }

    // Поворот влево или вправо
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    // Проверяем землю под ногами и ставим анимацию прыжка/падения, если земли нет
    void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkGround.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
            anim.SetInteger("State", 3);
    }

    // Пересчитываем здоровье в +/-
    public void RecountHp(int deltaHp)
    {
        curHp += deltaHp;
        if (deltaHp < 0)
        {
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
        }
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
        }
    }

    // Красим персонажа в красный и обратно при ударе
    IEnumerator OnHit()
    {
        if(isHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.04f, GetComponent<SpriteRenderer>().color.b - 0.04f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.04f, GetComponent<SpriteRenderer>().color.b + 0.04f);
        if (GetComponent<SpriteRenderer>().color.g == 1f)
            StopCoroutine(OnHit());
        if (GetComponent<SpriteRenderer>().color.g <= 0)
            isHit = false;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }

    // Перезапуск уровня, если кончилась жизнь
    void Lose()
    {
        main.GetComponent<Main>().Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            key = true;
        }

        if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.GetComponent<Door>().isOpen && canTP)
            {
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                canTP = false;
                StartCoroutine(TPwait());            
            }
            else if (key)
                collision.gameObject.GetComponent<Door>().Unlock();
        }
    }

    IEnumerator TPwait()
    {
        yield return new WaitForSeconds(1f);
        canTP = true;
    }
}
