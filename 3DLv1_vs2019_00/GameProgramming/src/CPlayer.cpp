//プレイヤークラスのインクルード
#include "CPlayer.h"

#define ROTATION_YV	CVector(0.0f, 1.0f, 0.0f) //回転速度

//CPlayer(位置, 回転, スケール)
CPlayer::CPlayer(const CVector& pos, const CVector& rot
	, const CVector& scale)
{
	CTransform::Update(pos, rot, scale); //行列の更新
}

//更新処理
void CPlayer::Update() {
	//Dキー入力で回転
	if (mInput.Key('D')) {
		//Y軸の回転値を減少
		mRotation = mRotation - ROTATION_YV;
	}
	//変換行列の更新
	CTransform::Update();
}
