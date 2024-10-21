using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGMcontroller : MonoBehaviour
{
    new AudioSource audio;
    bool Fade = false;
    public Text Stagename;
    public AudioClip A;
    public AudioClip B;
    public AudioClip B_Boss;
    GameObject player;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Fade) //�������X�ɏ���
        {
            audio.volume -= 0.01f;
        }
        else //�����o��
        {
            audio.volume = 0.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //�X�e�[�W�̋��E�ɐG���Ɖ�������
        if (other.gameObject.tag == "StageBorder")
        {
            Fade = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //�X�e�[�W�̋��E����o��Ɖ����o��
        if (other.gameObject.tag == "StageBorder")
        {
            Fade = false;
            //�����̈ʒu�ɂ����BGM��ς���
            if(player.transform.position.y <= -50)
            {
                audio.Stop();
                StageNameChange(2);
                audio.PlayOneShot(B);
            }
            else
            {
                audio.Stop();
                StageNameChange(1);
                audio.PlayOneShot(A);
            }
        }
    }
    void StageNameChange(int i)
    {
        switch(i)
        {
            case 1:
                Stagename.text = string.Format("�n��");
                break;
            case 2:
                Stagename.text = string.Format("���w �s�s�O��");
                break;
        }
        for(float a = 0; a < 20; ++a)
        {
            StartCoroutine(Utilities.DelayMethod(a / 10, () => Stagename.color += new Color(0f, 0f, 0f, 0.05f)));
            StartCoroutine(Utilities.DelayMethod((a + 60) / 10, () => Stagename.color -= new Color(0f, 0f, 0f, 0.05f)));
        }
    }
}
