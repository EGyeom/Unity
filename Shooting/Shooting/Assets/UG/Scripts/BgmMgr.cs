using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmMgr : MonoBehaviour
{
    //BGMMgr 싱글톤 만들기
    //모든씬에서 사용가능해야 하니 BGMMgr를 삭제하면 안된다
    public static BgmMgr Instance;      //BGMMgr 싱글톤 인스턴스(객체)
    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    Dictionary<string, AudioClip> bgmTable; //BGM파일들을 담아놓을 딕셔너리 (STL Map)

    AudioSource audioMain;                  //메인오디오
    AudioSource audioSub;                   //서브오디오(BGM 교체시 사용함)

    [Range(0, 1.0f)]                        //[]로 만들어져 있는 Attribute라고 하고 인스펙터창 값을 0 ~ 1로 고정
    public float masterVolume = 1.0f;        //마스터 볼륨은 0 ~ 1.0f 값
    float volumeMain = 0.0f;                 //메인오디오 볼륨
    float volumeSub = 0.0f;                  //서브오디오 볼륨
    float crossFadeTime = 3.0f;              //크로스페이드 타임 3초

    void Start()
    {
        //BGM테이블 생성
        bgmTable = new Dictionary<string, AudioClip>();
        //오디오 소스 코드로 추가
        audioMain = gameObject.AddComponent<AudioSource>();
        audioSub = gameObject.AddComponent<AudioSource>();
        //오디오 소스 볼륨 0으로 초기화
        audioMain.volume = 0.0f;
        audioSub.volume = 0.0f;
    }

    void Update()
    {
        //브금이 플레이중일때 메인볼륨은 올리고 서브볼륨은 내린다
        if (audioMain.isPlaying)
        {
            //메인오디오 볼륨 올리기
            if (volumeMain < 1.0f)
            {
                volumeMain += Time.deltaTime / crossFadeTime;
                if (volumeMain >= 1.0f) volumeMain = 1.0f;
            }
            //서브오디오 볼륨 내리기
            if (volumeSub > 0.0f)
            {
                volumeSub -= Time.deltaTime / crossFadeTime;
                if (volumeSub <= 0.0f)
                {
                    volumeSub = 0.0f;
                    //서브오디오 정지
                    audioSub.Stop();
                }
            }
        }

        //볼륨조정
        audioMain.volume = volumeMain * masterVolume;
        audioSub.volume = volumeSub * masterVolume;
    }

    //BGM플레이
    public void PlayBGM(string bgmName)
    {
        //딕셔너리 안에 브금이 없으면 리소스폴더에서 찾아서 새로 추가하자
        if (bgmTable.ContainsKey(bgmName) == false)
        {
            //유니티엔진에서 특별한 기능의 Resources 폴더가 존재함
            //어디에서든 파일을 로드할 수 있다
            //단 스펠링 주의

            //Resources/BGM/ 폴더안에서 오디오클립을 찾는다
            AudioClip bgm = (AudioClip)Resources.Load("BGM/" + bgmName);
            //AudioClip bgm = Resources.Load("BGM/" + bgmName) as AudioClip;

            //리소스폴더에 bgm이 없다면 딕셔너리에 추가하지말고 그냥 리턴하고 나온다
            //오디오파일이 없으니 재생할 수 없다
            if (bgm == null) return;

            //딕셔너리에 bgmName의 키값으로 bgm을 추가하자
            bgmTable.Add(bgmName, bgm);
        }

        //메인오디오의 클립에 새로운 오디오클립을 연결한다
        audioMain.clip = bgmTable[bgmName];
        //메인오디오 플레이 하기
        audioMain.Play();

        //볼륨값 세팅
        volumeMain = 1.0f;
        volumeSub = 0.0f;
    }

    //브금 크로스페이드 플레이
    public void CrossFadeBGM(string bgmName, float cfTime = 1.0f)
    {
        //딕셔너리 안에 브금이 없으면 리소스폴더에서 찾아서 새로 추가하자
        if (bgmTable.ContainsKey(bgmName) == false)
        {
            //유니티엔진에서 특별한 기능의 Resources 폴더가 존재함
            //어디에서든 파일을 로드할 수 있다
            //단 스펠링 주의

            //Resources/BGM/ 폴더안에서 오디오클립을 찾는다
            AudioClip bgm = (AudioClip)Resources.Load("BGM/" + bgmName);
            //AudioClip bgm = Resources.Load("BGM/" + bgmName) as AudioClip;

            //리소스폴더에 bgm이 없다면 딕셔너리에 추가하지말고 그냥 리턴하고 나온다
            //오디오파일이 없으니 재생할 수 없다
            if (bgm == null) return;

            //딕셔너리에 bgmName의 키값으로 bgm을 추가하자
            bgmTable.Add(bgmName, bgm);
        }

        //크로스페이드 타임
        crossFadeTime = cfTime;

        //메인오디오에서 플레이 되고 있는걸 서브오디오로 변경
        AudioSource temp = audioMain;
        audioMain = audioSub;
        audioSub = temp;

        //볼륨값도 스위칭
        float tempVolume = volumeMain;
        volumeMain = volumeSub;
        volumeSub = tempVolume;

        //메인오디오의 클립에 새로운 오디오 클립을 연결한다
        audioMain.clip = bgmTable[bgmName];
        //메인오디오 플레이 하기
        audioMain.Play();
    }

    //일시정지
    public void PauseBGM()
    {
        audioMain.Pause();
    }
    //다시재생
    public void ResumeBGM()
    {
        audioMain.Play();
    }
}
