using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool canMove = true;
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            UpdateMovement();
        }
    }

    void UpdateMovement()
    {
        var inputHorizontal = Input.GetAxisRaw("Horizontal");
        var inputVertical = Input.GetAxisRaw("Vertical");
        var moveVector = new Vector3(inputHorizontal, 0, inputVertical).normalized * moveSpeed;

        controller.Move(moveVector * Time.deltaTime);
    }
}
