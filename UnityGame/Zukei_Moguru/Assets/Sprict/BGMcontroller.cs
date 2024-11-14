using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGMcontroller : MonoBehaviour
{
    new AudioSource audio;
    public static bool ones = true;
    public static bool Fade = false;
    public Text Stagename;
    public AudioSource A;
    public AudioSource B;
    public AudioSource B_Boss;
    GameObject player;
    static public float Takasa;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Takasa = player.GetComponent<Transform>().position.y;
        if (Takasa % 200 <= -40 && Takasa % 200 >= -50)
        {
            Fade = true;
            ones = true;
        }
        else if (ones)
        {
            StartCoroutine(Utilities.DelayMethod(1, () => audio.Stop()));
            //自分の位置によってBGMを変える
            if (Takasa <= -50)
            {
                StartCoroutine(Utilities.DelayMethod(1, () => StageNameChange(2)));
                StartCoroutine(Utilities.DelayMethod(1, () => B.volume = 0.5f));
                StartCoroutine(Utilities.DelayMethod(1, () => B.Play()));
            }
            else
            {
                StartCoroutine(Utilities.DelayMethod(1, () => StageNameChange(1)));
                StartCoroutine(Utilities.DelayMethod(1, () => A.volume = 0.5f));
                StartCoroutine(Utilities.DelayMethod(1, () => A.Play()));
            }
            ones = false;
            StartCoroutine(Utilities.DelayMethod(1, () => Fade = false));
        }
        if (Fade) //音を徐々に消す
        {
            A.volume -= 0.01f;
            B.volume -= 0.01f;
        }
        else //音を出す
        {

        }
    }

    void StageNameChange(int i)
    {
        switch(i)
        {
            case 1:
                Stagename.text = string.Format("地上");
                break;
            case 2:
                Stagename.text = string.Format("第一層 都市外壁");
                break;
        }
        for(float a = 0; a < 20; ++a)
        {
            StartCoroutine(Utilities.DelayMethod(a / 10, () => Stagename.color += new Color(0f, 0f, 0f, 0.05f)));
            StartCoroutine(Utilities.DelayMethod((a + 60) / 10, () => Stagename.color -= new Color(0f, 0f, 0f, 0.05f)));
        }
    }
}
