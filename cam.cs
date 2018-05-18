using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour
{

    public Transform target;
    private float minVertical = 0f;
    private float maxVertical = 85f;
    private float x = 0.0f;
    private float y = 0.0f;
    private float distance = 0.0f;

    private float newdis = 0;
    private float olddis = 0;
    // Use this for initialization
    void Start()
    {
        distance = (transform.position - target.position).magnitude;
        print(distance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        float dt = Time.deltaTime;
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {

            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    float x1 = Input.GetAxis("Mouse X");
                    float y1 = Input.GetAxis("Mouse Y");
                    x += x1 * dt * 150;
                    y += -y1 * dt * 150;
                    SetPos(x, y);

                }
            }

            print("sdasd");
            if (Input.touchCount == 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    Vector3 s1 = Input.GetTouch(0).position;
                    Vector3 s2 = Input.GetTouch(1).position;
                    newdis = Vector2.Distance(s1, s2);
                    if (newdis > olddis)
                    {
                        if ((transform.position - target.position).magnitude > 4)
                        {
                            distance -= Time.deltaTime * 50f;
                        }
                    }
                    if (newdis < olddis)
                    {
                        if ((transform.position - target.position).magnitude < 50)
                        {
                            distance += Time.deltaTime * 50f;
                        }
                    }
                    print("distance = " + distance);
                    SetPos(x, y);
                    olddis = newdis;
                }

            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {

                float x1 = Input.GetAxis("Mouse X");
                float y1 = Input.GetAxis("Mouse Y");
                x += x1 * dt * 150f;
                y += -y1 * dt * 150f;
                SetPos(x, y);

            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                distance -= Input.GetAxis("Mouse ScrollWheel");
                SetPos(x, y);
            }
        }

    }


    void SetPos(float x, float y)
    {
        y = ClampAngle(y, minVertical, maxVertical);
        var rotation = Quaternion.Euler(y, x, 0.0f);
        var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        transform.rotation = rotation;
        transform.position = position;
    }


    static float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
