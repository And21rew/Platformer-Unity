using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpHeight;
    public Transform checkGround;
    bool isGrounded;
    Animator anim;
    int curHp;
    public int maxHP = 3;
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
    public SoundEffect soundEffect;
    public float speed;
    private int jumpsValue;
    private int jumps;
    public Joystick joystick;
    public Camera1 Camera1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHP;
        jumps = 1;
        jumpsValue = PlayerPrefs.GetInt("jump");
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Main>();
        Camera1 = main.gameObject.GetComponent<Camera1>();
    }

    void Update()
    {
        if (inLava && !isClimb)
        {
            anim.SetInteger("State", 4);
            isGrounded = true;
            //if (Input.GetAxis("Horizontal") != 0) Управление для кнопок WASD
            if ((joystick.Horizontal != 0) || (Input.GetAxis("Horizontal") != 0))
                Flip();
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
        else if (inWater && !isClimb)
        {
            anim.SetInteger("State", 4);
            isGrounded = true;
            //if (Input.GetAxis("Horizontal") != 0) Управление для кнопок WASD
            if ((joystick.Horizontal != 0) || (Input.GetAxis("Horizontal") != 0))
                Flip();
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
        else
        {
            GroundCheck();
            if (jumpsValue == 1)
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
                }
            }
            if (jumpsValue == 2)
            {
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumps == 1)
                {
                    jumps++;
                    rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && jumps == 2 && !isGrounded)
                {
                    rb.AddForce(transform.up * jumpHeight/1.5f, ForceMode2D.Impulse);
                    jumps = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && jumps == 2 && isGrounded)
                {
                    rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
                    jumps = 1;
                }
            }
            //if (Input.GetAxis("Horizontal") == 0 && (isGrounded) && (!isClimb)) Управление для кнопок WASD
            if (joystick.Horizontal == 0 && isGrounded && !isClimb)
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

    public void OnPointerDown(BaseEventData _)
    {
        OnJumpButtonDown();
    }

    public void OnJumpButtonDown()
    {
        GroundCheck();
        if (jumpsValue == 1)
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            }
        }
        if (jumpsValue == 2)
        {
            if (isGrounded && jumps == 1)
            {
                jumps++;
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            }
            else if (jumps == 2 && !isGrounded)
            {
                rb.AddForce(transform.up * jumpHeight / 1.5f, ForceMode2D.Impulse);
                jumps = 1;
            }
            else if (jumps == 2 && isGrounded)
            {
                rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
                jumps = 1;
            }
        }
    }

    void FixedUpdate()
    {
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y); Управление для кнопок WASD
        rb.velocity = new Vector2(joystick.Horizontal * speed, rb.velocity.y);
    }

    void Flip()
    {
        //if (Input.GetAxis("Horizontal") > 0) Управление для кнопок WASD
        if (joystick.Horizontal > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        //if (Input.GetAxis("Horizontal") < 0) Управление для кнопок WASD
        if (joystick.Horizontal < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkGround.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded && !isClimb)
            anim.SetInteger("State", 3);
    }

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
            Invoke(nameof(Lose), 1.5f);
        }
    }

    IEnumerator OnHit()
    {
        var color = GetComponent<SpriteRenderer>().color;
        
        if(isHit)
            color = new Color(1f, color.g - 0.04f, color.b - 0.04f);
        else
            color = new Color(1f, color.g + 0.04f, color.b + 0.04f);
        
        if (color.g == 1f)
        {
            StopCoroutine(OnHit());
            canHit = true;
        }

        if (color.g <= 0)
            isHit = false;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }

    void Lose()
    {
        main.GetComponent<Main>().Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("QueenCam"))
        {
            Camera1.check = true;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            key = true;
            inventory.Add_key();
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            var door = collision.gameObject.GetComponent<Door>();
            if (door.isOpen && canTP)
            {
                door.Teleport(gameObject);
                canTP = false;
                StartCoroutine(TPwait());            
            }
            else if (key)
                door.Unlock();
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            soundEffect.PlayCoinSound();
        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            inventory.Add_hp();

        }

        if (collision.gameObject.CompareTag("Mushroom"))
        {
            Destroy(collision.gameObject);
            RecountHp(-1);
        }

        if (collision.gameObject.CompareTag("BlueGem"))
        {
            Destroy(collision.gameObject);
            inventory.Add_bluegem();
        }

        if (collision.gameObject.CompareTag("GreenGem"))
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
        if(collision.gameObject.CompareTag("Ladder"))
        {
            isClimb = true;
            rb.bodyType = RigidbodyType2D.Kinematic;

            //if (Input.GetAxis("Vertical") == 0) Управление для кнопок WASD
            if (joystick.Vertical == 0)
            {
                anim.SetInteger("State", 5);
            }
            else
            {
                anim.SetInteger("State", 6);
                transform.Translate(joystick.Vertical * speed * Time.deltaTime * Vector3.up);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isClimb = false;
        if (collision.gameObject.CompareTag("Ladder"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            StartCoroutine(TrampolineAnim(collision.gameObject.GetComponentInParent<Animator>()));
        }

        if (collision.gameObject.CompareTag("MovePlatform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovePlatform"))
        {
            this.transform.parent = null;
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
        jumpHeight *= 1.5f;
        greenGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(Invis(greenGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        speed /= 2; 
        jumpHeight /= 1.5f;

        gemCount--;
        greenGem.SetActive(false);
        CheckGems(blueGem);
    }

    void CheckGems(GameObject obj)
    {
        if (gemCount == 1)
            obj.transform.localPosition = new Vector3(0f, 1.5f, obj.transform.localPosition.z);
        else if (gemCount == 2)
        {
            blueGem.transform.localPosition = new Vector3(-0.5f, 1.5f, blueGem.transform.localPosition.z);
            greenGem.transform.localPosition = new Vector3(0.5f, 1.5f, greenGem.transform.localPosition.z);
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
