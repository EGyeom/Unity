using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    //카메라를 마우스 움직이는 방향으로 회전하기
    public float speed = 50; //회전속도 (Time.deltaTime을 통해서 1초에 150도 회전)
    float angleX, angleY; //직접 제어할 회전각도
    public GameObject weapon;
    public Transform target;
    public GameObject go;
    public float addx = 0;
    public float addz = 0;
    CamRotate value;
    bool isget = false;
    // Update is called once per frame
    private void Start()
    {
        value = GetComponent<CamRotate>();
    }
    void Update()
    {
        if(weapon.activeSelf)
        {
            if(isget == false)
            {
                transform.eulerAngles = new Vector3(-angleY, angleX, 0);
                isget = true;
            }
            else
            {
                Rotate();
            }
        }
        else
        {
            angleX = value.angleX;
            angleY = value.angleY;
        }
    }

    //카메라 회전
    private void Rotate()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        //Vector3 dir = new Vector3(h, v, 0);
        //회전은 각각의 축을 기반으로 회전이 된다
        //Vector3 dir = new Vector3(-v, h, 0);
        //transform.Rotate(dir * speed * Time.deltaTime);


        //유니티엔진에서 제공해주는 함수를 사용함에 있어서
        //Translate함수는 그래도 사용하는데 큰 불편함이 없지만
        //회전을 담당하는 Rotate함수를 사용하면
        //우리가 회전처리를 제어하기 힘들다
        //인스펙터장의 로테이션값은 사용자가 보기 편하도록 편의를 위해서
        //오얼러각도(0도~360도)로 표시되지만
        //내부적으로 쿼터니온으로 회전처리가 되고 있다
        //쿼터니온을 사용하는 이유는 짐벌락현상을 방지할 수 있기때문에 사용한다
        //회전을 직접 제어 할때는 Rotate함수를 사용하지 않고
        //트렌스폼의 오일러앵글을 사용하면 된다


        //이동공식
        //P = P0 + vt;
        //P += vt;
        //transform.position += dir * speed * Time.deltaTime;

        //각도또한 똑같다
        //transform.eulerAngles += dir * speed * Time.deltaTime;
        //카메라가 상하회전각도를 제한해줘야 한다
        //안그러면 백덤블링을 한다
        //Vector3 angle = transform.eulerAngles;
        //angle += dir * speed * Time.deltaTime;
        //if (angle.x > 60) angle.x = 60;
        //if (angle.x < -60) angle.x = -60;
        //transform.eulerAngles = angle;

        //여기에는 또 문제가 있다
        //유니티엔진 내부적으로 -각도는 360도를 더해서 처리된다
        //내가 만든 각도를 가지고 계산처리하자

        angleX += h * speed * Time.deltaTime;
        angleY += v * speed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -60, 60);
        transform.eulerAngles = new Vector3(-angleY, angleX, 0);
    }
}
