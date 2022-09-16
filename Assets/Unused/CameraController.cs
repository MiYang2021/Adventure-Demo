using UnityEngine;
using System.Collections;
//已不用
//相机一直拍摄主角的后背
public class CameraController : MonoBehaviour
{

    public Transform target;


    public float distanceUp = 2f;
    public float distanceAway = 3f;
    public float smooth = 2f;//位置平滑移动值
    public float camDepthSmooth = 5f;

    public float Rotatespeed = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标轴控制相机的远近
        if ((Input.mouseScrollDelta.y < 0 && Camera.main.fieldOfView >= 3) || Input.mouseScrollDelta.y > 0 && Camera.main.fieldOfView <= 80)
        {
            Camera.main.fieldOfView += Input.mouseScrollDelta.y * camDepthSmooth * Time.deltaTime;
        }

    }

    void LateUpdate()
    {
        //相机的位置
        Vector3 disPos = target.position + Vector3.up * distanceUp - target.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);
        //相机的角度
        //transform.LookAt(target.position);
        //Vector3 targetLook = new Vector3(Input.mousePosition.x * Time.deltaTime, target.position.y, Input.mousePosition.z * Time.deltaTime);
        //transform.LookAt(targetLook);

        float X = Input.GetAxis("Mouse X") * Rotatespeed;
        float Y = Input.GetAxis("Mouse Y") * Rotatespeed;
        GetComponent<Camera>().transform.localRotation = GetComponent<Camera>().transform.localRotation * Quaternion.Euler(-Y * 2, 0, 0);

        target.transform.localRotation = target.transform.localRotation * Quaternion.Euler(0, X, 0);



    }

    //float yaw;
    //float pitch;
    //public float mousemovespeed = 2;
    //public Transform playertransform;

    //public float distanceUp = 2f;
    //public float distanceAway = 3f;
    //public float smooth = 0.5f;//位置平滑移动值
    //public float camDepthSmooth = 1f;

    //public float Rotatespeed = 1f;
    //void Start()
    //{
    //    //相机的位置
    //    //Vector3 disPos = playertransform.position + Vector3.up * distanceUp - playertransform.forward * distanceAway;
    //    //transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);
    //}
    //void Update()
    //{
    //    //if ((Input.mouseScrollDelta.y < 0 && Camera.main.fieldOfView >= 3) || Input.mouseScrollDelta.y > 0 && Camera.main.fieldOfView <= 80)
    //    //{
    //    //    Camera.main.fieldOfView += Input.mouseScrollDelta.y * camDepthSmooth * Time.deltaTime;
    //    // }
    //    ////相机的位置
    //    //Vector3 disPos = playertransform.position + Vector3.up * distanceUp - playertransform.forward * distanceAway;
    //    //transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);

    //    yaw += Input.GetAxis("Mouse X");
    //    pitch += Input.GetAxis("Mouse Y");
    //    transform.eulerAngles = new Vector3(pitch, yaw, 0);
    //    //transform.position = playertransform.position - transform.forward * 3;
    //    transform.position = playertransform.position - transform.forward * 3 + transform.up * 2;
    //    //Vector3 disPos = playertransform.position + Vector3.up * distanceUp - playertransform.forward * distanceAway;
    //    //transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);
    //}
    //void LateUpdate()
    //{

    //}
}
