using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public GameObject GameOverPanel;

    private Rigidbody rb;
    public float move;
    public float jump;
    private bool isjumping;
    public GameObject player;

    private float counter;
    public Text cointext;
    
    void Start()
    {
        counter = 0;
        cointext.text = "COINS : " + counter;
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float hmove = Input.GetAxis("Horizontal") * move;
        float vmove = Input.GetAxis("Vertical") * move;

        rb.velocity = new Vector3 (hmove,rb.velocity.y, vmove);
        if(Input.GetKeyDown(KeyCode.Space) && isjumping == true)
        {
            rb.velocity = new Vector3(0,jump , 0);
        }
        isjumping = false;

        if(player.transform.position.y <= -1)
        {
            GameOverPanel.SetActive(true);
            //SceneManager.LoadScene("Level");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        isjumping = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
           other.gameObject.SetActive(false);
           counter = counter + 1;
           cointext.text = "COINS : " + counter;
        }
       
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("Level");
    }

}
