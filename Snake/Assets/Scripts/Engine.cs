/* Author: Armando "Mandiux" Fernandez
 * Date:   06/12/2021
 * Github: https://github.com/DevMando/Unity2D-Snake
 * Description: I felt like building the classic snake game.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour
{

    string[] wallNames = { "Left", "Right", "Down", "Up" };
    List<GameObject> wallList = new List<GameObject>();
    [SerializeField]
    GameObject enemyBot;
    void Start()
    {
        FindWalls();
        InvokeRepeating("CreateBots", 0f, 1f);
        
    }

    void CreateBots()
    {
        GameObject botClone = Instantiate(enemyBot);
        Snake enemyScript = botClone.GetComponent<Snake>();
        int wallNumber = Random.Range(0, 4);
        enemyScript.direction = AssignDirection(enemyScript, wallNumber);
        enemyScript.transform.position = AssignPosition(wallNumber);
    }

    void FindWalls()
    {
        foreach (string wall_name in wallNames)
        {
            wallList.Add(GameObject.Find($"Wall_{wall_name}"));;
        }

        wallList.OrderBy((o) => o);
    }

    Snake.DIRECTION AssignDirection(Snake enemyScript, int number)
    {
            switch (number)
            {
                case 0: return Snake.DIRECTION.RIGHT;
                case 1: return Snake.DIRECTION.LEFT;
                case 2: return Snake.DIRECTION.UP;
                case 3: return Snake.DIRECTION.DOWN;
                default: return Snake.DIRECTION.DOWN;
            }
    }

    Vector3 AssignPosition(int number)
    {
        var oldPosition = wallList[number].transform.position;
        Vector3 position = new Vector3(oldPosition.x, oldPosition.y, 0);
        Debug.Log(position);
        return position;
    }

    void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
