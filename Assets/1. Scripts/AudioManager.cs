using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] // Ŭ������ ����ȭ ��Ű�� -> �ν����� â�� �߰� �ϱ� ���ؼ� 
public class Sound
{
    public string name;  // �뷡�� �̸� 
    public AudioClip clip; // �뷡.
}
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance; // �ν��Ͻ�ȭ

    public AudioSource[] audioSourceEffects; // ȿ������ �����ų mp3 �÷��̾�
    public AudioSource audioSourceBgm; // bgm �����ų mp3 �÷��̾� 



    public Sound[] effectSounds; // ȿ������ �뷡�� �̸��� �뷡�� ���� Ŭ���� 
    public AudioClip[] bgmSounds;  // bgm�� �뷡�� �̸��� �뷡�� ���� Ŭ���� 



    private void Awake()
    {
       
        

        if (instance == null) // ���� ��¥�϶� 
        {
            instance = this; // �ν��Ͻ��� ���� �Ҵ����ش�. 

            DontDestroyOnLoad(gameObject);  // �����ż� ������ ����� �� ���� 
        }

        else if (instance != this) // ���� ��¥�� �ƴ϶�� 
        {
            Destroy(gameObject);  // ���ӿ�����Ʈ�� �ı��Ѵ�.
        }


    }

    private void Start()
    {
        audioSourceBgm.volume = 0.7f;
    }

    // ���� ����
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
                        return; // �̹� ����������ϱ� ���� �ٽ� �ݺ��� �� �ʿ�� ������. -> ���ÿ� �����Ҹ��� �� �� �ְԲ� ��.
                    }
                }
                return;
            }
        }


    }


    // ���Ϳ� �������� �� BGM ���� �ڷ�ƾ�Լ� ȣ��
    public void PlayBossSceneBgm()
    {
        StartCoroutine(ChangeBossBgm());
    }

    // ���Ϳ� ���������� �� BGM ����
    public IEnumerator ChangeBossBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(0.5f);
        audioSourceBgm.clip = bgmSounds[3];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }

    // ���丮�� ���� �� BGM ���� �ڷ�ƾ�Լ� ȣ�� 
    public void PlayStorySceneBgm()
    {
        StartCoroutine(StartStorySceneBGM());
    }

    // ���丮�� ���� �� BGM ���� 
    public IEnumerator StartStorySceneBGM()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[1];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }

    // ������ ���� �� BGM ���� �ڷ�ƾ �Լ� ȣ��
    public void PlayEndingBgm()
    { 
       StartCoroutine(EndingBgm());
    }

    // ������ ���� �� BGM ����
    public IEnumerator EndingBgm()
    {
        yield return new WaitForSeconds(8f);
        audioSourceBgm.clip = bgmSounds[4];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }

    // ���ӽ��۾� ���� �� BGM ���� �ڷ�ƾ �Լ� ȣ��
    public void PlayStartGameSceneBgm()
    {
        StartCoroutine(StartGameSceneBgm());
    }

    // ���ӽ��۾� ���� �� BGM ����
    public IEnumerator StartGameSceneBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[7];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
        
    }

    // �������� ���� �� BGM ���� �ڷ�ƾ �Լ� ȣ��
    public void PlayStartBossBgm()
    {
        StartCoroutine(StartBossBgm());
    }

    // �������� ���� �� BGM ����
    public IEnumerator StartBossBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[9];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();

    }

    // �������� ���� �� BGM ���� �ڷ�ƾ�Լ� ȣ�� 
    public void PlayStartFieldBgm()
    {
        StartCoroutine(StartFieldBgm());
    }
    
    // �������� ���� �� BGM ����
    public IEnumerator StartFieldBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[2];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();

    }

    // �벿����� ���� �� BGM ���� �ڷ�ƾ�Լ� ȣ�� 
    public void PlayStartMountainBgm()
    {
        StartCoroutine(StartMountainBgm());
    }
    // �벿����� ���� �� BGM ����
    public IEnumerator StartMountainBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[8];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();

    }

    // BGM ���� 
    public IEnumerator StopBgm()
    {
        yield return new WaitForSeconds(7f);
        audioSourceBgm.Stop();
    }


    // �غ��� ���� �� BGM ���� �ڷ�ƾ�Լ� ȣ�� 
    public void StartBeachBgm()
    {
        StartCoroutine(BeachBgm());

    }

    // �غ��� ���� �� BGM ����
    public IEnumerator BeachBgm()
    {
        audioSourceBgm.Stop();
        yield return new WaitForSeconds(1f);
        audioSourceBgm.clip = bgmSounds[5];
        audioSourceBgm.volume = 0.4f;
        audioSourceBgm.Play();
    }


    // ó��ȭ���� BGM ���� -> ���� �ٷ� ����ʿ� ���� �ʿ������
    public void StartSceneBgm()
    {
        audioSourceBgm.clip = bgmSounds[0];
        audioSourceBgm.Play();
    }
}
