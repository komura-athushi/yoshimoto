using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    Transform m_transform;
    GameObject m_player;
    PlayerController m_playerController;
    float m_stopTimer = 0.0f;
    float m_stopTime = 0.0f;
    public float DOGDISTANCE = 10.0f * 10.0f;
    //アニメ
    Animator m_anime;
    Vector3 latestPos;
    Vector3 speed;
    public Timer m_timer;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.GetComponent<Transform>();
        //アニメ
        m_anime = this.GetComponent<Animator>();
        m_player = GameObject.Find("Player");
        m_playerController = m_player.GetComponent<PlayerController>();
        m_stopTime = Random.Range(3.0f, 1.0f);
        Animation();


        m_timer = GameObject.Find("GameDirector").GetComponent<Timer>();

    }

    void Rotation()
    {
        m_stopTimer += Time.deltaTime;
        if(m_stopTimer >= m_stopTime)
        {
            Quaternion rot = Quaternion.identity;
            Vector3 front = Vector3.zero;
            front.x = Random.Range(-1.0f, 1.0f);
            front.z = Random.Range(-1.0f, 1.0f);
            rot.SetLookRotation(front);
            m_transform.rotation = rot;
            m_stopTimer = 0.0f;
            m_stopTime = Random.Range(3.0f, 7.0f);
        }
    }
    void EscapeDog()
    {
        if(m_playerController.GetisEscape())
        {
            return;
        }
        Vector3 distance = m_player.transform.position - m_transform.position;
        if(distance.sqrMagnitude <= DOGDISTANCE)
        {

            Vector3 front = m_transform.forward;
            distance.Normalize();
            float angle = Mathf.Acos(Vector3.Dot(front, distance));
            if (Mathf.Abs(angle) <= Mathf.PI * 0.25f)
            {
                m_playerController.SetisEscape(front);
            }
        }
    }
    void Animation()
    {
        m_anime.Play("Walk");
    }
    // Update is called once per frame
    void Update()
    {
        if(!m_timer.GetisStart() || m_timer.GetisEnd())
        {
            return;
        }

        Rotation();
        EscapeDog();
    }
}
