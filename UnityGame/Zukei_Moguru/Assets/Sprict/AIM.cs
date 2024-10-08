using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIM : MonoBehaviour
{
    new Transform transform;
    //座標用の変数
    Vector2 mousePos, worldPos;
    GameObject player;

    void FixedUpdate()
    {
        //マウス座標の取得
        mousePos = Input.mousePosition;
        //プレイヤーの座標を取得
        this.player = GameObject.Find("Player");
        transform = GetComponent<Transform>();
        //スクリーン座標をワールド座標に変換
        worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        //ワールド座標を自身の座標に設定
        transform.position = new(worldPos.x, worldPos.y);
    }
}
