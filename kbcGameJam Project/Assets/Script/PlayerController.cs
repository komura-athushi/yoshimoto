using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    Transform m_transform;
    Rigidbody m_rigidbody;
    GameCameraController m_gameCameraController;
    bool m_isBark = false;
    float m_timer = 0.0f;
    bool m_isEscape = false;
    float m_isEscapeTimer = 0.0f;
    float m_isEscapeTime = 2.0f;
    public float BARKLONG = 10.0f;
    public float ESCAPESPEED = 1500.0f;
    public float MOVESPEED = 13.0f;
    //吠えたかどうか
    public bool GetisBark()
    {
        return m_isBark;
    }
    //犬の吠えたベクトルを取得
    public Vector3 GetBarkVector()
    {
        
        return BARKLONG * m_transform.forward;
    }
    //逃げてる?
    public bool GetisEscape()
    {
        return m_isEscape;
    }
    //逃げた！
    public void SetisEscape(Vector3 vector)
    {
        m_isEscape = true;
        m_rigidbody.velocity =vector* ESCAPESPEED * Time.deltaTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.GetComponent<Transform>();
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_gameCameraController = GameObject.Find("GameCamera").GetComponent<GameCameraController>();
    }

    void LookDownCamera()
    {
        

        
        Vector3 moveSpeed = new Vector3(0.0f,0.0f,0.0f);
        moveSpeed.x = Input.GetAxis("Horizontal") * MOVESPEED;
        moveSpeed.z = Input.GetAxis("Vertical") * MOVESPEED;
        //m_transform.position += moveSpeed;
        //rigidbody.velocity = moveSpeed;
        m_rigidbody.velocity = moveSpeed;
        Quaternion rotation = new Quaternion();
        rotation.SetLookRotation(moveSpeed);
        m_transform.rotation = rotation;
    }
    void MoveRot()
    {
        if(m_isBark && m_isEscape)
        {
            return;
        }
     

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
        rot.SetLookRotation(moveSpeed * Time.deltaTime);
        transform.rotation = rot;
    }
    void Bark()
    {
        if(m_isEscape)
        {
            return;
        }
        if(m_isBark)
        {
            m_timer += Time.deltaTime;
            if(m_timer >= 1.0f)
            {
                m_isBark = false;
                m_timer = 0.0f;
            }
        }
        else if(Input.GetKeyDown("joystick button 0")) {
            m_isBark = true;
            m_rigidbody.velocity = Vector3.zero;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        MoveRot();
        Bark();
      
        if(m_isEscape) {
            m_isEscapeTimer += Time.deltaTime;
            if(m_isEscapeTimer >= m_isEscapeTime)
            {
                m_isEscape = false;
                m_isEscapeTimer = 0.0f;
            }
        }
    }
}
