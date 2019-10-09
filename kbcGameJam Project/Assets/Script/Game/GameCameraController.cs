using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    Transform m_target;
    Transform m_transform;
    float m_degreeH = 180.0f;
    float m_degreeV = 50.0f;
    Vector3 m_front = Vector3.zero;
    Vector3 m_right = Vector3.zero;
    public float radius = 8.0f;
    public float targetHeight = 1.5f;
    public float targetFront = 0.3f;
    public float SPEED = 2.3f * 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_target = GameObject.Find("Player").GetComponent<Transform>();
        m_transform = this.GetComponent<Transform>();
     
    }
    public Vector3 GetFront()
    {
        return m_front;
    }
    public Vector3 GetRight()
    {
        return m_right;
    }

    [System.Obsolete]
    void TPSCamera()
    {
        
        float degreeH = Input.GetAxis("Horizontal2") * SPEED * Time.deltaTime;
        float degreeV = 0.0f; //= Input.GetAxis("Vertical2") * 2.3f;

        m_degreeH += degreeH;
        m_degreeV += degreeV;

        if (m_degreeV >= 40.0f)
        {
            m_degreeV = 40.0f;
        }
        else if (m_degreeV <= -15.0f)
        {
            m_degreeV = -15.0f;
        }
        float angleH = Mathf.PI / 180.0f * m_degreeH;
        float angleV = Mathf.PI / 180.0f * m_degreeV;
        //float angleH = m_degreeH;
        //float angleV = m_degreeV;


        Vector3 targetCamPos = Vector3.zero;
        targetCamPos.y += targetHeight;
        targetCamPos += m_target.position;

        Quaternion qRot = Quaternion.identity;
        Vector3 axisY = new Vector3(0.0f, 1.0f, 0.0f);
        qRot.SetAxisAngle(axisY, angleH);

        Vector3 toPos = new Vector3(0.0f, 0.0f, 1.0f);
        toPos = qRot * toPos;

        Vector3 rotAxis = Vector3.zero;
        rotAxis = Vector3.Cross(toPos, axisY);
        rotAxis = rotAxis.normalized;

        qRot.SetAxisAngle(rotAxis, angleV);
        toPos = qRot * toPos;
        toPos *= radius;
        Vector3 position = targetCamPos + toPos;

        m_transform.position = position;
        toPos.y = 0.0f;
        targetCamPos -= toPos * targetFront;
        m_transform.LookAt(targetCamPos);

        m_front = targetCamPos - position;
        m_front = m_front.normalized;
        m_right = m_front;
        Quaternion rot = Quaternion.identity;
        rot.SetAxisAngle(axisY, -90.0f * Mathf.PI / 180.0f);
        m_right = rot * m_right;
    }
    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        TPSCamera();
    }
}
