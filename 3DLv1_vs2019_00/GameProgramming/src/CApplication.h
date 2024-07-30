#pragma once
#include "CTexture.h"
#include "CBullet.h"
#include "CEnemy.h"
#include "CEnemy3.h"
#include "CPlayer.h"
#include "CInput.h"
#include "CFont.h"
#include <vector>
#include "CVector.h"
#include "CSound.h"
#include "CModel.h"
#include "CTaskManager.h"
#include "CColliderTriangle.h"
#include "CColliderMesh.h"
#include "CCamera.h"
#include "CUi.h"
#include "CModelX.h"
#include "CXCharacter.h"
#include "CXPlayer.h"
#include "CXEnemy.h"

class CApplication
{
private:
	//敵のインスタンス
	CXEnemy mXEnemy;
	CModelX mKnight;
	//キャラクタのインスタンス
	CXPlayer mXPlayer;
	CMatrix mMatrix;
	CModelX mModelX;
	static CUi* spUi;	//UIクラスのポインタ
	//モデルからコライダを生成
	CColliderMesh mColliderMesh;
	//三角コライダの作成
	//CColliderTriangle mColliderTriangle;
	//三角コライダの作成
	//CColliderTriangle mColliderTriangle2;
	//モデルビューの逆行列
	static CMatrix mModelViewInverse;
	//C5モデル
	CModel mModelC5; 
	CPlayer mPlayer;
	CModel mBackGround; //背景モデル
	CSound mSoundBgm;	//BGM
	CSound mSoundOver;	//ゲームオーバー
	enum class EState
	{
		ESTART,	//ゲーム開始
		EPLAY,	//ゲーム中
		ECLEAR,	//ゲームクリア
		EOVER,	//ゲームオーバー
	};
	EState mState;
//	CCharacter mRectangle;
	CPlayer* mpPlayer;
	static CTexture mTexture;
	CEnemy* mpEnemy;
//	CBullet* mpBullet;
	CInput mInput;
	CFont mFont;
	CVector mEye;
	//モデルクラスのインスタンス作成
	CModel mModel;
	//CCharacterのポインタの可変長配列
//	std::vector<CCharacter*> mCharacters;
public:
	~CApplication();
	//モデルビュー行列の取得
	static CTexture* Texture();
	//最初に一度だけ実行するプログラム
	void Start();
	//繰り返し実行するプログラム
	void Update();
};