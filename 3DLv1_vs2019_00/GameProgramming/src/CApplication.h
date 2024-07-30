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
	//�G�̃C���X�^���X
	CXEnemy mXEnemy;
	CModelX mKnight;
	//�L�����N�^�̃C���X�^���X
	CXPlayer mXPlayer;
	CMatrix mMatrix;
	CModelX mModelX;
	static CUi* spUi;	//UI�N���X�̃|�C���^
	//���f������R���C�_�𐶐�
	CColliderMesh mColliderMesh;
	//�O�p�R���C�_�̍쐬
	//CColliderTriangle mColliderTriangle;
	//�O�p�R���C�_�̍쐬
	//CColliderTriangle mColliderTriangle2;
	//���f���r���[�̋t�s��
	static CMatrix mModelViewInverse;
	//C5���f��
	CModel mModelC5; 
	CPlayer mPlayer;
	CModel mBackGround; //�w�i���f��
	CSound mSoundBgm;	//BGM
	CSound mSoundOver;	//�Q�[���I�[�o�[
	enum class EState
	{
		ESTART,	//�Q�[���J�n
		EPLAY,	//�Q�[����
		ECLEAR,	//�Q�[���N���A
		EOVER,	//�Q�[���I�[�o�[
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
	//���f���N���X�̃C���X�^���X�쐬
	CModel mModel;
	//CCharacter�̃|�C���^�̉ϒ��z��
//	std::vector<CCharacter*> mCharacters;
public:
	~CApplication();
	//���f���r���[�s��̎擾
	static CTexture* Texture();
	//�ŏ��Ɉ�x�������s����v���O����
	void Start();
	//�J��Ԃ����s����v���O����
	void Update();
};