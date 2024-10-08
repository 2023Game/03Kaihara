using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoob : MonoBehaviour
{
    public bool Boss;
    public string Name;
    public string ENa; //���O
    public float EHp = 1; //�̗�
    public int[] EAt = new int[2]; //�U����
    public int EDf; //�h���
    public float[] EArDf = new float[5]; //�����ϐ�[������,��,�X.��,��]
    public int ESp; //�ړ����x
    public int EMo; //������
    int[] ESc;  //�X�N���b�v�h���b�v�� [1point , 10point ,100point]
    int[] EDr;�@//���Ƃ�����̎�ނƊm��
    public int EEx; //�o���l
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
        //HP��0�ɂȂ�Ə�����
        if (EHp <= 0)
        {
            //EnemySummon.enemynum--;
            Live = false;
            rigid.velocity *= 0f;
            gameObject.GetComponent<Renderer>().material.color *= new Color(0.7f, 0.7f, 0.7f, 0.2f);
            if (ones)
            {
                if (Boss) BossHPUI.SetActive(false);
                //�{�X�Ȃ�10��A�U�R�Ȃ�1��X�N���b�v�𗎂Ƃ��AEXP�𑝂₵�A�p�[�e�B�N�����c���B
                for (int i = 0; i < 10; i++)
                {
                    int indexA = UnityEngine.Random.Range(1, 100);
                    int indexB = UnityEngine.Random.Range(1, 100);
                    int indexC = UnityEngine.Random.Range(1, 100);
                    int indexD = UnityEngine.Random.Range(1, 100);
                    if (indexA <= ESc[0])
                    {
                        Instantiate(Scrap[0], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //�~�j�X�N���b�v����
                    }
                    if (indexB <= ESc[1])
                    {
                        Instantiate(Scrap[1], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //�X�N���b�v����
                    }
                    if (indexC <= ESc[2])
                    {
                        Instantiate(Scrap[2], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //�r�b�N�X�N���b�v����
                    }
                    if (indexD <= EDr[1] && !GunDeta.BKaihou[EDr[0]])
                    {
                        Instantiate(DropItem, transform.position, new Quaternion(0f, 0f, 0f, 0f));//�h���b�v�A�C�e���𗎂Ƃ�
                    }
                    if (!Boss) i = 10;
                }
                Instantiate(EnemyDeath, transform.position, transform.rotation);
                PlayerMoob.EXP += EEx;
                Destroy(gameObject);
            }
        }
        //�v���C���[�Ƃ̋������v�Z
        float dis = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        //�ړ�����
        if (Live)
        {
            //���������������v�Z
            Vector3 dir = new(transform.position.x - GameObject.Find("Player").transform.position.x, 0f, transform.position.z - GameObject.Find("Player").transform.position.z);
            //������
            switch (EMo)
            {
                //�����Ȃ�
                case 0:
                    break;
                //�G�̕����֓���������
                case 1:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector3.right, dir);
                    transform.Translate(Vector3.right * Time.deltaTime * ESp);
                    break;
                //1�b���ɓG�֌����Ē��i
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
                //�Œ�C��
                case 3:
                    if (i < 60) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //����
                        i = 0;
                    }
                    break;
                //��苗����ۂ��čU��
                case 4:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector3.up, dir);
                    if (dis > 12) transform.Translate(Vector3.up * Time.deltaTime * ESp);
                    else transform.Translate(Vector3.up * Time.deltaTime * -ESp);
                    if (i < 60) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //����
                        i = 0;
                    }
                    break;
                //1�{�X
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
                //�ڂ��j
                case 11:
                    i++;
                    if (i == 720 || i == 360)
                        Instantiate(EBullet, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), transform.rotation); //����
                    if (i == 720) i = 0;
                    break;
                //�ڂ��C��
                case 12:
                    i++;
                    if (i <= 480 && (i % 180 == 121 || i % 180 == 131 || i % 180 == 141))
                        for (int q = 0; q < 3; q++)
                        {
                            transform.localRotation = new(transform.localRotation.z, transform.localRotation.y + (0.10f - (q * 0.10f)), transform.localRotation.z, transform.localRotation.w);//�e�����u����
                            Vector3 pos = transform.position;
                            Quaternion rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                            Instantiate(EBullet, pos, rot); //����
                            transform.localRotation = new(0, 0, 0, 0);
                        }
                    else if (i > 480 && i % 60 == 0 && i < 640)
                    {
                        for (int q = 0; q < 4; q++)
                        {
                            transform.localRotation = new(transform.localRotation.z, transform.localRotation.y + (0.375f - (q * 0.25f)), transform.localRotation.z, transform.localRotation.w);//�e�����u����
                            Vector3 pos = transform.position;
                            Quaternion rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                            Instantiate(EBullet, pos, rot); //����
                            transform.localRotation = new(0, 0, 0, 0);
                        }
                    }
                    else if (i > 480 && i % 60 == 30 && i < 640)
                    {
                        for (int q = 0; q < 3; q++)
                        {
                            transform.localRotation = new(transform.localRotation.z, transform.localRotation.y + (0.25f - (q * 0.25f)), transform.localRotation.z, transform.localRotation.w);//�e�����u����
                            Vector3 pos = transform.position;
                            Quaternion rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                            Instantiate(EBullet, pos, rot); //����
                            transform.localRotation = new(0, 0, 0, 0);
                        }
                    }
                    if (i >= 720) i = 0;
                    break;
                //�e��
                case -1:
                    transform.Translate(Vector3.up * Time.deltaTime * ESp);
                    i++;
                    if (i >= 120) Destroy(gameObject);
                    break;
                //�e��
                case -2:
                    transform.Translate(Vector3.right * Time.deltaTime * ESp);
                    i++;
                    if (i >= 120) Destroy(gameObject);
                    break;
            }
        }
        //�v���C���[������ȏ㗣��ď����o�߂���Ə���
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
            //�v���C���[�Ƃ̍����̋������v�Z
            float dis = GameObject.Find("Player").transform.position.y - transform.position.y;
            //�v���C���[�ɂԂ���Ƒ����HP�����炷
            if (collision.gameObject.tag == "Player" && AtInterval >= 60)
            {
                collision.gameObject.GetComponent<PlayerMoob>().Damage(EAt);
                AtInterval = 0;
            }
            //�v���C���[��������荂���ʒu�ɂ��鎞�W�����v
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
        //���g���e�ۂ̏ꍇ�v���C���[�ɓ�����ƃ_���[�W��^���ď��ł���
        if (other.gameObject.tag == "Player" && gameObject.tag == "EBullet")
        {
            other.gameObject.GetComponent<PlayerMoob>().Damage(EAt);
            Destroy(gameObject);
        }
        //�ǂɓ�����Ə���
        if (other.gameObject.tag == "Tail" && gameObject.tag == "EBullet")
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float[] BAt)
    {
        //�e�e�̈З�-�G�̖h���
        damage = BAt[0] - EDf;
        //�_���[�W�ɑ����ϐ����|����
        damage *= EArDf[(int)BAt[1]];
        //�_���[�W��0�ȉ��ɂȂ�����1�_���[�W�ɂȂ�
        if (damage <= 0) damage = 1;
        //�G��HP���Z�o�_���[�W�������炷
        EHp -= damage;
        if (Boss) //�������{�X�Ȃ�HPUI��\��
        {
            BossHPUI.SetActive(true);
            BossHPUI.GetComponent<Image>().fillAmount = (MaxEHp * 0.07f + EHp) / MaxEHp;
        }
    }
    void statusSet()
    {
        Name = this.name;
        //���O����(Clone)�����O
        Name = Name.Replace("(Clone)", "");
        switch (Name)
        {
            case "�e�X�g":
                Boss = false;
                ENa = ""; //���O
                EHp = 100000; //�̗�
                EAt = new int[] { 10, 0 }; //�U����
                EDf = 0; //�h���
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 0; //�ړ����x
                EMo = 0; //�s��AI
                ESc = new int[] { 0, 0, 0 }; //���Ƃ��X�N���b�v
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 0;
                break;
        }
    }
}