#pragma once
#ifndef CCOLLISIONMANAGER_H
#define CCOLLISIONMANAGER_H
//衝突処理範囲より離れているコライダは衝突処理しない
#define COLLISIONRANGE 30

#include "CCollider.h"
#include "CTaskManager.h"

class CCollisionManager : public CTaskManager
{
public:
	//インスタンスの取得
	static CCollisionManager* Instance();
	//衝突処理
	void Collision();
	//衝突処理
	void Collision(CCollider* c, int range);
private:
	//マネージャのインスタンス
	static CCollisionManager* mpInstance;
};

#endif
