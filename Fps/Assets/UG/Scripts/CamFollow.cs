using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //카메라가 플레이어 따라다니기
    //플레이어 자식으로 붙여서 이동해도 상관없다
    //하지만 게임에 따라서 드라마틱한 연출이 필요할때라던지...
    //게임 기획에 따라 1인칭또는 3인칭등 변경이 필요할 수 있다
    //지금은 우리 눈 역할을 할거라서 그냥 순간이동 시킨다
    
    public float speed = 10.0f;      //카메라 이동속도
    public Transform target1st;     //1인칭 카메라 타겟
    public Transform target3rd;     //3인칭 카메라 타겟
    bool isFPS = true;             //1인칭이냐? 3인칭이냐?


    // Update is called once per frame
    void Update()
    {
        transform.position = target1st.position;
        //카메라가 타겟 따라다니기
        //FollowTarget();

        //1인칭 to 3인칭, 3인칭 to 1인칭으로 카메라 변경
        //ChangeView();
    }

    private void ChangeView()
    {
        if (Input.GetKeyDown("1"))
        {
            isFPS = true;
        }
        if (Input.GetKeyDown("3"))
        {
            isFPS = false;
        }

        if (isFPS)//1인칭이냐?
        {
            //카메라 위치를 강제로 타겟위치에 고정해둔다
            transform.position = target1st.position;
        }
        else//3인칭이냐?
        {
            //카메라 위치를 강제로 타겟위치에 고정해둔다
            transform.position = target3rd.position;
        }
    }

    private void FollowTarget()
    {
        //타겟방향 구하기 (벡터의 뺄셈)
        //방향 = 타겟 - 자기자신
        //Vector3 dir = target.position - transform.position;
        //dir.Normalize();
        //transform.Translate(dir * speed * Time.deltaTime);

        //문제점 : 타겟에 도착하면 덜덜덜 거림
        //거리를 구해서 고정시키면 된다
        //최적화 관련 면접 질문으로 종종 나온다
        //거리구하는 공식
        //float x = x2 - x1;
        //float y = y2 - y1;
        //return sqrtf(x * x + y * y);
        //1. 벡터안의 Distance() 함수 사용 
        //2. 벡터안의 magnitude 속성 사용 
        //3. 벡터안의 sqrMagnitude 속성 사용

        //1. Distance()
        //if(Vector3.Distance(target.position, transform.position) < 1.0f)
        //{
        //    transform.position = target.position;
        //}

        //2. magnitude
        //float distance = (target.position - transform.position).magnitude;
        //if(distance < 1.0f) transform.position = target.position;

        //3. sqrMagnitude (정확한 값은 아니고 크기 비교만 할때 사용한다)
        //성능상 유리하다 왜냐면 루트연산을 하지 않는다
        //float distance = (target.position - transform.position).sqrMagnitude;
        //if(distance < 1.0f) transform.position = target.position;

        //위와같이 하고도 덜덜거리는 경우
        //FollowTarget을 LateUpdate()에 넣으면 해결된다 꿀팁!!
    }
}
