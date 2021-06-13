/* Author: Armando "Mandiux" Fernandez
 * Date:   06/12/2021
 * Github: https://github.com/DevMando/Unity2D-Snake
 * Description: I felt like building the classic snake game.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    
    [SerializeField]
    GameObject snakeRef;
    Snake snake;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        float refreshRate = 0.5f;
        snake = new Snake(snakeRef);
        InvokeRepeating("Slither", refreshRate, refreshRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        snake.PlayerInput();
    }

    void Slither() => snake.Tick();

}

class Snake
{
    public enum DIRECTION { West, South, East, North };
    public DIRECTION direction;
    public Rigidbody2D rigidbody;
    List<Snake> tail = new List<Snake>();

    public Snake(GameObject snakeRef)
    {
        direction = DIRECTION.North;
        rigidbody = snakeRef.GetComponent<Rigidbody2D>();
    }

    public void Tick()
    {
        float diff = 0.50f;
        float none = 0;
        switch (direction)
        {
            case DIRECTION.East: Move(diff, none); break;
            case DIRECTION.West: Move(-diff, none); break;
            case DIRECTION.South: Move(none, -diff); break;
            case DIRECTION.North: Move(none, diff); break;
            default: Debug.LogError("UNKNOWN DIRECTION DETECTED!"); break;
        }
    }

    public void PlayerInput()
    {
        if (Input.GetKey("up")) direction = DIRECTION.North;
        if (Input.GetKey("down")) direction = DIRECTION.South;
        if (Input.GetKey("left")) direction = DIRECTION.West;
        if (Input.GetKey("right")) direction = DIRECTION.East;
    }

    void Move(float x, float y)
    {
        float pointX = (rigidbody.transform.position.x + x);
        float pointY = (rigidbody.transform.position.y + y);
        rigidbody.transform.position = new Vector3(pointX, pointY, 0);
    }

}