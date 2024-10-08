using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoob : MonoBehaviour
{
    new Transform transform;
    Rigidbody2D rigib;
    private int time = 0;
    public GameObject BulletParticle;
    public GameObject Exprosion;
    float[] BAt; //威力,属性,ノックバック
    float BSp; //弾丸速度
    float BRa; //弾丸射程
    float Bkidou; //弾丸の軌道
    Gun gun;
    void Start()
    {
        if (Exprosion != null) Exprosion.SetActive(false);
        transform = GetComponent<Transform>();
        rigib = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Gun");
        gun = player.GetComponent<Gun>();
        //弾丸のステータスをGunから取得
        BAt = new float[] { gun.BAt[0], gun.BAt[1], gun.BAt[2] };
        BSp = gun.BSp; //弾丸速度
        BRa = gun.BRa; //射程
        Bkidou = gun.BKi; //弾丸の軌道
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;
        switch (Bkidou)  //直進
        {
            case 0: //直進
            case 2: //着弾で爆発
                rigib.velocity = transform.right * BSp;
                break;
            case 1: //徐々に加速
                rigib.velocity += new Vector2(0, BSp / 30);
                break;
        }
        //射程の分時間が経つと消す
        if (time == BRa)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //地形タイルに当たると消滅
        if (other.gameObject.tag == "Tail")
        {
            if (Bkidou == 2)
            {
                BSp = 0;
                Exprosion.SetActive(true);
                StartCoroutine(Utilities.DelayMethod(1, () => Destroy(gameObject)));
            }
            else
            {
                Destroy(gameObject);
            }
        }
        //敵に当たるとダメージを与えて消滅する
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(BulletParticle, transform.position, transform.rotation); //パーティクル
            if (Bkidou == 2)
            {
                BSp = 0;
                Exprosion.SetActive(true);
                StartCoroutine(Utilities.DelayMethod(1, () => Destroy(gameObject)));
            }
            else
            {
                other.gameObject.GetComponent<EnemyMoob>().Damage(BAt);
                Destroy(gameObject);
            }
        }
    }
}