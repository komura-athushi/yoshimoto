using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    Transform m_transform;
    Rigidbody m_rigidbody;
    GameObject m_player;
    Rigidbody m_playerRigidbody;
    PlayerController m_playerController;
    float m_stopTimer = 0.0f;
    float m_stopTime = 0.0f;
    public float DOGDISTANCE = 10.0f * 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.GetComponent<Transform>();
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_player = GameObject.Find("Player");
        m_playerRigidbody = m_player.GetComponent<Rigidbody>();
        m_playerController = m_player.GetComponent<PlayerController>();
        m_stopTime = Random.Range(3.0f, 1.0f);
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
                Debug.Log("A");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Rotation();
        EscapeDog();
    }
}
