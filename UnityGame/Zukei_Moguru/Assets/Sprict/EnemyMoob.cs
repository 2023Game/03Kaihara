using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoob : MonoBehaviour
{
    public bool Boss;
    public string Name;
    public string ENa; //名前
    public float EHp = 1; //体力
    public int[] EAt = new int[2]; //攻撃力
    public int EDf; //防御力
    public float[] EArDf = new float[5]; //属性耐性[無属性,炎,氷.雷,魔]
    public int ESp; //移動速度
    public int EMo; //動き方
    int[] ESc;  //スクラップドロップ率 [1point , 10point ,100point]
    int[] EDr;　//落とす武器の種類と確率
    public int EEx; //経験値
    bool ones = true;
    float MaxEHp;
    int i = 0;
    int j = 0;
    int janpinterval = 20;

    new Transform transform;
    Rigidbody2D rigid;
    public float damage;
    public int AtInterval = 0;
    bool Live = true;
    public GameObject EBullet;
    public GameObject[] Scrap;
    public GameObject DropItem;
    AudioSource audioSource;
    public AudioClip Attack;
    public GameObject BossHPUI;
    public GameObject EnemyDeath;

    void Start()
    {
        transform = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        GameObject.Find("Player");
        statusSet();
        BossHPUI = GameObject.Find("BossHPUI");
    }

    void FixedUpdate()
    {
        AtInterval++;
        rigid.velocity *= 0.95f;
        //HPが0になると消える
        if (EHp <= 0)
        {
            //EnemySummon.enemynum--;
            Live = false;
            rigid.velocity *= 0f;
            gameObject.GetComponent<Renderer>().material.color *= new Color(0.7f, 0.7f, 0.7f, 0.2f);
            if (ones)
            {
                if (Boss) BossHPUI.SetActive(false);
                //ボスなら10回、ザコなら1回スクラップを落とし、EXPを増やし、パーティクルを残す。
                for (int i = 0; i < 10; i++)
                {
                    int indexA = UnityEngine.Random.Range(1, 100);
                    int indexB = UnityEngine.Random.Range(1, 100);
                    int indexC = UnityEngine.Random.Range(1, 100);
                    int indexD = UnityEngine.Random.Range(1, 100);
                    if (indexA <= ESc[0])
                    {
                        Instantiate(Scrap[0], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //ミニスクラップ召喚
                    }
                    if (indexB <= ESc[1])
                    {
                        Instantiate(Scrap[1], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //スクラップ召喚
                    }
                    if (indexC <= ESc[2])
                    {
                        Instantiate(Scrap[2], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //ビックスクラップ召喚
                    }
                    if (indexD <= EDr[1] && !GunDeta.BKaihou[EDr[0]])
                    {
                        Instantiate(DropItem, transform.position, new Quaternion(0f, 0f, 0f, 0f));//ドロップアイテムを落とす
                    }
                    if (!Boss) i = 10;
                }
                Instantiate(EnemyDeath, transform.position, transform.rotation);
                PlayerMoob.EXP += EEx;
                Destroy(gameObject);
            }
        }
        //プレイヤーとの距離を計算
        float dis = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        //移動処理
        if (Live)
        {
            //向きたい方向を計算
            Vector3 dir = new(transform.position.x - GameObject.Find("Player").transform.position.x, 0f, transform.position.z - GameObject.Find("Player").transform.position.z);
            //動き方
            switch (EMo)
            {
                //動かない
                case 0:
                    break;
                //敵の方向へ動き続ける
                case 1:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector3.right, dir);
                    transform.Translate(Vector3.right * Time.deltaTime * ESp);
                    break;
                //1秒毎に敵へ向けて直進
                case 2:
                    if (i < 60 && Live)
                    {
                        transform.Translate(Vector3.right * Time.deltaTime * ESp);
                        i++;
                        if (EHp <= 0)
                        {
                            i = 0;
                            Live = false;
                        }
                    }
                    else
                    {
                        this.transform.rotation = Quaternion.FromToRotation(-Vector3.right, dir);
                        i = 0;
                    }
                    break;
                //固定砲台
                case 3:
                    if (i < 60) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //発射
                        i = 0;
                    }
                    break;
                //一定距離を保って攻撃
                case 4:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector3.up, dir);
                    if (dis > 12) transform.Translate(Vector3.up * Time.deltaTime * ESp);
                    else transform.Translate(Vector3.up * Time.deltaTime * -ESp);
                    if (i < 60) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //発射
                        i = 0;
                    }
                    break;
                //1ボス
                case 10:
                    i++;
                    if (Live && (i < 120 || (i > 180 && i < 300) || (i > 360 && i < 480)))
                    {
                        transform.Translate(Vector3.right * Time.deltaTime * ESp);
                        if (EHp <= 0)
                        {
                            i = 0;
                            Live = false;
                        }
                    }
                    else if (i <= 720)
                    {
                        this.transform.rotation = Quaternion.FromToRotation(-Vector3.right, dir);
                    }
                    else i = 0;
                    break;
                //ぼす針
                case 11:
                    i++;
                    if (i == 720 || i == 360)
                        Instantiate(EBullet, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), transform.rotation); //発射
                    if (i == 720) i = 0;
                    break;
                //ぼす砲台
                case 12:
                    i++;
                    if (i <= 480 && (i % 180 == 121 || i % 180 == 131 || i % 180 == 141))
                        for (int q = 0; q < 3; q++)
                        {
                            transform.localRotation = new(transform.localRotation.z, transform.localRotation.y + (0.10f - (q * 0.10f)), transform.localRotation.z, transform.localRotation.w);//銃口をブラす
                            Vector3 pos = transform.position;
                            Quaternion rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                            Instantiate(EBullet, pos, rot); //発射
                            transform.localRotation = new(0, 0, 0, 0);
                        }
                    else if (i > 480 && i % 60 == 0 && i < 640)
                    {
                        for (int q = 0; q < 4; q++)
                        {
                            transform.localRotation = new(transform.localRotation.z, transform.localRotation.y + (0.375f - (q * 0.25f)), transform.localRotation.z, transform.localRotation.w);//銃口をブラす
                            Vector3 pos = transform.position;
                            Quaternion rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                            Instantiate(EBullet, pos, rot); //発射
                            transform.localRotation = new(0, 0, 0, 0);
                        }
                    }
                    else if (i > 480 && i % 60 == 30 && i < 640)
                    {
                        for (int q = 0; q < 3; q++)
                        {
                            transform.localRotation = new(transform.localRotation.z, transform.localRotation.y + (0.25f - (q * 0.25f)), transform.localRotation.z, transform.localRotation.w);//銃口をブラす
                            Vector3 pos = transform.position;
                            Quaternion rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                            Instantiate(EBullet, pos, rot); //発射
                            transform.localRotation = new(0, 0, 0, 0);
                        }
                    }
                    if (i >= 720) i = 0;
                    break;
                //弾丸
                case -1:
                    transform.Translate(Vector3.up * Time.deltaTime * ESp);
                    i++;
                    if (i >= 120) Destroy(gameObject);
                    break;
                //弾丸
                case -2:
                    transform.Translate(Vector3.right * Time.deltaTime * ESp);
                    i++;
                    if (i >= 120) Destroy(gameObject);
                    break;
            }
        }
        //プレイヤーから一定以上離れて少し経過すると消滅
        if ((dis > 35 && !Boss) || dis > 50)
        {
            j++;
            if (j <= 240)
            {
                //EnemySummon.enemynum--;
                Destroy(gameObject);
            }
            else j = 0;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (Live)
        {
            //プレイヤーとの高さの距離を計算
            float dis = GameObject.Find("Player").transform.position.y - transform.position.y;
            //プレイヤーにぶつかると相手のHPを減らす
            if (collision.gameObject.tag == "Player" && AtInterval >= 60)
            {
                collision.gameObject.GetComponent<PlayerMoob>().Damage(EAt);
                AtInterval = 0;
            }
            //プレイヤーが自分より高い位置にいる時ジャンプ
            if (collision.gameObject.tag == "Grand" && rigid.velocity.magnitude <= 10 && dis > 0.4f && janpinterval <= 0 && !(EMo == 0))
            {
                rigid.velocity = new(0f, 24f);
                StartCoroutine(Utilities.DelayMethod(0.2f, () => rigid.velocity = Vector3.right * 4));
                janpinterval = 60;
            }
            else
            {
                janpinterval--;
                if (janpinterval < 0) janpinterval = 60;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //自身が弾丸の場合プレイヤーに当たるとダメージを与えて消滅する
        if (other.gameObject.tag == "Player" && gameObject.tag == "EBullet")
        {
            other.gameObject.GetComponent<PlayerMoob>().Damage(EAt);
            Destroy(gameObject);
        }
        //壁に当たると消滅
        if (other.gameObject.tag == "Tail" && gameObject.tag == "EBullet")
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float[] BAt)
    {
        //銃弾の威力-敵の防御力
        damage = BAt[0] - EDf;
        //ダメージに属性耐性を掛ける
        damage *= EArDf[(int)BAt[1]];
        //ダメージが0以下になったら1ダメージになる
        if (damage <= 0) damage = 1;
        //敵のHPを算出ダメージだけ減らす
        EHp -= damage;
        if (Boss) //自分がボスならHPUIを表示
        {
            BossHPUI.SetActive(true);
            BossHPUI.GetComponent<Image>().fillAmount = (MaxEHp * 0.07f + EHp) / MaxEHp;
        }
    }
    void statusSet()
    {
        Name = this.name;
        //名前から(Clone)を除外
        Name = Name.Replace("(Clone)", "");
        switch (Name)
        {
            case "テスト":
                Boss = false;
                ENa = ""; //名前
                EHp = 100000; //体力
                EAt = new int[] { 10, 0 }; //攻撃力
                EDf = 0; //防御力
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 0; //移動速度
                EMo = 0; //行動AI
                ESc = new int[] { 0, 0, 0 }; //落とすスクラップ
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 0;
                break;
        }
    }
}