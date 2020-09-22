using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rB2D; //Karakterimizin 2D uzayda fiziksel haraketi için
    [SerializeField] float speed = 2; //Hız ayarlaması için 
    Animator anim; //Karakterimizin animasyon kontrolü özelliğin için
    bool isFacingRight = true;
    [SerializeField] float jumpForce = 100;
    bool isGrounded = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>(); //rB2D değişkeninin atamasını yapıyoruzs
        anim = GetComponent<Animator>();    //anim değişkeninin atamasını yapıyoruz
    }

    void FixedUpdate()
    {
        // Debug.Log(rB2D.velocity.y);
        // Debug.LogWarning("<color=red>RAW: "+ Input.GetAxisRaw("Horizontal")+"</color>");//Debug yazdırılırken başına <color=renk> sonuna </color> eklenirse verilen renkte yazdırılır eğer Debug.Log sonuna Warning veya Error eklenirse yazı ikonu değiştirilir
        //  Debug.LogError("<color=blue>Normal: " + Input.GetAxis("Horizontal") + "</color>");
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rB2D.velocity.y); //input ile gidilecek birim yön vektörünü oluşturuyoruz.
        rB2D.velocity = dir;//Rigidbody2D nin hız değerini belirliyoruz
        anim.SetBool("isWalking", 0 != Mathf.Abs(dir.x)); //Animasyon parametresi olan isWalking değerine input alınıp alınmadığını yazıyoruz (giriş değerinin mutlak değeri 1 mi)
        anim.SetFloat("velocityY", rB2D.velocity.y);
        if (rB2D.velocity.x > 0 && !isFacingRight)//Karakterin gittiği yönde döndürmek için gittiği yönü ve şu anda dönük olduğu yönü kontrol ediyoruz
        {
            isFacingRight = !isFacingRight;//Şu anda dönük olduğu yönü tutuyoruz
            Reverse();//Karakterin scale ini - ile çarparak ters çeviriyoruz
        }
        else if (rB2D.velocity.x < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Reverse();
        }
        if (/*Input.GetAxis("Jump")==1||*/Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rB2D.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
        else if (!isGrounded)
        {
            anim.SetBool("isJumping", false);
            if (Physics2D.OverlapCircle(groundCheck.position,0.1f,groundMask))
            {
                isGrounded = true;
            }
        }
    }
    void Reverse()
    {
        Vector3 charScale = transform.localScale;
        charScale.x *= -1;
        transform.localScale = charScale;
        //   transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }
  /*  private void OnGUI()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }*/
}
