using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //네비게이션을 사용할 수 있다

public class EnemyFSM : MonoBehaviour
{
    //에너미 상태
    enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }
    EnemyState state; //에너미 상태변수

    //유용한 기능
    #region "Idel 상태에 필요한 변수들"

    #endregion

    #region "Move 상태에 필요한 변수들"

    #endregion

    #region "Attack 상태에 필요한 변수들"

    #endregion

    #region "Return 상태에 필요한 변수들"

    #endregion

    #region "Damaged 상태에 필요한 변수들"

    #endregion

    #region "Die 상태에 필요한 변수들"

    #endregion


    //필요한 변수들
    public float findRange = 15f;     //플레이어를 찾는 범위
    public float moveRange = 30f;    //시작지점에서 최대 이동가능한 범위
    public float attackRange = 2f;   //공격 가능 범위
    Vector3 startPoint;             //에너미 시작위치
    Transform player;               //플레이어를 찾기위해서
    CharacterController cc;         //에너미 이동을 위해서

    //에너미 일반변수
    int hp = 100; //체력
    int att = 5; //공격력
    float speed = 5.0f; //이동속도

    //공격 딜레이
    float attTime = 2.0f; //2초에 한번 공격
    float timer = 0.0f;

    //애니메이션을 제어하기 위한 애니메이터 컴포넌트
    Animator anim;

    //유니티 길찾기 알고리즘이 적용된 네비게이션을 사용하려면 
    //반드시 UnityEngine.AI를 추가해줘야 한다
    //네비게이션이 2D에서 했던 길찾기 알고리즘보다 성능이 좋다
    //2D기반 A*같은경우는 본인 위치에서 실시간으로 계산을 해야 하는 반면
    //유니티 네비게이션은 맵전체를 베이크 해서 에이전트가 어느 위치에 있던
    //미리 계산된 정보를 사용한다
    NavMeshAgent agent;
    //FSM기반으로 코드를 짜는 경우 주의해야 할 사항
    //충돌은 콜리더로 하고
    //이동만 네비메시에이전트를 사용해야
    //EnemyFSM을 제대로 사용할 수 있다
    //충돌이 제대로 작동안 할 수도 있다
    //따라서 시작할때 네비메시에이전트는 꺼줘야 한다
    //게임오브젝트 -> 활성, 비활성 SetActive(true or false)
    //컴포넌트 -> 활성, 비활성 ?? enalbe = true or false
    

    void Start()
    {
        //에너미 상태 초기화
        state = EnemyState.Idle;
        //시작지점 저장
        startPoint = transform.position;
        //플레이어 찾기
        player = GameObject.Find("Player").transform;
        //캐릭터 컨트롤로 컴포넌트
        cc = GetComponent<CharacterController>();
        //애니메이터 컴포넌트
        anim = GetComponentInChildren<Animator>();

        //네비메시에이전트 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //에너미 상태에 따른 행동처리
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
    }

    //대기상태
    private void Idle()
    {
        //1. 플레이와 일정범위가 되면 이동상태로 변경 (탐지범위)
        //- 플레이어 찾기 (GameObject.Find("Player")) 타겟찾기
        //- 일정범위 20미터 (거리비교 : Distance, magnitude 아무거나 사용)
        //- 상태변경 -> 이동
        //- 상태전환 출력

        if(Vector3.Distance(transform.position, player.position) < findRange)
        {
            state = EnemyState.Move;
            print("상태전환 : Idle -> Move");
            //애니메이션 상태전환
            anim.SetTrigger("Move");
        }
         
    }

    //이동상태
    private void Move()
    {
        //이동은 네비게이션을 사용한다
        if (!agent.enabled) agent.enabled = true;


        //1. 플레이어를 향해서 이동 후 공격범위 안에 들어오면 공격상태로 변경
        //2. 플레이어를 추격하더라도 처음위치에서 일정범위를 넘어가면 리턴상태로 변경
        //- 플레이어 처럼 캐릭터컨트롤러를 이용하기
        //- 공격범위 1미터
        //- 상태변경 -> 공격 or 리턴
        //- 상태전환 출력

        //이동 할 수 있는 최대범위를 벗어나면 돌아와야 한다
        if(Vector3.Distance(transform.position, startPoint) > moveRange)
        {
            state = EnemyState.Return;
            print("상태전환 : Move -> Return");
            //애니메이션 상태전환
            anim.SetTrigger("Return");
        }
        //리턴상태가 아니면 플레이어를 추격해야 한다
        else if(Vector3.Distance(transform.position, player.position) > attackRange)
        {
            //플레이어를 향해서 이동해라
            //네비메시에이전트가 회전처리부터 이동까지 전부다 처리해준다
            agent.SetDestination(player.position);




            /*
            //플레이어를 추격
            //이동방향 (벡터의 뺄셈)
            //방향 = 타겟 - 자기자신
            Vector3 dir = (player.position - transform.position).normalized;
            //dir.Normalize();

            //에너미가 백스텝으로 쫓아온다
            //에너미가 타겟을 바라보게 하고 싶다
            //방법1
            //transform.forward = dir;
            //방법2
            //transform.LookAt(player);

            //순간이동이 아닌 좀더 자연스럽게 회전처리를 하고 싶다
            //선형보간
            //transform.forward = Vector3.Lerp(transform.forward, dir, 5 * Time.deltaTime);
            //여기도 문제가 있는데 플레이어와 에너미가 일직선상에 있으면 
            //왼쪽으로 회전해야 할지 오른쪽으로 회전해야 할지 알 수가 없어서
            //그냥 백덤블링을 해버린다 ㅋㅋㅋ

            //유니티 내부적으로 회전은 전부 쿼터니온으로 처리되고 있기때문에
            //자연스러운 회전처리를 하려면 결국 쿼터니온으로 사용해야 한다
            //하지만 이걸 몰라도 크게 상관은 없다
            //결국 우리는 네비메시에이전트를 사용하면 이딴거 다 자동으로 적용된다
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(dir), 10 * Time.deltaTime);

            //에너미 이동
            //cc.Move(dir * speed * Time.deltaTime);
            //중력이 적용안되는 문제가 있다

            //중력문제를 해결하기 위해서 심플무브를 사용한다
            //심플무브는 최소한의 물리가 적용되어 중력문제를 해결할 수 있다
            //단 내부적으로 시간처리를 하기때문에
            //Time.deltaTime을 사용하지 않는다
            cc.SimpleMove(dir * speed);
            */
        }
        //공격범위 안에 들어온 상태
        else
        {
            state = EnemyState.Attack;
            print("상태전환 : Move -> Attack");
            //애니메이션 상태전환
            anim.SetTrigger("Attack");
        }

    }

    //공격상태
    private void Attack()
    {
        //에이전트 오프
        agent.enabled = false;


        //1. 플레이어가 공격범위 안에 있다면 일정한 시간 간격으로 플레이어를 공격
        //2. 플레이어가 공격범위를 벗어나면 이동 상태로 변경
        //- 공격범위 1미터
        //- 상태변경 -> 이동
        //- 상태전환 출력

        //공격범위안에 들어옴
        if(Vector3.Distance(transform.position, player.position) < attackRange)
        {
            //일정 시간마다 플레이어를 공격하기
            timer += Time.deltaTime;
            if(timer > attTime)
            {
                print("공격");
                //플레이어의 필요한 스크립트 컴포넌트를 가져와서 데이지를 주면 된다
                //player.GetComponent<PlayerMove>()......

                //타이머 초기화
                timer = 0.0f;

                //애니메이션 상태전환
                anim.SetTrigger("Attack");
            }
        }
        else //현재상태를 무부를 전환(재추격)
        {
            state = EnemyState.Move;
            print("상태전환 : Attack -> Move");
            //애니메이션 상태전환
            anim.SetTrigger("Move");
            //타이머 초기화
            timer = 0.0f;
        }

    }

    //복귀상태
    private void Return()
    {
        //1. 에너미가 플레이어를 추격하더라도 처음위치에서 일정범위를 벗어나면 다시 돌아옴
        //- 처음위치에서 일정범위 30미터
        //- 상태변경
        //- 상태전환 출력

        //시작위치까지 도달하지 않을때는 이동
        //도착하면 대기상태로 변경
        if(Vector3.Distance(transform.position, startPoint) > 0.1)
        {
            //복귀
            agent.SetDestination(startPoint);

            /*
            Vector3 dir = (startPoint - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            cc.SimpleMove(dir * speed);
            */
        }
        else
        {
            //위치값을 강제로 고정
            transform.position = startPoint;
            transform.rotation = Quaternion.identity;
            //Quaternion.identity => 쿼터니온 회전값을 0으로 초기화 시켜준다
            //TM.reset => TM.identity 완죤 같은 아이라고 생각하면 된다
            state = EnemyState.Idle;
            print("상태전환 : Return -> Idle");
            //애니메이션 상태전환
            anim.SetTrigger("Idle");

            //에이전트 오프
            agent.enabled = false;

        }
    }

    //플레이어쪽에서 충돌감지를 할 수 있으니 이 함수는 퍼블릭으로 만들자
    public void hitDamage(int value)
    {

        //예외처리
        //피격상태이거나, 죽은 상태일때는 데미지 중첩으로 주지 않는다
        if (state == EnemyState.Damaged || state == EnemyState.Die) return;


        //체력깍기
        hp -= value;
        //에너미의 체력이 1이상이라면 피격상태
        if(hp > 0)
        {
            state = EnemyState.Damaged;
            print("상태전환 : AnyState -> Damaged");
            print("HP : " + hp);
            //애니메이션 상태전환
            anim.SetTrigger("Damaged");
            Damaged();
        }
        //0이하면 죽음상태
        else
        {
            state = EnemyState.Die;
            print("상태전환 : AnyState -> Die");
            //애니메이션 상태전환
            anim.SetTrigger("Die");
            hp = 0;

            Die();
        }
    }

    //피격상태 (Any State)
    private void Damaged()
    {
        //코루틴 사용하자
        //1. 에너미 체력이 1이상일때만 피격받을 수 있단
        //2. 다시 이전상태로 변경
        //- 상태변경
        //- 상태전환 출력

        //피격 상태를 처리하기 위한 코루틴 실행
        StartCoroutine(DamageProc());
    }

    //피격상태 처리용 코루틴
    IEnumerator DamageProc()
    {
        //피격모션 시간만큼 기다리기
        yield return new WaitForSeconds(1.0f);
        //현재상태를 이동으로 전환
        state = EnemyState.Move;
        anim.SetTrigger("Move");
        print("상태전환 : Damaged -> Move");
    }

    //죽음상태 (Any State)
    private void Die()
    {
        //코루틴 사용하자
        //1. 체력이 0이하
        //2. 몬스터 오브젝트 삭제
        //- 상태변경
        //- 상태전환 출력

        //진해중인 모든 코루틴은 정지한다
        StopAllCoroutines();

        //죽음상태를 처리하기 위한 코루틴 실행
        StartCoroutine(DieProc());
    }

    IEnumerator DieProc()
    {
        //캐릭터컨트롤러 비활성화
        cc.enabled = false;
        //에이전트 오프
        agent.enabled = false;

        //2초후에 자기자신을 제거한다
        yield return new WaitForSeconds(2.0f);
        print("죽었다");
        Destroy(gameObject);
    }

    //여기에서 정의 해둔 기즈모들은 씬뷰에서만 보인다
    private void OnDrawGizmos()
    {
        //공격가능 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //플레이어 찾을 수 있는 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);
        //시작지점으로 부터 이동가능한 최대 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPoint, moveRange);
    }
}
