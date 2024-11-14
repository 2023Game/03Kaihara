using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoob : MonoBehaviour
{
    public string Name;
    public string ENa; //���O
    public float EHp = 1; //�̗�
    public int[] EAt = new int[3]; //�U����
    public int EDf; //�h���
    public float[] EArDf = new float[5]; //�����ϐ�[������,��,�X.��,��]
    public float ESp; //�ړ����x
    public int EMo; //������
    int[] ECo;  //�R�C���h���b�v�� [1point , 5point , 10point , 50point , 100point , 500point]
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
        rigid.velocity *= new Vector2(0.95f ,1f); //���E����
        //HP��0�ɂȂ�Ə�����
        if (EHp <= 0)
        {
            //EnemySummon.enemynum--;
            Live = false;
            rigid.velocity *= 0f;
            gameObject.GetComponent<Renderer>().material.color *= new Color(0.7f, 0.7f, 0.7f, 0.2f);
            if (ones)
            {
                //�R�C���𗎂Ƃ��AEXP�𑝂₵�A�p�[�e�B�N�����c���B
                    int indexA = UnityEngine.Random.Range(1, 100);
                    int indexB = UnityEngine.Random.Range(1, 100);
                    if (indexA <= ECo[0])
                    {
                        Instantiate(Scrap[0], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //1Coin����
                    }
                    else if (indexA <= ECo[1] + ECo[0])
                    {
                        Instantiate(Scrap[1], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //5Coin����
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2])
                    {
                        Instantiate(Scrap[2], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //10Coin����
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2] + ECo[3])
                    {
                        Instantiate(Scrap[3], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //50Coin����
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2] + ECo[3] + ECo[4])
                    {
                        Instantiate(Scrap[4], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //100Coin����
                    }
                    else if (indexA <= ECo[1] + ECo[0] + ECo[2] + ECo[3] + ECo[4] + ECo[5])
                    { 
                        Instantiate(Scrap[5], transform.position, new Quaternion(0f, 0f, 0f, 0f)); //500Coin����
                    }
                    if (indexB <= EDr[1] && !GunDeta.BKaihou[EDr[0]])
                    {
                        Instantiate(DropItem, transform.position, new Quaternion(0f, 0f, 0f, 0f));//�h���b�v�A�C�e���𗎂Ƃ�
                    }
                Instantiate(EnemyDeath, transform.position, transform.rotation);//�p�[�e�B�N������
                if (!name.Contains("����")) EnemySummon.enemynum--;//�������q�@�Ŗ����Ȃ�G�̃J�E���g�����炷
                PlayerMoob.EXP += EEx;//�v���C���[�Ɍo���l��^����
                Destroy(gameObject);
            }
        }
        //�v���C���[�Ƃ̋������v�Z
        float dis = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position);
        //�ړ�����
        if (Live)
        {
            //�v���C���[�̕������v�Z
            Vector2 dir = new(transform.position.x - GameObject.Find("Player").transform.position.x, transform.position.y - GameObject.Find("Player").transform.position.y);
            //������
            switch (EMo)
            {
                //�����Ȃ�
                case 0:
                    break;
                //�G�̕����֓���������(�n��)
                case 1:
                    if (dir.x < 0)
                    {
                        rigid.velocity += new Vector2(ESp / 10, 0f);  //��
                    }
                    else if (dir.x > 0)
                    {
                        rigid.velocity += new Vector2(-ESp / 10, 0f);  //��
                    }
                    break;
                //�G�̕����֓���������(��)
                case 2:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector2.up, dir);
                    rigid.velocity += new Vector2(transform.up.x * ESp / 20, transform.up.y * ESp / 20);
                    break;
                    //���i(�e�ۗp)
                case 2_1:
                    rigid.velocity = new Vector2(transform.up.x * ESp, transform.up.y * ESp);
                    break;
                //1�b���ɓG�֌����Ē��i
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
                //�Œ�C��
                case 4:
                    if (i < EAt[2] * 120) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //����
                        i = 0;
                    }
                    break;
                //��苗����ۂ��čU��
                case 5:
                    this.transform.rotation = Quaternion.FromToRotation(-Vector2.up, dir);
                    if (dis > 5) transform.Translate(Vector2.up * Time.deltaTime * ESp);
                    else transform.Translate(Vector2.up * Time.deltaTime * -ESp);
                    if (i < EAt[2] * 120) i++;
                    else
                    {
                        Instantiate(EBullet, transform.position, transform.rotation); //����
                        i = 0;
                    }
                    break;
            }
        }
        //�v���C���[������ȏ㗣��ď����o�߂���Ə���
        if (dis > 25)
        {
            j++;
            if (j <= 240)
            {
                //�G�̏ꍇ�G�̃J�E���^�[�����炷
                if (gameObject.tag == "Enemy" && !name.Contains("����")) EnemySummon.enemynum--;
                Destroy(gameObject);
            }
            else j = 0;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (Live)
        {
            //�v���C���[�Ƃ̍����̋������v�Z
            float dis = GameObject.Find("Player").transform.position.y - transform.position.y;
            //�v���C���[�ɂԂ���Ƒ����HP�����炷
            if (collision.gameObject.tag == "Player" && AtInterval >= 120)
            {
                Debug.Log("A");
                collision.gameObject.GetComponent<PlayerMoob>().Damage(EAt);
                AtInterval = 0;
            }
            //�v���C���[��������荂���ʒu�ɂ��鎞�W�����v
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
    }
    void statusSet()
    {
        Name = this.name;
        //���O����(Clone)�����O
        Name = Name.Replace("(Clone)", "");
        switch (Name)
        {
            case "�e�X�g":
                ENa = ""; //���O
                EHp = 100000; //�̗�
                EAt = new int[] { 10, 0 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 0; //�h���
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 0; //�ړ����x
                EMo = 0; //�s��AI
                ECo = new int[] { 0, 0, 0, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 0;
                break;
            case "�܂�":
                ENa = "�܂�"; //���O
                EHp = 120; //�̗�
                EAt = new int[] { 10, 0 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 1; //�h���
                EArDf = new float[] { 1.0f, 1.5f, 1.0f, 1.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 2; //�ړ����x
                EMo = 1; //�s��AI
                ECo = new int[] { 20, 0, 0, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 10;
                break;
            case "���񂩂�":
                ENa = "���񂩂�";//���O
                EHp = 80; //�̗�
                EAt = new int[] { 18, 0 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 1; //�h���
                EArDf = new float[] { 1.0f, 1.0f, 2.0f, 1.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 4; //�ړ����x
                EMo = 2; //�s��AI
                ECo = new int[] { 30, 10, 0 , 0, 0, 0}; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 6, 10 };//���Ƃ�����
                EEx = 20;
                break;
            case "������":
                ENa = "������";//���O
                EHp = 100; //�̗�
                EAt = new int[] { 10, 3 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 5; //�h���
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 0.5f, 1.5f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 1; //�ړ����x
                EMo = 1; //�s��AI
                ECo = new int[] { 50, 50, 0, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 30;
                break;
            case "�������o���A":
                ENa = "�������o���A";//���O
                EHp = 150; //�̗�
                EAt = new int[] { 10, 3 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 10; //�h���
                EArDf = new float[] { 1.0f, 1.0f, 2.0f, 0.0f, 2.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 1; //�ړ����x
                EMo = 0; //�s��AI
                ECo = new int[] { 0, 0, 0, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 0;
                break;
            case "�������":
                ENa = "�������";//���O
                EHp = 250; //�̗�
                EAt = new int[] { 5, 0 ,3 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 0; //�h���
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 2.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 1; //�ړ����x
                EMo = 4; //�s��AI
                ECo = new int[] { 50, 40, 10, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 25;
                break;
            case "���т������":
                ENa = "���т������";//���O
                EHp = 10; //�̗�
                EAt = new int[] { 12, 0 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 0; //�h���
                EArDf = new float[] { 1.0f, 1.0f, 1.0f, 2.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 2.5f; //�ړ����x
                EMo = 1; //�s��AI
                ECo = new int[] { 10, 00, 0, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 2;
                break;
            case "�₶�邵":
                ENa = "�₶�邵";//���O
                EHp = 80; //�̗�
                EAt = new int[] { 12, 2, 1 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 0; //�h���
                EArDf = new float[] { 1.0f, 2.0f, 1.0f, 2.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 2f; //�ړ����x
                EMo = 5; //�s��AI
                ECo = new int[] { 80, 20, 0, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 20;
                break;
            case "���т₶�邵":
                ENa = "���т₶�邵";//���O
                EHp = 1; //�̗�
                EAt = new int[] { 12, 2 }; //�U����[�З�,����.�U�����x(�e�ۂ��΂��G�̂�)]
                EDf = 0; //�h���
                EArDf = new float[] { 1.0f, 2.0f, 1.0f, 2.0f, 1.0f }; //�����ϐ�[������,��,�X.��,��]
                ESp = 2.5f; //�ړ����x
                EMo = 2_1; //�s��AI
                ECo = new int[] { 80, 20, 0, 0, 0, 0 }; //���Ƃ��R�C��[1,5,10,50,100,500]�𗎂Ƃ��m��
                EDr = new int[] { 0, 0 };//���Ƃ�����
                EEx = 20;
                break;
        }
    }
}