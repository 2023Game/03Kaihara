using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class Utilities
{
    public static IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
public class PlayerMoob : MonoBehaviour
{
    public static bool moob = true; //動けるかどうか
    static public float PHp = 100; //現在HP
    static public float MaxPHp = 100; //最大HP
    static public float EXP; //所持経験値
    int PDf = 0; //防御力
    float[] PRe = new float[5] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //属性耐性
    int PHe = 0; //再生力
    int a = 0;
    int DIn = 120; //ダメージを受ける間隔
    public static int HaveCoin = 100; //所持コイン数
    int DashInterval = 60; //ダッシュのクールタイム
    float damage; //ダメージ
    float spead = 0.6f; //移動速度
    new Transform transform;
    Rigidbody2D rigid;
    AudioSource audioSource;
    public AudioClip Dash;
    public AudioClip DamageSE;
    public AudioClip KeikokuSE;
    public Transform Player; // 動かすオブジェクトのトランスフォーム
    public Transform Aim; // ターゲットのオブジェクトのトランスフォーム
    public Text PlayerText;
    bool Isground = false; //地に足を付いているか
    bool Jamp = false; //ジャンプ中か
    void Start()
    {
        //フレームレートを120にする
        Application.targetFrameRate = 120;
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 向きたい方向を計算
        Vector2 dir = (Aim.position - Player.position);
        // 向きたい方向に回転
        Player.rotation = Quaternion.FromToRotation(Vector2.right, dir);
        if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity += new Vector2(spead, 0f);  //→
            if (Input.GetMouseButton(1) && DashInterval <= 0)//左クリックでダッシュ
            {
                rigid.velocity += new Vector2(6f, 0f);  //→ダッシュ
                DashInterval = 120;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity += new Vector2(-spead, 0f); //←

        }
        if (Input.GetMouseButton(1) && DashInterval <= 0)//左クリックでダッシュ
        {
            audioSource.PlayOneShot(Dash);
            spead *= 3;
            StartCoroutine(Utilities.DelayMethod(0.3f, () => spead /= 3));
            DashInterval = 120;
        }
        rigid.velocity *= new Vector2(0.9f, 1); //徐々に減速 

        DashInterval--;

        if (Input.GetKey(KeyCode.Space) && Isground && !Jamp)//地面を踏んでいる状態でスペースを押すとジャンプ
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 8f);
            a = 0;
            Jamp = true;
        }
        else if (Input.GetKey(KeyCode.Space) && a < 20)//長押しで長ジャンプ
        {
            a++;
        }
        else if(a == 60 || Jamp)//一定以上長ジャンプするかスペースを離すとジャンプを止める
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 6f);
            a = 61;
            Jamp = false;
        }
        if(Jamp)
        {
            rigid.gravityScale = 0.2f;
        }
        else
        {
            rigid.gravityScale = 2f;
        }
    }
    public void Damage(int[] EAt)
    {
        //敵の攻撃力-プレイヤーの防御力×属性耐性
        damage = EAt[0] - PDf * PRe[EAt[1]];
        if (damage <= 0) damage = 1; //0ダメージ以下なら1ダメージにする
        PHp -= damage;
        audioSource.PlayOneShot(DamageSE);
        gameObject.GetComponent<Renderer>().material.color *= new Color(0.5f, 0.5f, 0.5f, 1f);//無敵時間中色を暗くする
        StartCoroutine(Utilities.DelayMethod(1, () => gameObject.GetComponent<Renderer>().material.color *= new Color(2f, 2f, 2f, 1f)));
        gameObject.layer = 1; //敵と衝突しないレイヤーに切り替える
        StartCoroutine(Utilities.DelayMethod(1, () => gameObject.layer = 0));
        //HPが残り少ない時警告音を鳴らす
        if (PHp <= MaxPHp / 5)
        {
            audioSource.PlayOneShot(KeikokuSE);
        }
    }
    public void CoinGet(int Coin)
    {
        //コインを拾う
        HaveCoin += Coin;
        audioSource.Play();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //地面を踏んでいるか
        if (collision.gameObject.tag == "Grand")
        {
            Isground = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //地面から離れたか
        if (collision.gameObject.tag == "Grand")
        {
            Isground = false;
        }
    }
}
