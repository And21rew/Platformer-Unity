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
    bool isClimb = false;
    int coins = 0;
    public bool canHit = true;
    public GameObject blueGem, greenGem;
    int gemCount = 0;
    public Inventory inventory;

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
        if (inLava && !isClimb)
        {
            anim.SetInteger("State", 4);
            isGrounded = true;
            if (Input.GetAxis("Horizontal") != 0)
                Flip();
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
        else if (inWater && !isClimb)
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

            if (Input.GetAxis("Horizontal") == 0 && (isGrounded) && (!isClimb))
            {
                anim.SetInteger("State", 1);
            }
            else
            {
                Flip();
                if (isGrounded && !isClimb)
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
        if (!isGrounded && !isClimb)
            anim.SetInteger("State", 3);
    }

    // Пересчитываем здоровье в +/-
    public void RecountHp(int deltaHp)
    {
        if (deltaHp < 0 && canHit)
        {
            curHp += deltaHp;
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
        }
        else if (curHp > maxHP)
        {
            curHp += deltaHp;
            curHp = maxHP;
        }
        else if (deltaHp > 0)
        {
            curHp += deltaHp;
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
        {
            StopCoroutine(OnHit());
            canHit = true;
        }

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
            inventory.Add_key();
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

        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coins++;
        }

        if (collision.gameObject.tag == "Heart")
        {
            Destroy(collision.gameObject);
            inventory.Add_hp();

        }

        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            RecountHp(-1);
        }

        if (collision.gameObject.tag == "BlueGem")
        {
            Destroy(collision.gameObject);
            inventory.Add_bluegem();
        }

        if (collision.gameObject.tag == "GreenGem")
        {
            Destroy(collision.gameObject);
            inventory.Add_greengem();
        }
    }

    IEnumerator TPwait()
    {
        yield return new WaitForSeconds(1f);
        canTP = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ladder")
        {
            isClimb = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (Input.GetAxis("Vertical") == 0)
            {
                anim.SetInteger("State", 5);
            }
            else
            {
                anim.SetInteger("State", 6);
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isClimb = false;
        if (collision.gameObject.tag == "Ladder")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trampoline")
        {
            StartCoroutine(TrampolineAnim(collision.gameObject.GetComponentInParent<Animator>()));
        }
    }

    IEnumerator TrampolineAnim(Animator animation)
    {
        animation.SetBool("IsJump", true);
        yield return new WaitForSeconds(0.5f);
        animation.SetBool("IsJump", false);
    }

    IEnumerator NoHit()
    {
        gemCount++;
        blueGem.SetActive(true);
        CheckGems(blueGem);

        canHit = false;
        blueGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(Invis(blueGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        canHit = true;

        gemCount--;
        blueGem.SetActive(false);
        CheckGems(greenGem);
    }

    IEnumerator SpeedBonus()
    {
        gemCount++;
        greenGem.SetActive(true);
        CheckGems(greenGem);

        speed *= 2;
        greenGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(Invis(greenGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        speed /= 2;

        gemCount--;
        greenGem.SetActive(false);
        CheckGems(blueGem);
    }

    void CheckGems(GameObject obj)
    {
        if (gemCount == 1)
            obj.transform.localPosition = new Vector3(0f, 0.6f, obj.transform.localPosition.z);
        else if (gemCount == 2)
        {
            blueGem.transform.localPosition = new Vector3(-0.5f, 0.6f, blueGem.transform.localPosition.z);
            greenGem.transform.localPosition = new Vector3(0.5f, 0.6f, greenGem.transform.localPosition.z);
        }
    }

    IEnumerator Invis(SpriteRenderer spr, float time)
    {
        spr.color = new Color(1f, 1f, 1f, spr.color.a - time * 2);
        yield return new WaitForSeconds(time);
        if (spr.color.a > 0)
            StartCoroutine(Invis(spr, time));
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetHP()
    {
        return curHp;
    }

    public void BlueGems()
    {
        StartCoroutine(NoHit());
    }

    public void GreenGems()
    {
        StartCoroutine(SpeedBonus());
    }
}
