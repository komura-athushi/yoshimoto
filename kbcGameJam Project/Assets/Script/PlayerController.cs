using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    Transform m_transform;
    Rigidbody m_rigidbody;
    GameCameraController m_gameCameraController;
    // Start is called before the first frame update
    void Start()
    {
      
        m_transform = this.GetComponent<Transform>();
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_gameCameraController = GameObject.Find("GameCamera").GetComponent<GameCameraController>();
    }

    void LookDownCamera()
    {
        const float SPEED = 10.15f;

        
        Vector3 moveSpeed = new Vector3(0.0f,0.0f,0.0f);
        moveSpeed.x = Input.GetAxis("Horizontal") * SPEED;
        moveSpeed.z = Input.GetAxis("Vertical") * SPEED;
        //m_transform.position += moveSpeed;
        //rigidbody.velocity = moveSpeed;
        m_rigidbody.velocity = moveSpeed;
        Quaternion rotation = new Quaternion();
        rotation.SetLookRotation(moveSpeed);
        m_transform.rotation = rotation;
    }
    void MoveRot()
    {
        const float MOVESPEED = 13.0f;

        Vector3 front = m_gameCameraController.GetFront();
        Vector3 right = m_gameCameraController.GetRight();

        right *= Input.GetAxis("Horizontal") * -MOVESPEED;
        front *= Input.GetAxis("Vertical") * MOVESPEED;

        Vector3 moveSpeed;
        moveSpeed = front + right;
        moveSpeed.y = 0.0f;
        m_rigidbody.velocity = moveSpeed;
        if(moveSpeed.magnitude <= 0.01f)
        {
            return;
        }
        Quaternion rot = new Quaternion();
        rot.SetLookRotation(moveSpeed);
        transform.rotation = rot;
    }
    // Update is called once per frame
    void Update()
    {
        MoveRot();
     
    }
}
