using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIM : MonoBehaviour
{
    new Transform transform;
    //���W�p�̕ϐ�
    Vector2 mousePos, worldPos;
    GameObject player;

    void FixedUpdate()
    {
        //�}�E�X���W�̎擾
        mousePos = Input.mousePosition;
        //�v���C���[�̍��W���擾
        this.player = GameObject.Find("Player");
        transform = GetComponent<Transform>();
        //�X�N���[�����W�����[���h���W�ɕϊ�
        worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        //���[���h���W�����g�̍��W�ɐݒ�
        transform.position = new(worldPos.x, worldPos.y);
    }
}
