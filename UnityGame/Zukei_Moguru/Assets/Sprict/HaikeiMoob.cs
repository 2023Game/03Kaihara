using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaikeiMoob : MonoBehaviour
{
    new Transform transform;
    GameObject player;

    void FixedUpdate()
    {
        //プレイヤーの座標を取得
        player = GameObject.Find("Player");
        transform = GetComponent<Transform>();
        //プレイヤー座標を自身の座標に設定
        transform.position = new(player.transform.position.x * 0.8f, player.transform.position.y * 0.8f);
    }
}
