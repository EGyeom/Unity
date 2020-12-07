using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFxFactory; //총알이펙트 프리팹
    public GameObject bombFactory; //폭탄 프리팹
    public GameObject firePoint;    //폭탄 발사위치
    public GameObject fireeffectFactory;
    public Transform weaponPoint;
    public float power = 20.0f;     //폭탄 던질 파워

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        //마우스 왼쪽버튼 클릭시 레이캐스트로 총알발사
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;

            //레이랑 충돌했냐?
            if(Physics.Raycast(ray,out hitInfo))
            {
                print("충돌오브젝트 : " + hitInfo.collider.name);

                //내가 쏜 레이가 에너미와 충돌했으니 에너미 체력깍기
                EnemyFSM enemy = hitInfo.collider.GetComponent<EnemyFSM>();
                enemy.hitDamage(10);
                //hitInfo.collider.gameObject.GetComponent<EnemyFSM>().hitDamage(10);
                //hitInfo.transform.GetComponent<EnemyFSM>().hitDamage(10);


                GameObject fireEffect = Instantiate(fireeffectFactory);
                fireEffect.transform.position = weaponPoint.position;

                //충돌지점에 총알이펙트만 생성하면 된다
                //총알이펙트 생성
                GameObject bulletFx = Instantiate(bulletFxFactory);
                //부딪힌 지점 hitInfo안에 정보들이 담겨있다
                bulletFx.transform.position = hitInfo.point;
                //파편이 부딪힌 지점이 향하는 방향으로 튀게 해줘야 한다
                bulletFx.transform.forward = hitInfo.normal;

                //진짜 중요 중요 중요
                //유니티 최적화
                //1. 오브젝트 풀링
                //2. 정점수 줄이기 LOD
                //3. 파티클에서 3D모델보다는 가급적 스프라이트 사용하기
                //4. 비어있는 함수 제거하기
                //5. 레이어 마스크 사용 충돌처리
                
                /*
                //유니티 내부적으로 속도향상을 위해 비트연산 처리가 된다
                //총 32비트를 사용하기 때문에 레이어도 32개까지 추가 가능함
                int layer = gameObject.layer;
                //layer = 1 << 8;
                //0000 0000 0000 0001 => 0000 0001 0000 0000
                layer = 1 << 8 | 1 << 9 | 1 << 12;
                //0000 0001 0000 0000 => player
                //0000 0010 0000 0000 => enemy
                //0001 0000 0000 0000 => boss
                //0001 0011 0000 0000 => P, E, B 모두다 충돌처리 가능
                //if(Physics.Raycast(ray, out hitInfo, 100, layer)) //layer만 충돌
                if(Physics.Raycast(ray, out hitInfo, 100, ~layer)) //layer만 충돌제외
                {
                    //if(플레이어와 충돌했다면)
                    //if(에너미와 충돌했다면)
                    //if(보스와 충돌했다면)
                    //이런식이면 if문이 많이 들어가게 되고
                    //당연히 성능이 조금이라도 떨어질 수밖에 없다
                    //비트연산은 성능 최적화에 도움이 된다
                }
                */
            }
        }

        //마우스우측 버튼 클릭시 수류탄 던지기
        if(Input.GetMouseButtonDown(1))
        {
            //수류탄 생성
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePoint.transform.position;
            //폭탄은 플레이어가 던지기 때문에
            //폭탄의 리지드바디를 이용해서 던지면 된다
            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            //전방으로 물리적인 힘을 가한다
            //rb.AddForce(Camera.main.transform.forward * power, ForceMode.Impulse);

            //45도 각도로 발사
            Vector3 dir = Camera.main.transform.forward + (Camera.main.transform.up * 2);
            dir.Normalize();
            rb.AddForce(dir * power, ForceMode.Impulse);

        }

    }
}
