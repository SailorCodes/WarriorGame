using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private Animator anim;
    private Rigidbody2D rb2d;
    float moveHorizontal;

    public bool facingRight;

    public float jumpForce;
    public bool isGrounded;
    public bool canDoubleJump;

    PlayerCombat playercombat;

    public bool characterattack;

    public float charactertimer;

    void Start()
    {
        moveSpeed = 10;
        moveHorizontal = Input.GetAxis("Horizontal");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        playercombat = GetComponent<PlayerCombat>();
        charactertimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        CharacterAnimation();
        CharacterAttack();
        CharacterRunAttack();
        CharacterJump();
        CharacterAttackSpacing();
    }

    void CharacterMovement() {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveHorizontal*moveSpeed,rb2d.velocity.y);

    }

    void CharacterAnimation(){
        if (moveHorizontal>0)
        {
            anim.SetBool("isRunning",true);
        }
        if (moveHorizontal==0)
        {
            anim.SetBool("isRunning",false);
        }
        if (moveHorizontal<0)
        {
            anim.SetBool("isRunning",true);
        }
        if (facingRight == false && moveHorizontal>0)
        {
            CharacterFlip();
        }
        if (facingRight == true && moveHorizontal<0)
        {
            CharacterFlip();
        }
    }
    void CharacterFlip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void CharacterAttack() {
        if (Input.GetKeyDown(KeyCode.E) && moveHorizontal == 0)
        {
            
            if (characterattack)
            {
                anim.SetTrigger("isAttack");
                playercombat.DamageEnemy();
                characterattack = false;
            }
       
            FindObjectOfType<AudioManager>().Play("swordsound1");
        }
    }
    void CharacterRunAttack() {
        if (Input.GetKeyDown(KeyCode.E) && moveHorizontal > 0 || Input.GetKeyDown(KeyCode.E) && moveHorizontal < 0)
        {
            
            if (characterattack)
            {
                anim.SetTrigger("isRunAttack");
                playercombat.DamageEnemy();
                characterattack = false;
            }
            FindObjectOfType<AudioManager>().Play("swordsound1");
        }
    }
    void CharacterJump() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping",true);
            if (isGrounded)
            {
                rb2d.velocity = Vector2.up * jumpForce;
                canDoubleJump = true;
            }
            else if(canDoubleJump){
                jumpForce = jumpForce / 1.5f;
                rb2d.velocity = Vector2.up * jumpForce;

                canDoubleJump = false;
                jumpForce = jumpForce * 1.5f;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col){
        anim.SetBool("isJumping",false);

        if (col.gameObject.tag=="Grounded")
        {
            isGrounded = true;
        }
    }
    void OnCollisionStay2D(Collision2D col){
        anim.SetBool("isJumping", false);
        if (col.gameObject.tag=="grounded")
        {
            isGrounded = true;
        }
    }

    void CharacterAttackSpacing()
    {
        if (characterattack==false) 
        {
            charactertimer -= Time.deltaTime;
        }
        if (charactertimer < 0)
        {
            charactertimer = 0;
        }
        if (charactertimer==0f)
        {
            characterattack = true;
            charactertimer = 1f;
        }
    }


    void OnCollisionExit2D(Collision2D col){
        anim.SetBool("isJumping",true);
        if (col.gameObject.tag=="Grounded")
        {
            isGrounded = false;
        }
    }
}
