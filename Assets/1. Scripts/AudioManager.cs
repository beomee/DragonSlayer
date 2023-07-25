using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] // 클래스를 직렬화 시키기 -> 인스펙터 창에 뜨게 하기 위해서 
public class Sound
{
    public string name;  // 노래의 이름 
    public AudioClip clip; // 노래.
}
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance; // 인스턴스화

    public AudioSource[] audioSourceEffects; // 효과음을 재생시킬 mp3 플레이어
    public AudioSource audioSourceBgm; // bgm 재생시킬 mp3 플레이어 



    public Sound[] effectSounds; // 효과음의 노래의 이름과 노래를 담을 클래스 
    public AudioClip[] bgmSounds;  // bgm의 노래의 이름과 노래를 담을 클래스 



    private void Awake()
    {
       
        

        if (instance == null) // 내가 진짜일때 
        {
            instance = this; // 인스턴스에 나를 할당해준다. 

            DontDestroyOnLoad(gameObject);  // 삭제돼서 오류가 생기는 걸 방지 
        }

        else if (instance != this) // 내가 진짜가 아니라면 
        {
            Destroy(gameObject);  // 게임오브젝트를 파괴한다.
        }


    }

    private void Start()
    {
        audioSourceBgm.volume = 0.7f;
    }

    // 사운드 실행
    public void PlaySE(string _name,float _pitch, float _volume)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                     {
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        audioSourceEffects[j].pitch = _pitch;
                        audioSourceEffects[j].volume = _volume;
                        return; // 이미 재생시켰으니까 굳이 다시 반복을 할 필요는 없으니. -> 동시에 여러소리가 날 수 있게끔 함.
                    }
                }
                return;
            }
        }


    }


    // 몬스터와 전투시작 시 BGM 실행 코루틴함수 호출
    public void PlayBossSceneBgm()
    {
        StartCoroutine(ChangeBossBgm());
    }

    // 몬스터와 전투시작할 때 BGM 실행
    public IEnumerator ChangeBossBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(0.5f);
        audioSourceBgm.clip = bgmSounds[3];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }

    // 스토리씬 시작 시 BGM 실행 코루틴함수 호출 
    public void PlayStorySceneBgm()
    {
        StartCoroutine(StartStorySceneBGM());
    }

    // 스토리씬 시작 시 BGM 실행 
    public IEnumerator StartStorySceneBGM()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[1];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }

    // 엔딩씬 시작 시 BGM 실행 코루틴 함수 호출
    public void PlayEndingBgm()
    { 
       StartCoroutine(EndingBgm());
    }

    // 엔딩씬 시작 시 BGM 실행
    public IEnumerator EndingBgm()
    {
        yield return new WaitForSeconds(8f);
        audioSourceBgm.clip = bgmSounds[4];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }

    // 게임시작씬 시작 시 BGM 실행 코루틴 함수 호출
    public void PlayStartGameSceneBgm()
    {
        StartCoroutine(StartGameSceneBgm());
    }

    // 게임시작씬 시작 시 BGM 실행
    public IEnumerator StartGameSceneBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[7];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
        
    }

    // 보스구역 진입 시 BGM 실행 코루틴 함수 호출
    public void PlayStartBossBgm()
    {
        StartCoroutine(StartBossBgm());
    }

    // 보스구역 진입 시 BGM 실행
    public IEnumerator StartBossBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[9];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();

    }

    // 넓은들판 진입 시 BGM 실행 코루틴함수 호출 
    public void PlayStartFieldBgm()
    {
        StartCoroutine(StartFieldBgm());
    }
    
    // 넓은들판 진입 시 BGM 실행
    public IEnumerator StartFieldBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[2];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();

    }

    // 용꼬리계곡 진입 시 BGM 실행 코루틴함수 호출 
    public void PlayStartMountainBgm()
    {
        StartCoroutine(StartMountainBgm());
    }
    // 용꼬리계곡 진입 시 BGM 실행
    public IEnumerator StartMountainBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[8];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();

    }

    // BGM 중지 
    public IEnumerator StopBgm()
    {
        yield return new WaitForSeconds(7f);
        audioSourceBgm.Stop();
    }


    // 해변가 진입 시 BGM 실행 코루틴함수 호출 
    public void StartBeachBgm()
    {
        StartCoroutine(BeachBgm());

    }

    // 해변가 진입 시 BGM 실행
    public IEnumerator BeachBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[5];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }


    // 처음화면의 BGM 실행 -> 게임 바로 종료됨에 따라 필요없어짐
    public void StartSceneBgm()
    {
        audioSourceBgm.clip = bgmSounds[0];
        audioSourceBgm.Play();
    }
}
