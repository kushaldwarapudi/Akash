using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float horizontal;
    public int speed;
    public float JumpForce = 30f;
    public bool IsGrounded;
    public Color Red, Blue, Yellow,SkyBlue;
    public Text ScoreText;
    public int ScoreValue;
    public GameObject CoinBlast;
    public GameObject BallBlast;
    public GameObject GameOverMenu;
    public GameObject YouWinPanel;
    public bool ISGameOver;
    // Start is called before the first frame update
    void Start()
    {
        ScoreValue = 0;
        speed = 7;
        GetComponent<SpriteRenderer>().enabled = true;
        ScoreText.text = "Score : " + ScoreValue.ToString();
        GetComponent<SpriteRenderer>().color = Color.white;
        IsGrounded = true;
        GameOverMenu.SetActive(false);
        YouWinPanel.SetActive(false);
        ISGameOver = false;

        GetComponent<Rigidbody2D>().simulated = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }
    public void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(horizontal * speed * Time.fixedDeltaTime, 0));

    }
    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
        IsGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Red")
        {
            GetComponent<SpriteRenderer>().color = Red;
            IsGrounded = true;
        }
        if (collision.gameObject.tag == "Blue")
        {
            GetComponent<SpriteRenderer>().color = Blue;
            IsGrounded = true;
        }
        if (collision.gameObject.tag == "Yellow")
        {
            GetComponent<SpriteRenderer>().color = Yellow;
            IsGrounded = true;
        }
        if (collision.gameObject.tag == "SkyBlue")
        {
            GetComponent<SpriteRenderer>().color = SkyBlue;
            IsGrounded = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
        if (collision.gameObject.tag == "Spikes" && !ISGameOver)
        {
            StartCoroutine(GameOver());
        }
        if (collision.gameObject.name == "Win")
        {
            YouWonGame();
        }

    }
    public void YouWonGame()
    {
        YouWinPanel.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        speed = 0;
        GetComponent<Rigidbody2D>().simulated = false;
    }
    public IEnumerator GameOver()
    {
        GameObject BallParticle = Instantiate(BallBlast, transform.position, Quaternion.identity);
        Destroy(BallParticle, 3f);
        GetComponent<SpriteRenderer>().enabled = false;
        speed = 0;
        GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(2f);
        
        GameOverMenu.SetActive(true);
        ISGameOver = true;
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            ScoreValue++;
            ScoreText.text = "Score : " + ScoreValue.ToString();
            GameObject CoinParticle = Instantiate(CoinBlast, this.transform.position,Quaternion.identity);
            Destroy(CoinParticle, 3f);
            Destroy(collision.gameObject);
        }
    }


}
