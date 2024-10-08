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
    float[] BAt; //�З�,����,�m�b�N�o�b�N
    float BSp; //�e�ۑ��x
    float BRa; //�e�ێ˒�
    float Bkidou; //�e�ۂ̋O��
    Gun gun;
    void Start()
    {
        if (Exprosion != null) Exprosion.SetActive(false);
        transform = GetComponent<Transform>();
        rigib = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Gun");
        gun = player.GetComponent<Gun>();
        //�e�ۂ̃X�e�[�^�X��Gun����擾
        BAt = new float[] { gun.BAt[0], gun.BAt[1], gun.BAt[2] };
        BSp = gun.BSp; //�e�ۑ��x
        BRa = gun.BRa; //�˒�
        Bkidou = gun.BKi; //�e�ۂ̋O��
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;
        switch (Bkidou)  //���i
        {
            case 0: //���i
            case 2: //���e�Ŕ���
                rigib.velocity = transform.right * BSp;
                break;
            case 1: //���X�ɉ���
                rigib.velocity += new Vector2(0, BSp / 30);
                break;
        }
        //�˒��̕����Ԃ��o�Ə���
        if (time == BRa)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //�n�`�^�C���ɓ�����Ə���
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
        //�G�ɓ�����ƃ_���[�W��^���ď��ł���
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(BulletParticle, transform.position, transform.rotation); //�p�[�e�B�N��
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