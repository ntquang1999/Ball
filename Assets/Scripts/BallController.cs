using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [SerializeField] private Vector2 mousePosition2D;
    [SerializeField] private float force = 400f;
    [SerializeField] private float slowdownFactor = 0.25f;

    public TimeManager timeManager;
    public GameObject directionArrow;
    public bool Godmode = false;
    public Camera camera;

    [SerializeField] private bool powerAvailable = false;

    public float fixDT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fixDT = Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.G))
            Godmode = !Godmode;

        if(powerAvailable)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timeManager.EnterBulletTime(slowdownFactor);
                directionArrow.GetComponent<SpriteRenderer>().enabled = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Vector2 currentPosition = GetComponent<Rigidbody2D>().position;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
                Vector2 direction = currentPosition - mousePosition2D;
                direction.Normalize();
                //force = Vector2.Distance(currentPosition, mousePosition2D) * 200;
                //if (force > 500f) force = 500f;
                timeManager.ExitBulletTime();
                directionArrow.GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(direction * force);
                powerAvailable = false;
            }
        }  
        else GetComponent<SpriteRenderer>().color = Color.black;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        powerAvailable = true;
        AudioController.PlaySound("bounce");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            AudioController.PlaySound("win");
        }
        else if (collision.CompareTag("power"))
        {
            force += 30f;
            if (force > 700f) force = 700f;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("slowmotion"))
        {
            slowdownFactor = slowdownFactor * 0.5f;
            if (slowdownFactor < 0.05f) slowdownFactor = 0.05f;
            Destroy(collision.gameObject);
        }
        else if (!Godmode)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            powerAvailable = true;
        }
    }

}
