#ifndef CENEMY_H
#define CENEMY_H
//�L�����N�^�N���X�̃C���N���[�h
#include "CCharacter3.h"
#include "CCollider.h"
#include "CTriangle.h"
#include "CCollider.h"
#include "CCollisionManager.h"
/*
�G�l�~�[�N���X
�L�����N�^�N���X���p��
*/
class CEnemy : public CCharacter3 {
public:
	//�Փˏ���
	void CEnemy::Collision()
	{
		//�R���C�_�̗D��x�ύX
		mCollider1.ChangePriority();
		mCollider2.ChangePriority();
		mCollider3.ChangePriority();
		//�Փˏ��������s
		CCollisionManager::Instance()->Collision(&mCollider1, COLLISIONRANGE);
		CCollisionManager::Instance()->Collision(&mCollider2, COLLISIONRANGE);
		CCollisionManager::Instance()->Collision(&mCollider3, COLLISIONRANGE);
	}

	//�Փˏ���
	//Collision(�R���C�_1, �R���C�_2)
	void Collision(CCollider* m, CCollider* o);
	//�R���X�g���N�^
	//CEnemy(���f��, �ʒu, ��], �g�k)
	CEnemy(CModel* model, const CVector& position,
		const CVector& rotation, const CVector& scale);
	//�X�V����
	void Update();
private:
	//�R���C�_
	CCollider mCollider1;
	CCollider mCollider2;
	CCollider mCollider3;
};
#endif
