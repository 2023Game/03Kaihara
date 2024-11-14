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
    public static bool moob = true; //�����邩�ǂ���
    static public float PHp = 100; //����HP
    static public float MaxPHp = 100; //�ő�HP
    static public float EXP; //�����o���l
    int PDf = 0; //�h���
    float[] PRe = new float[5] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //�����ϐ�
    int PHe = 0; //�Đ���
    int a = 0;
    int DIn = 120; //�_���[�W���󂯂�Ԋu
    public static int HaveCoin = 100; //�����R�C����
    int DashInterval = 60; //�_�b�V���̃N�[���^�C��
    float damage; //�_���[�W
    float spead = 0.6f; //�ړ����x
    new Transform transform;
    Rigidbody2D rigid;
    AudioSource audioSource;
    public AudioClip Dash;
    public AudioClip DamageSE;
    public AudioClip KeikokuSE;
    public Transform Player; // �������I�u�W�F�N�g�̃g�����X�t�H�[��
    public Transform Aim; // �^�[�Q�b�g�̃I�u�W�F�N�g�̃g�����X�t�H�[��
    public Text PlayerText;
    bool Isground = false; //�n�ɑ���t���Ă��邩
    bool Jamp = false; //�W�����v����
    void Start()
    {
        //�t���[�����[�g��120�ɂ���
        Application.targetFrameRate = 120;
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ���������������v�Z
        Vector2 dir = (Aim.position - Player.position);
        // �������������ɉ�]
        Player.rotation = Quaternion.FromToRotation(Vector2.right, dir);
        if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity += new Vector2(spead, 0f);  //��
            if (Input.GetMouseButton(1) && DashInterval <= 0)//���N���b�N�Ń_�b�V��
            {
                rigid.velocity += new Vector2(6f, 0f);  //���_�b�V��
                DashInterval = 120;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity += new Vector2(-spead, 0f); //��

        }
        if (Input.GetMouseButton(1) && DashInterval <= 0)//���N���b�N�Ń_�b�V��
        {
            audioSource.PlayOneShot(Dash);
            spead *= 3;
            StartCoroutine(Utilities.DelayMethod(0.3f, () => spead /= 3));
            DashInterval = 120;
        }
        rigid.velocity *= new Vector2(0.9f, 1); //���X�Ɍ��� 

        DashInterval--;

        if (Input.GetKey(KeyCode.Space) && Isground && !Jamp)//�n�ʂ𓥂�ł����ԂŃX�y�[�X�������ƃW�����v
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 8f);
            a = 0;
            Jamp = true;
        }
        else if (Input.GetKey(KeyCode.Space) && a < 20)//�������Œ��W�����v
        {
            a++;
        }
        else if(a == 60 || Jamp)//���ȏ㒷�W�����v���邩�X�y�[�X�𗣂��ƃW�����v���~�߂�
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
        //�G�̍U����-�v���C���[�̖h��́~�����ϐ�
        damage = EAt[0] - PDf * PRe[EAt[1]];
        if (damage <= 0) damage = 1; //0�_���[�W�ȉ��Ȃ�1�_���[�W�ɂ���
        PHp -= damage;
        audioSource.PlayOneShot(DamageSE);
        gameObject.GetComponent<Renderer>().material.color *= new Color(0.5f, 0.5f, 0.5f, 1f);//���G���Ԓ��F���Â�����
        StartCoroutine(Utilities.DelayMethod(1, () => gameObject.GetComponent<Renderer>().material.color *= new Color(2f, 2f, 2f, 1f)));
        gameObject.layer = 1; //�G�ƏՓ˂��Ȃ����C���[�ɐ؂�ւ���
        StartCoroutine(Utilities.DelayMethod(1, () => gameObject.layer = 0));
        //HP���c�菭�Ȃ����x������炷
        if (PHp <= MaxPHp / 5)
        {
            audioSource.PlayOneShot(KeikokuSE);
        }
    }
    public void CoinGet(int Coin)
    {
        //�R�C�����E��
        HaveCoin += Coin;
        audioSource.Play();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //�n�ʂ𓥂�ł��邩
        if (collision.gameObject.tag == "Grand")
        {
            Isground = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //�n�ʂ��痣�ꂽ��
        if (collision.gameObject.tag == "Grand")
        {
            Isground = false;
        }
    }
}
