using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private JoyStick control;
    private GameController _GameController;
    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        control = FindObjectOfType(typeof(JoyStick)) as JoyStick;
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        Application.targetFrameRate = 60;
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float posY = transform.position.y;
        float posX = transform.position.x;
        playerRb.velocity = new Vector2(horizontal * _GameController.velocityMovement, vertical * _GameController.velocityMovement);

        float h = control.Horizontal();
        float v = control.Vertical();
        playerRb.velocity = new Vector2(h * _GameController.velocityMovement, v * _GameController.velocityMovement);

        // Verifica o limite X
        if (transform.position.x > _GameController.limitMaxX)
        {
            posX = _GameController.limitMaxX;
        }
        else if (transform.position.x < _GameController.limitMinX)
        {
            posX = _GameController.limitMinX;
        }

        // Verifica o limite Y
        if (transform.position.y > _GameController.limitMaxY)
        {
            posY = _GameController.limitMaxY;
        }
        else if (transform.position.y < _GameController.limitMinY)
        {
            posY = _GameController.limitMinY;
        }

        transform.position = new Vector2(posX, posY);
    }

    void OnTriggerEnter2D()
    {
        _GameController.changeScene("GameOver");
    }
}
