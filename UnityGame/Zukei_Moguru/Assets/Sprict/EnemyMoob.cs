using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoob : MonoBehaviour
{
    public string Name;
    public string ENa; //名前
    public float EHp = 1; //体力
    public int[] EAt = new int[3]; //攻撃力
    public int EDf; //防御力
    public float[] EArDf = new float[5]; //属性耐性[無属性,炎,氷.雷,魔]
    public float ESp; //移動速度
    public int EMo; //動き方
    int[] ECo;  //コインドロップ率 [1point , 5point , 10point , 50point , 100point , 500point]
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
        rigid.velocity *= new Vector2(0.95f ,1f); //左右減速
        //HPが0になると消える
        if (EHp <= 0)
        {
            //EnemySummon.enemynum--;
            Live = false;
            rigid.velocity *= 0f;
            gameObject.GetComponent<Renderer>().material.color *= new Color(0.7f, 0.7f, 0.7f, 0.2f);
            if (ones)
            {
                //コインを落とし、EXPを増やし、パーティクルを残す。
                    int indexA = UnityEngine.Random.Range(1, 100);
                    int indexB = UnityEngine.Random.Range(1, 100);
                    if (indexA <= ECo[0])
                    {
                        Instantiate(Scrap[0], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //1Coin召喚
                    }
                    else if (indexA <= ECo[1] + ECo[0])
                    {
                        Instantiate(Scrap[1], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //5Coin召喚
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2])
                    {
                        Instantiate(Scrap[2], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //10Coin召喚
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2] + ECo[3])
                    {
                        Instantiate(Scrap[3], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //50Coin召喚
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2] + ECo[3] + ECo[4])
                    {
                        Instantiate(Scrap[4], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //100Coin召喚
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2] + ECo[3] + ECo[4] + ECo[5])
                    { 
                        Instantiate(Scrap[5], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //500Coin召喚
                    }
                    if (indexB <= EDr[1] && !GunDeta.BKaihou[EDr[0]])
                    {
                        Instantiate(DropItem, transform.position, new Quaternion(0f, 0f, 0f, 0f));//ドロップアイテムを落とす
                    }
                Instantiate(EnemyDeath, transform.position, transform.rotation);//パーティクル召喚
                if (!name.Contains("ちび")) EnemySummon.enemynum--;//自分が子機で無いなら敵のカウントを減らす
                PlayerMoob.EXP += EEx;//プレイヤーに経験値を与える
                Destroy(gameObject);
            }
        }
        //プレイヤーとの距離を計算
        float dis = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position);
        //移動処理
        if (Live)
        {
            //プレイヤーの方向を計算
            Vector2 dir = new(transform.position.x - GameObject.Find("Player").transform.position.x, transform.position.y - GameObject.Find("Player").transform.position.y);
            //動き方
            switch (EMo)
            {
                //動かない
                case 0:
                    break;
                //敵の方向へ動き続ける(地上)
                case 1:
                    if (dir.x < 0)
                    {
                        rigid.velocity += new Vector2(ESp / 10, 0f);  //→
                    }
                    else if (dir.x > 0)
                    {
                        rigid.velocity += new Vector2(-ESp / 10, 0f);  //→
                    }
                    break;
                //敵の方向へ動き続ける(空中)
                case 2:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector2.up, dir);
                    rigid.velocity += new Vector2(transform.up.x * ESp / 20, transform.up.y * ESp / 20);
                    break;
                    //直進(弾丸用)
                case 2_1:
                    rigid.velocity = new Vector2(transform.up.x * ESp, transform.up.y * ESp);
                    break;
                //1秒毎に敵へ向けて直進
                case 3:
                    if (i < 120 && Live)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * ESp);
                        i++;
                        if (EHp <= 0)
                        {
                            i = 0;
                            Live = false;
                        }
                    }
                    else
                    {
                        this.transform.rotation = Quaternion.FromToRotation(-Vector2.right, dir);
                        i = 0;
                    }
                    break;
                //固定砲台
                case 4:
                    if (i < EAt[2] * 120) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //発射
                        i = 0;
                    }
                    break;
                //一定距離を保って攻撃
                case 5:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector2.up, dir);
                    if (dis > 5) transform.Translate(Vector2.up * Time.deltaTime * ESp);
                    else transform.Translate(Vector2.up * Time.deltaTime * -ESp);
                    if (i < EAt[2] * 120) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //発射
                        i = 0;
                    }
                    break;
            }
        }
        //プレイヤーから一定以上離れて少し経過すると消滅
        if (dis > 25)
        {
            j++;
            if (j <= 240)
            {
                //敵の場合敵のカウンターを減らす
                if (gameObject.tag == "Enemy" && !name.Contains("ちび")) EnemySummon.enemynum--;
                Destroy(gameObject);
            }
            else j = 0;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (Live)
        {
            //プレイヤーとの高さの距離を計算
            float dis = GameObject.Find("Player").transform.position.y - transform.position.y;
            //プレイヤーにぶつかると相手のHPを減らす
            if (collision.gameObject.tag == "Player" && AtInterval >= 120)
            {
                Debug.Log("A");
                collision.gameObject.GetComponent<PlayerMoob>().Damage(EAt);
                AtInterval = 0;
            }
            //プレイヤーが自分より高い位置にいる時ジャンプ
            if (collision.gameObject.tag == "Grand" && rigid.velocity.magnitude <= 10 && dis > 0.4f && janpinterval <= 0 && !(EMo == 0 || EMo == 2 || EMo == 2_1 ||EMo == 4))
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 10f);
                StartCoroutine(Utilities.DelayMethod(0.2f, () => rigid.velocity = Vector2.up * 4));
                janpinterval = 60;
            }
            else
            {
                janpinterval--;
                if (janpinterval < 0) janpinterval = 120;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
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
    }
    void statusSet()
    {
        Name = this.name;
        //名前から(Clone)を除外
        Name = Name.Replace("(Clone)", "");
        switch (Name)
        {
            case "テスト":
                ENa = ""; //名前
                EHp = 100000; //体力
                EAt = new int[] { 10, 0 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 0; //防御力
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 0; //移動速度
                EMo = 0; //行動AI
                ECo = new int[] { 0, 0, 0, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 0;
                break;
            case "まる":
                ENa = "まる"; //名前
                EHp = 120; //体力
                EAt = new int[] { 10, 0 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 1; //防御力
                EArDf = new float[] { 1.0f, 1.5f, 1.0f, 1.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 2; //移動速度
                EMo = 1; //行動AI
                ECo = new int[] { 20, 0, 0, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 10;
                break;
            case "さんかく":
                ENa = "さんかく";//名前
                EHp = 80; //体力
                EAt = new int[] { 18, 0 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 1; //防御力
                EArDf = new float[] { 1.0f, 1.0f, 2.0f, 1.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 4; //移動速度
                EMo = 2; //行動AI
                ECo = new int[] { 30, 10, 0 , 0, 0, 0}; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 6, 10 };//落とす武器
                EEx = 20;
                break;
            case "しかく":
                ENa = "しかく";//名前
                EHp = 100; //体力
                EAt = new int[] { 10, 3 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 5; //防御力
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 0.5f, 1.5f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 1; //移動速度
                EMo = 1; //行動AI
                ECo = new int[] { 50, 50, 0, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 30;
                break;
            case "しかくバリア":
                ENa = "しかくバリア";//名前
                EHp = 150; //体力
                EAt = new int[] { 10, 3 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 10; //防御力
                EArDf = new float[] { 1.0f, 1.0f, 2.0f, 0.0f, 2.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 1; //移動速度
                EMo = 0; //行動AI
                ECo = new int[] { 0, 0, 0, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 0;
                break;
            case "ろっかく":
                ENa = "ろっかく";//名前
                EHp = 250; //体力
                EAt = new int[] { 5, 0 ,3 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 0; //防御力
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 2.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 1; //移動速度
                EMo = 4; //行動AI
                ECo = new int[] { 50, 40, 10, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 25;
                break;
            case "ちびろっかく":
                ENa = "ちびろっかく";//名前
                EHp = 10; //体力
                EAt = new int[] { 12, 0 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 0; //防御力
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 2.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 2.5f; //移動速度
                EMo = 1; //行動AI
                ECo = new int[] { 10, 00, 0, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 2;
                break;
            case "やじるし":
                ENa = "やじるし";//名前
                EHp = 80; //体力
                EAt = new int[] { 12, 2, 1 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 0; //防御力
                EArDf = new float[] { 1.0f, 2.0f, 1.0f, 2.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 2f; //移動速度
                EMo = 5; //行動AI
                ECo = new int[] { 80, 20, 0, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 20;
                break;
            case "ちびやじるし":
                ENa = "ちびやじるし";//名前
                EHp = 1; //体力
                EAt = new int[] { 12, 2 }; //攻撃力[威力,属性.攻撃速度(弾丸を飛ばす敵のみ)]
                EDf = 0; //防御力
                EArDf = new float[] { 1.0f, 2.0f, 1.0f, 2.0f, 1.0f }; //属性耐性[無属性,炎,氷.雷,魔]
                ESp = 2.5f; //移動速度
                EMo = 2_1; //行動AI
                ECo = new int[] { 80, 20, 0, 0, 0, 0 }; //落とすコイン[1,5,10,50,100,500]を落とす確率
                EDr = new int[] { 0, 0 };//落とす武器
                EEx = 20;
                break;
        }
    }
}