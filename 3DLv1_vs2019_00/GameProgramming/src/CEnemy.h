#ifndef CENEMY_H
#define CENEMY_H
//キャラクタクラスのインクルード
#include "CCharacter3.h"
#include "CCollider.h"
#include "CTriangle.h"
#include "CCollider.h"
#include "CCollisionManager.h"
/*
エネミークラス
キャラクタクラスを継承
*/
class CEnemy : public CCharacter3 {
public:
	//衝突処理
	void CEnemy::Collision()
	{
		//コライダの優先度変更
		mCollider1.ChangePriority();
		mCollider2.ChangePriority();
		mCollider3.ChangePriority();
		//衝突処理を実行
		CCollisionManager::Instance()->Collision(&mCollider1, COLLISIONRANGE);
		CCollisionManager::Instance()->Collision(&mCollider2, COLLISIONRANGE);
		CCollisionManager::Instance()->Collision(&mCollider3, COLLISIONRANGE);
	}

	//衝突処理
	//Collision(コライダ1, コライダ2)
	void Collision(CCollider* m, CCollider* o);
	//コンストラクタ
	//CEnemy(モデル, 位置, 回転, 拡縮)
	CEnemy(CModel* model, const CVector& position,
		const CVector& rotation, const CVector& scale);
	//更新処理
	void Update();
private:
	//コライダ
	CCollider mCollider1;
	CCollider mCollider2;
	CCollider mCollider3;
};
#endif
