using UnityEngine;
using System.Collections;
public class Player2Controller : MonoBehaviour {
    
    public int speed;
    public Vector2 jumpHeight;
    private SpriteRenderer mySpriteRenderer;
    private Animator anim;
    
    private bool isGrounded = true;
    
    private void Awake() {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update () {
        anim.SetBool("walk",false);
        GetComponent<Rigidbody2D>().gravityScale = 1;

        if (Input.GetKey("left")) {
           transform.Translate(-Vector3.right * speed * Time.deltaTime);
            mySpriteRenderer.flipX = true;
            anim.SetBool("walk",true);
        }
        
        if (Input.GetKey("right")) {
           transform.Translate(Vector3.right * speed * Time.deltaTime);
            mySpriteRenderer.flipX = false;
            anim.SetBool("walk",true);
        }
        
        if (Input.GetKey("up") && isGrounded) {
            GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
            isGrounded=false;
            anim.SetBool("jump",true);
    
        }
        
        if (Input.GetKey("down") && !isGrounded) {
            GetComponent<Rigidbody2D>().gravityScale = 5;
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        isGrounded=true;
        anim.SetBool("jump",false);
        
    
    }
}