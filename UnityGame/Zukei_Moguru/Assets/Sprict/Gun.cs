using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float[,] BSt;
    public string[] BNa; //銃の名前
    public float[] BAt; //威力,属性,ノックバック
    public static float[] BRe; //発射間隔,装填数,リロード速度
    public float BSp; //弾速
    public float BWa; //発射方式
    public float BNu; //発射数
    public float BPr; //精度
    public float BRa; //射程
    float[] BBa = new float[3]; //弾丸の種類,発射音,切り替え音
    public float BKi; //弾丸の軌道
    public static float[,] Zandan = new float[2, 4];
    public GameObject[] Bullet;
    public AudioClip[] ShotSESet;
    public AudioClip[] KirikaeSESet;
    Vector3 pos;
    Quaternion rot;
    float wh = 0;
    public static int whs = 4000000;
    int FireInterval = 0;
    bool Reload = true;
    GameObject BulletPrefab;
    AudioClip ShotSE;
    AudioClip KirikaeSE;
    new Transform transform;
    AudioSource audioSource;
    GameObject player;
    public static float ReloadInterval = 0;
    GunDeta deta;
    void Start()
    {
        deta = GetComponent<GunDeta>();
        transform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        (BSt, BNa) = deta.BulletDeta();
        //装填数を初期化
        if (GunDeta.Buki[0] != -1) Zandan[0, 0] = BSt[GunDeta.Buki[0], 4];
        if (GunDeta.Buki[1] != -1) Zandan[0, 1] = BSt[GunDeta.Buki[1], 4];
        if (GunDeta.Buki[2] != -1) Zandan[0, 2] = BSt[GunDeta.Buki[2], 4];
        if (GunDeta.Buki[3] != -1) Zandan[0, 3] = BSt[GunDeta.Buki[3], 4];
        int i = 0;
        /*
        while(GunDeta.Buki[i] == -1)
        {
            whs++;
            i++;
        }
        */
        (BSt, BNa) = deta.BulletDeta();

    }

    // Update is called once per frame
    void Update()
    {
        Kirikae(); //銃の切り替え処理
        FireInterval++;
        //弾が残っていて動けるなら左クリック中に定期的に弾を発射
        if (Input.GetMouseButton(0) && FireInterval >= 120 / BRe[0] && PlayerMoob.moob && Zandan[0, whs % 4] > 0)
        {
            Fire();
            FireInterval = 0;//発射間隔をリセット
        }
        else if ((Zandan[0, whs % 4] <= 0 || Input.GetKey(KeyCode.R)) && Reload) //残弾が0以下になるかRを押すとリロードする
        {
            ReloadInterval = BRe[2] * 120;
            audioSource.PlayOneShot(KirikaeSE);
            StartCoroutine(Utilities.DelayMethod(BRe[2], () => Zandan[0, whs % 4] = Zandan[1, whs % 4]));
            StartCoroutine(Utilities.DelayMethod(BRe[2], () => Reload = true));
            Reload = false;
        }
    }
    void Fire()
    {
        Zandan[0, whs % 4]--; //弾を減らす
        switch (BWa)
        {
            //単発銃
            case 0:
                Dispersion(BulletPrefab);
                audioSource.PlayOneShot(ShotSE);
                transform.localRotation = new(0, 0, 0, 0);
                break;
            //散弾銃
            case 1:
                for (int a = 0; BNu > a; ++a)
                {
                    StartCoroutine(Utilities.DelayMethod(a / 60, () => Dispersion(BulletPrefab)));
                }
                audioSource.PlayOneShot(ShotSE);
                transform.localRotation = new(0, 0, 0, 0);
                break;
            //バースト銃
            case 2:
                for (float a = 0; BNu > a; ++a)
                {
                    StartCoroutine(Utilities.DelayMethod(BRe[0] / BNu / 2 / 60 * a, () => pos = transform.position));
                    StartCoroutine(Utilities.DelayMethod(BRe[0] / BNu / 2 / 60 * a, () => Dispersion(BulletPrefab)));
                    StartCoroutine(Utilities.DelayMethod(BRe[0] / BNu / 2 / 60 * a, () => audioSource.PlayOneShot(ShotSE)));
                    transform.localRotation = new(0, 0, 0, 0);
                }
                break;
            case 3:
                for (float a = 0; BNu > a; ++a)
                {
                    for (int b = 0; BNu > b; ++b)
                    {
                        StartCoroutine(Utilities.DelayMethod(a / (BNu * 3), () => pos = transform.position));
                        StartCoroutine(Utilities.DelayMethod(a / (BNu * 3), () => Dispersion(BulletPrefab)));
                        StartCoroutine(Utilities.DelayMethod(a / (BNu * 3), () => audioSource.PlayOneShot(ShotSE)));
                    }
                    transform.localRotation = new(0, 0, 0, 0);
                }
                break;
        }
    }
    void Dispersion(GameObject BulletPrefab) //精度の数値だけ角度をランダムに変えて発射
    {
        float R = BPr / 360; //360で割って角度に変換
        float index = UnityEngine.Random.Range(R, -R); //精度分の振れ幅のランダムな数値を出す
        transform.localRotation = new(transform.localRotation.z, transform.localRotation.y, transform.localRotation.z + index, transform.localRotation.w);//銃口をブラす
        pos = transform.position;
        rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        Instantiate(BulletPrefab, pos, rot); //発射
    }

    void Kirikae()
    {
        wh = Input.GetAxis("Mouse ScrollWheel") * 10;//マウスホイールの回転を検知
        //武器を切り替えた時切り替え音を鳴らす
        if (wh != 0)
        {
            audioSource.PlayOneShot(KirikaeSE);
            if (wh < 0)
            {
                do { whs--; }
                while (GunDeta.Buki[whs % 4] == -1);
            }
            else
            {
                do { whs++; }
                while (GunDeta.Buki[whs % 4] == -1);
            }
            while (whs < 0) { whs += 4; };
        }
        //弾丸のステータスを代入する
        BAt = new float[] { BSt[GunDeta.Buki[whs % 4], 0], BSt[GunDeta.Buki[whs % 4], 1], BSt[GunDeta.Buki[whs % 4], 2] };　 //威力,属性,ノックバック
        BRe = new float[] { BSt[GunDeta.Buki[whs % 4], 3], BSt[GunDeta.Buki[whs % 4], 4], BSt[GunDeta.Buki[whs % 4], 5] };  //発射間隔,装填数,リロード速度
        BSp = BSt[GunDeta.Buki[whs % 4], 6];   //弾速
        BWa = BSt[GunDeta.Buki[whs % 4], 7];   //発射方式
        BNu = BSt[GunDeta.Buki[whs % 4], 8];   //発射数
        BPr = BSt[GunDeta.Buki[whs % 4], 9];   //精度
        BRa = BSt[GunDeta.Buki[whs % 4], 10];  //射程
        BBa = new float[] { BSt[GunDeta.Buki[whs % 4], 11], BSt[GunDeta.Buki[whs % 4], 12], BSt[GunDeta.Buki[whs % 4], 13] };//弾丸の種類,切り替え音
        BKi = BSt[GunDeta.Buki[whs % 4], 14];  //弾丸の軌道
        Zandan[1, whs % 4] = BRe[1]; //最大装填数を代入
        //BBaの数値を元に弾丸の種類,発射音,切り替え音を挿入
        BulletPrefab = Bullet[(int)BBa[0]];
        ShotSE = ShotSESet[(int)BBa[1]];
        KirikaeSE = KirikaeSESet[(int)BBa[2]];
    }
}