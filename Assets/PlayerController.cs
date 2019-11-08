using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour {
    
    public int speed;
    public Vector2 jumpHeight;
    
    public Transform spawn;
    
    public bool isPlayer2;
    
    public GameObject otherPlayer;
    public GameObject winner;
    
    private SpriteRenderer mySpriteRenderer;
    private Animator anim;
    
    private bool isGrounded = true;
    
    private void Awake() {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        speed=4;
    }

    void Update () {
        anim.SetBool("walk",false);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        
        if(isPlayer2){
            if (Input.GetKey("left")) {
               moveLeft();
            }

            if (Input.GetKey("right")) {
                moveRight();
            }

            if (Input.GetKey("up") && isGrounded) {
                jump();
            }

            if (Input.GetKey("down") && !isGrounded) {
                fall();
            }
        }else{
            if (Input.GetKey("a")) {
               moveLeft();
            }

            if (Input.GetKey("d")) {
                moveRight();
            }

            if (Input.GetKey("w") && isGrounded) {
                jump();
            }

            if (Input.GetKey("s") && !isGrounded) {
                fall();
            }
        }
    }
    
    
    void moveLeft(){
        transform.Translate(-Vector3.right * speed * Time.deltaTime);
        mySpriteRenderer.flipX = true;
        anim.SetBool("walk",true);
    }    
    
    void moveRight(){
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        mySpriteRenderer.flipX = false;
        anim.SetBool("walk",true);  
    }
    
    void jump(){
        GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
        isGrounded=false;
        anim.SetBool("jump",true);
    }
    
    void fall(){
        GetComponent<Rigidbody2D>().gravityScale = 5;
    }
    
    
    
    
    void OnCollisionEnter2D(Collision2D col){
        Debug.Log("OnCollisionEnter2D");
        isGrounded=true;
        anim.SetBool("jump",false);
    }
    
    
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag=="KillZone"){
            transform.position=spawn.position;
        }
        if(col.tag=="Respawn"){
            spawn.position=col.transform.position;
        }
        if(col.tag=="Fast"){
            StartCoroutine(Fast());
            Destroy(col.gameObject);
        }
        if(col.tag=="Heavy"){
            StartCoroutine(otherPlayer.GetComponent<PlayerController>().Heavy());
            Destroy(col.gameObject);
        }
        if(col.tag=="Freeze"){
            StartCoroutine(otherPlayer.GetComponent<PlayerController>().Freeze());
            Destroy(col.gameObject);
        }
        
        if(col.tag=="Finish"){
            mySpriteRenderer.color = new Color(0, 1, 0, 1);
            Destroy(otherPlayer);
            winner.SetActive (true);        }
    }
    
    
    IEnumerator Fast(){
        speed=10;
        mySpriteRenderer.color = new Color(1, 0.5f, 0, 1);
        yield return new WaitForSeconds(5);
        speed=4;
        mySpriteRenderer.color = new Color(1, 1, 1, 1);
    
    }
    
    IEnumerator Heavy(){
        speed=1;
        mySpriteRenderer.color = new Color(0.5f, 0.25f, 0.5f, 1);
        yield return new WaitForSeconds(5);
        speed=4;
        mySpriteRenderer.color = new Color(1, 1, 1, 1);
    }
    
    IEnumerator Freeze(){
        speed=0;
        mySpriteRenderer.color = new Color(0, 0, 1, 1);
        yield return new WaitForSeconds(5);
        speed=4;
        mySpriteRenderer.color = new Color(1, 1, 1, 1);
    }
}