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
    int DIn = 60; //�_���[�W���󂯂�Ԋu
    public static int HaveCoin = 100; //�����R�C����
    int DashInterval = 60; //�_�b�V���̃N�[���^�C��
    float damage; //�_���[�W
    float spead = 0.12f; //�ړ����x
    new Transform transform;
    Rigidbody2D rigid;
    AudioSource audioSource;
    public AudioClip Dash;
    public AudioClip DamageSE;
    public AudioClip KeikokuSE;
    public Transform Player; // �������I�u�W�F�N�g�̃g�����X�t�H�[��
    public Transform Aim; // �^�[�Q�b�g�̃I�u�W�F�N�g�̃g�����X�t�H�[��
    public Text PlayerText;
    void Start()
    {
        //�t���[�����[�g��60�ɂ���
        Application.targetFrameRate = 60;
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
            rigid.velocity += new Vector2(0.6f, 0f);  //��
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity += new Vector2(-0.6f, 0f); //��
        }
            rigid.velocity *= new Vector2(0.9f, 1); //���X�Ɍ��� 
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        //�n�ʂ𓥂񂾏�ԂŃX�y�[�X�������ƃW�����v
        if (collision.gameObject.tag == "Grand" && Input.GetKey(KeyCode.Space) && rigid.velocity.magnitude <= 8f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 8f);
        }
    }
    public void Damage(int[] EAt)
    {
        //�G�̍U����-�v���C���[�̖h��́~�����ϐ�
        damage = EAt[0] - PDf * PRe[EAt[1]];
        if (damage <= 0) damage = 1; //0�_���[�W�ȉ��Ȃ�1�_���[�W�ɂ���
        PHp -= damage;
        audioSource.PlayOneShot(DamageSE);
        gameObject.GetComponent<Renderer>().material.color *= new Color(0.5f, 0.5f, 0.5f, 0.5f);//���G���Ԓ��F���Â�����
        StartCoroutine(Utilities.DelayMethod(1, () => gameObject.GetComponent<Renderer>().material.color *= new Color(2f, 2f, 2f, 2f)));
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
}
