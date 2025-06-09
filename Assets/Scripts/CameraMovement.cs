using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Quaternion m_Rotation;
    Transform m_Transform;
    bool Plsrotate;
    public float speed;

    void Start()
    {
        m_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Rotate();
    }

    void Rotate()
    {

        m_Rotation = Quaternion.LookRotation(m_Transform.position - transform.position);
        if (Quaternion.Angle(transform.rotation, m_Rotation) < 1f)
            return;
        transform.rotation = Quaternion.Slerp(transform.rotation ,m_Rotation, speed * Time.deltaTime);


        
    }
}
