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
    // Start is called before the first frame update
    void Start()
    {
        m_player= GameObject.Find("Player");
        m_playerController = m_player.GetComponent<PlayerController>();
        m_transform = this.GetComponent<Transform>();
        m_rigidBody = this.GetComponent<Rigidbody>();
        m_stopTime = Random.Range(5.0f, 10.0f);
    }

    void BarkMove()
    {
        const float SPEED = 1300.0f;

        Vector3 moveSpeed = m_playerController.GetBarkVector();
        moveSpeed.Normalize();
        moveSpeed *= SPEED;
        moveSpeed = Quaternion.Euler(0, Random.Range(-25.0f, 25.0f), 0) * moveSpeed;
        m_rigidBody.velocity = moveSpeed * Time.deltaTime;
        m_isBarkMove = true;

    }
    void BarkOut()
    {
        const float TIME = 1.5f;

        if(m_isBarkMove)
        {
            m_barkMoveTimer += Time.deltaTime;
            if(m_barkMoveTimer >= TIME)
            {
                m_isBarkMove = false;
                m_barkMoveTimer = 0.0f;
            }
        }
    }
    void DogDistance()
    {
        const float DISTANCE = 5.0f * 5.0f;

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
            if(distance.sqrMagnitude <= DISTANCE)
            {
                const float SPEED = 1500.0f;
                Debug.Log(distance);
                distance.Normalize();
                distance *= SPEED;
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
        const float SPEED = 1000.0f;

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
            m_rigidBody.velocity = moveSpeed * Time.deltaTime * SPEED;
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
    // Update is called once per frame
    void Update()
    {
        DogDistance();
        BarkOut();
        Move();
        Rotation();
    }
}
