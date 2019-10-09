using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    GameObject m_player;
    PlayerController m_playerController;
    Transform m_transform;
    bool m_isBarkMove = false;
    float m_barkMoveTimer = 0.0f;
    float m_stopTimer = 0.0f;
    float m_stopTime = 0.0f;
    Rigidbody m_rigidBody;
    bool m_isCapture = false;
    public float BARKSPEED = 1300.0f;
    public float BARKMOVETIME = 1.5f;
    public float DOGDISTANCE = 5.0f * 5.0f;
    public float DOGMOVESPEED = 1500.0f;
    public float MOVESPEED = 1000.0f;
    public float LIMITEDZ = 1000.0f; 
    public int m_point = 0;
    //アニメ追加ぁぁ
    Animator m_anime;


    public Timer m_timer;
    //捕まった！
    public void SetisCapture()
    {
        m_isCapture = true;
    }
    //捕まった？
    public bool GetisCapture()
    {
        return m_isCapture;
    }
    //こいつの持ってるポイントを取得
    public int GetPoint()
    {
        return m_point;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_player= GameObject.Find("Player");
        m_playerController = m_player.GetComponent<PlayerController>();
        m_transform = this.GetComponent<Transform>();
        m_rigidBody = this.GetComponent<Rigidbody>();
        m_anime = this.GetComponent<Animator>();
        m_stopTime = Random.Range(5.0f, 10.0f);


        m_timer = GameObject.Find("GameDirector").GetComponent<Timer>();

    }

    void BarkMove()
    {
      

        Vector3 moveSpeed = m_playerController.GetBarkVector();
        moveSpeed.y = 0.0f;
        moveSpeed.Normalize();
        moveSpeed *= BARKSPEED;
        moveSpeed = Quaternion.Euler(0, Random.Range(-25.0f, 25.0f), 0) * moveSpeed;
        m_rigidBody.velocity = moveSpeed * Time.deltaTime;
        m_isBarkMove = true;

    }
    void BarkOut()
    {
        if(m_isCapture)
        {
            return;
        }
        if(m_isBarkMove)
        {
            m_barkMoveTimer += Time.deltaTime;
            if(m_barkMoveTimer >= BARKMOVETIME)
            {
                m_isBarkMove = false;
                m_barkMoveTimer = 0.0f;
            }
        }
    }
    void DogDistance()
    {
        if(m_isCapture)
        {
            return;
        }
        if(m_playerController.GetisBark() && !m_isBarkMove)
        {
            Vector3 distance = m_transform.position - m_player.transform.position;
            if (distance.sqrMagnitude <= m_playerController.GetBarkVector().sqrMagnitude)
            {
                distance.y = 0.0f;
                distance.Normalize();
                float angle = Mathf.Acos(Vector3.Dot(distance, m_playerController.GetBarkVector().normalized));
                if (Mathf.Abs(angle) <= Mathf.PI * 0.25f)
                {
                    BarkMove();
                    //Debug.Log();
                }
            }
 
        }
        else
        {
            Vector3 distance = m_transform.position - m_player.transform.position;
            if(distance.sqrMagnitude <= DOGDISTANCE)
            {
                //Debug.Log(distance);
                distance.Normalize();
                distance *= DOGMOVESPEED;
                float ram = Random.Range(-1, 1);
                float degree = 0.0f;
                if(ram > 0.2f)
                {
                    degree = Random.Range(-90.0f, -50.0f);
                }
                else if(ram < -0.2f)
                {
                    degree = Random.Range(50.0f, 90.0f);
                }
                else
                {
                    degree = Random.Range(-40.0f, 40.0f);
                }
                distance = Quaternion.Euler(0.0f, degree, 0.0f) * distance;
                m_rigidBody.velocity = distance * Time.deltaTime; ;
                m_stopTimer = 0.0f;
               
            }

        }
    }
    void Move()
    {
        if (m_isBarkMove)
        {
            return;
        }
        m_stopTimer += Time.deltaTime;
        if (m_stopTimer >= m_stopTime)
        {
            Vector3 moveSpeed = Vector3.zero;
            moveSpeed.x = Random.Range(-1.0f, 1.0f);
            moveSpeed.z = Random.Range(-1.0f, 1.0f);
            m_rigidBody.velocity = moveSpeed * Time.deltaTime * MOVESPEED;
            m_stopTimer = 0.0f;
            m_stopTime = Random.Range(3.0f, 5.0f);
           
        }

    }

    void Rotation()
    {
        Vector3 front = m_rigidBody.velocity;
        front.y = 0.0f;
        if(front.sqrMagnitude >= 1.0f)
        {
            Quaternion rot = Quaternion.identity;
            rot.SetLookRotation(front);
            m_transform.rotation = rot;
        }
    }

    void Animation()
    {
        if (m_rigidBody.velocity.sqrMagnitude >= 2.0f)
        {
            m_anime.SetInteger("Walk", 1);
            //Debug.Log("動いてるねぇ!");
        }
        else
        {
            m_anime.SetInteger("Walk", 0);
            //Debug.Log("動いてないねぇ!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_timer.GetisStart() || m_timer.GetisEnd())
        {
            return;
        }
        if(m_isCapture)
        {
            m_isBarkMove = false;
        }
        DogDistance();
        BarkOut();
        Move();
        Rotation();
        Animation();

        if(m_isCapture && m_transform.position.z <= LIMITEDZ)
        {
            m_transform.position = new Vector3(m_transform.position.x,m_transform.position.y,LIMITEDZ);
        }
    }
}
