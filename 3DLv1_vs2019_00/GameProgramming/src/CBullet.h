#ifndef CBULLET_H
#define CBULLET_H
//�L�����N�^�N���X�̃C���N���[�h
#include "CCharacter3.h"
//�O�p�`�N���X�̃C���N���[�h
#include "CTriangle.h"
#include "CCollider.h"
#include "CCollisionManager.h"
/*
�e�N���X
�O�p�`���΂�
*/
class CBullet : public CCharacter3{
public:
	//�Փˏ���
	void CBullet::Collision()
	{
		//�R���C�_�̗D��x�ύX
		mCollider1.ChangePriority();
		//�Փˏ��������s
		CCollisionManager::Instance()->Collision(&mCollider1, COLLISIONRANGE);
	}
	//�Փˏ���
	//Collision(�R���C�_1, �R���C�_2)
	void Collision(CCollider* m, CCollider* o);
	CBullet();
	//���Ɖ��s���̐ݒ�
	//Set(��, ���s)
	void Set(float w, float d);
	//�X�V
	void Update();
	//�`��
	void Render();
private:
	//�R���C�_
	CCollider mCollider1;
	//��������
	int mLife;
	//�O�p�`
	CTriangle mT;
};

#endif


