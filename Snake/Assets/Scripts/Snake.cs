using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    public enum DIRECTION { LEFT, DOWN, RIGHT, UP };
    public DIRECTION direction;
    Rigidbody2D rb;
    [SerializeField]
    bool isPlayer;

    void Start()
    {
        float refreshRate = 0.09f;
        direction = isPlayer ? DIRECTION.UP : NumToDirection(Random.Range(0,4));
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        InvokeRepeating("Tick", refreshRate, refreshRate);
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            PlayerInput();
        }
    }

    public DIRECTION NumToDirection(int number)
    {
        switch (number)
        {
            case 0: return DIRECTION.DOWN;
            case 1: return DIRECTION.LEFT;
            case 2: return DIRECTION.RIGHT;
            case 3: return DIRECTION.UP;
            default: return DIRECTION.DOWN;
        }
    }

    public void Tick()
    {
        float diff = 0.50f;
        float none = 0;
        switch (direction)
        {
            case DIRECTION.RIGHT: Move(diff, none); break;
            case DIRECTION.LEFT: Move(-diff, none); break;
            case DIRECTION.DOWN: Move(none, -diff); break;
            case DIRECTION.UP: Move(none, diff); break;
            default: Debug.LogError("UNKNOWN DIRECTION DETECTED!"); break;
        }
    }

    public void PlayerInput()
    {
        if (Input.GetKey("up")) direction = DIRECTION.UP;
        if (Input.GetKey("down")) direction = DIRECTION.DOWN;
        if (Input.GetKey("left")) direction = DIRECTION.LEFT;
        if (Input.GetKey("right")) direction = DIRECTION.RIGHT;
    }

    void Move(float x, float y)
    {
        float pointX = (rb.transform.position.x + x);
        float pointY = (rb.transform.position.y + y);
        rb.transform.position = new Vector3(pointX, pointY, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            RestartScene();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlayer && collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            RestartScene();
        }
    }

    void RestartScene()
    {
        Debug.Log("DEAD");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
