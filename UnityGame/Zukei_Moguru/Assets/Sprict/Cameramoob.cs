using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramoob : MonoBehaviour
{
    new Transform transform;
    GameObject player;

    void FixedUpdate()
    {
        //�v���C���[�̍��W���擾
        player = GameObject.Find("Player");
        transform = GetComponent<Transform>();
        //�v���C���[���W�����g�̍��W�ɐݒ�
        transform.position = new(player.transform.position.x, player.transform.position.y ,-10f);
    }
}
