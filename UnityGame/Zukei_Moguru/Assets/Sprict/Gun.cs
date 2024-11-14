using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float[,] BSt;
    public string[] BNa; //�e�̖��O
    public float[] BAt; //�З�,����,�m�b�N�o�b�N
    public static float[] BRe; //���ˊԊu,���U��,�����[�h���x
    public float BSp; //�e��
    public float BWa; //���˕���
    public float BNu; //���ː�
    public float BPr; //���x
    public float BRa; //�˒�
    float[] BBa = new float[3]; //�e�ۂ̎��,���ˉ�,�؂�ւ���
    public float BKi; //�e�ۂ̋O��
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
        //���U����������
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
        Kirikae(); //�e�̐؂�ւ�����
        FireInterval++;
        //�e���c���Ă��ē�����Ȃ獶�N���b�N���ɒ���I�ɒe�𔭎�
        if (Input.GetMouseButton(0) && FireInterval >= 120 / BRe[0] && PlayerMoob.moob && Zandan[0, whs % 4] > 0)
        {
            Fire();
            FireInterval = 0;//���ˊԊu�����Z�b�g
        }
        else if ((Zandan[0, whs % 4] <= 0 || Input.GetKey(KeyCode.R)) && Reload) //�c�e��0�ȉ��ɂȂ邩R�������ƃ����[�h����
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
        Zandan[0, whs % 4]--; //�e�����炷
        switch (BWa)
        {
            //�P���e
            case 0:
                Dispersion(BulletPrefab);
                audioSource.PlayOneShot(ShotSE);
                transform.localRotation = new(0, 0, 0, 0);
                break;
            //�U�e�e
            case 1:
                for (int a = 0; BNu > a; ++a)
                {
                    StartCoroutine(Utilities.DelayMethod(a / 60, () => Dispersion(BulletPrefab)));
                }
                audioSource.PlayOneShot(ShotSE);
                transform.localRotation = new(0, 0, 0, 0);
                break;
            //�o�[�X�g�e
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
    void Dispersion(GameObject BulletPrefab) //���x�̐��l�����p�x�������_���ɕς��Ĕ���
    {
        float R = BPr / 360; //360�Ŋ����Ċp�x�ɕϊ�
        float index = UnityEngine.Random.Range(R, -R); //���x���̐U�ꕝ�̃����_���Ȑ��l���o��
        transform.localRotation = new(transform.localRotation.z, transform.localRotation.y, transform.localRotation.z + index, transform.localRotation.w);//�e�����u����
        pos = transform.position;
        rot = new(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        Instantiate(BulletPrefab, pos, rot); //����
    }

    void Kirikae()
    {
        wh = Input.GetAxis("Mouse ScrollWheel") * 10;//�}�E�X�z�C�[���̉�]�����m
        //�����؂�ւ������؂�ւ�����炷
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
        //�e�ۂ̃X�e�[�^�X��������
        BAt = new float[] { BSt[GunDeta.Buki[whs % 4], 0], BSt[GunDeta.Buki[whs % 4], 1], BSt[GunDeta.Buki[whs % 4], 2] };�@ //�З�,����,�m�b�N�o�b�N
        BRe = new float[] { BSt[GunDeta.Buki[whs % 4], 3], BSt[GunDeta.Buki[whs % 4], 4], BSt[GunDeta.Buki[whs % 4], 5] };  //���ˊԊu,���U��,�����[�h���x
        BSp = BSt[GunDeta.Buki[whs % 4], 6];   //�e��
        BWa = BSt[GunDeta.Buki[whs % 4], 7];   //���˕���
        BNu = BSt[GunDeta.Buki[whs % 4], 8];   //���ː�
        BPr = BSt[GunDeta.Buki[whs % 4], 9];   //���x
        BRa = BSt[GunDeta.Buki[whs % 4], 10];  //�˒�
        BBa = new float[] { BSt[GunDeta.Buki[whs % 4], 11], BSt[GunDeta.Buki[whs % 4], 12], BSt[GunDeta.Buki[whs % 4], 13] };//�e�ۂ̎��,�؂�ւ���
        BKi = BSt[GunDeta.Buki[whs % 4], 14];  //�e�ۂ̋O��
        Zandan[1, whs % 4] = BRe[1]; //�ő呕�U������
        //BBa�̐��l�����ɒe�ۂ̎��,���ˉ�,�؂�ւ�����}��
        BulletPrefab = Bullet[(int)BBa[0]];
        ShotSE = ShotSESet[(int)BBa[1]];
        KirikaeSE = KirikaeSESet[(int)BBa[2]];
    }
}