#ifndef CPLAYER_H
#define CPLAYER_H
//キャラクタクラスのインクルード
#include "CCharacter3.h"
#include "CInput.h"
#include "CColliderLine.h"
#include "CCollider.h"
#include "CCollisionManager.h"
/*
プレイヤークラス
キャラクタクラスを継承
*/
class CPlayer : public CCharacter3 {
	//衝突処理
	void CPlayer::Collision()
	{
		//コライダの優先度変更
		mLine.ChangePriority();
		mLine2.ChangePriority();
		mLine3.ChangePriority();
		//衝突処理を実行
		CCollisionManager::Instance()->Collision(&mLine, COLLISIONRANGE);
		CCollisionManager::Instance()->Collision(&mLine2, COLLISIONRANGE);
		CCollisionManager::Instance()->Collision(&mLine3, COLLISIONRANGE);
	}
public:
	//衝突処理
	void Collision(CCollider* m, CCollider* o);
	CPlayer::CPlayer()
		: mLine(this, &mMatrix, CVector(0.0f, 0.0f, -14.0f), CVector(0.0f, 0.0f, 17.0f))
		, mLine2(this, &mMatrix, CVector(0.0, 5.0, -8.0), CVector(0.0, -3.0, -8.0))
		, mLine3(this, &mMatrix, CVector(9.0, 0.0, -8.0), CVector(-9.0, 0.0, -8.0))
	{
	}
	//CPlayer(位置, 回転, スケール)
	CPlayer(const CVector& pos, const CVector& rot
		, const CVector& scale);
	//更新処理
	void Update();
private:
	CInput mInput;
	CColliderLine mLine; //線分コライダ
	CColliderLine mLine2; //線分コライダ
	CColliderLine mLine3; //線分コライダ
};

#endif
