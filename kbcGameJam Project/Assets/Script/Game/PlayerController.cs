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
    bool m_isStartEscape = false;
    Vector3 m_escapeVector = Vector3.zero;
    public float BARKLONG = 10.0f;
    public float ESCAPESPEED = 500.0f;
    public float MOVESPEED = 600.0f;
    public float m_isEscapeTime = 1.5f;
    //アニメーションに必要なため変数を追加しました。
    Animator m_anim;

    public Timer m_timerr;
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
        m_escapeVector = vector;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.GetComponent<Transform>();
        m_rigidbody = this.GetComponent<Rigidbody>();
        //アニメーション用に追加
        m_anim = this.GetComponent<Animator>();
        m_gameCameraController = GameObject.Find("GameCamera").GetComponent<GameCameraController>();

        m_timerr = GameObject.Find("GameDirector").GetComponent<Timer>();
    }

    void LookDownCamera()
    {
        Vector3 moveSpeed = Vector3.zero;
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
        if(m_isBark || m_isEscape)
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
        m_rigidbody.velocity = moveSpeed * Time.deltaTime;
        if(moveSpeed.magnitude <= 0.01f)
        {
            return;
        }
        Quaternion rot = Quaternion.identity;
        rot.SetLookRotation(moveSpeed);
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
    void Escape()
    {
        if (m_isEscape)
        {
            if (!m_isStartEscape)
            {
                m_rigidbody.velocity = m_escapeVector * ESCAPESPEED * Time.deltaTime;
                m_isStartEscape = true;
                Quaternion rot = Quaternion.identity;
                rot.SetLookRotation(m_escapeVector);
                transform.rotation = rot;
            }
            m_isEscapeTimer += Time.deltaTime;
            if (m_isEscapeTimer >= m_isEscapeTime)
            {
                m_isEscape = false;
                m_isStartEscape = false;
                m_isEscapeTimer = 0.0f;
            }
        }
    }

    void Animation()
    {
        if (m_rigidbody.velocity != Vector3.zero)
        {
            m_anim.SetInteger("Walk", 1);
        }
        else
        {
            m_anim.SetInteger("Walk",0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_timerr.GetisStart() || m_timerr.GetisEnd())
        {
            return;
        }

        MoveRot();
        Bark();
        Escape();

        Animation();
    }

}
