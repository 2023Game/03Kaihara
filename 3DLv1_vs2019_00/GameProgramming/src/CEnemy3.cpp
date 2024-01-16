#include "CEnemy3.h"
#include "CEffect.h"
#include "CPlayer.h"
#include "CBullet.h"
#include "CMaterial.h"

#define OBJ "res\\f16.obj"	//���f���̃t�@�C��
#define MTL "res\\f16.mtl"	//���f���̃}�e���A���t�@�C��

CModel CEnemy3::sModel;	//���f���f�[�^�쐬

//�R���X�g���N�^
//CEnemy3(�ʒu, ��], �g�k)
CEnemy3::CEnemy3(const CVector& position
	, const CVector& rotation, const CVector& scale)
	: CEnemy3()	//�f�t�H���g�R���X�g���N�^�����s����
{
	//�ʒu�A��]�A�g�k��ݒ肷��
	mPosition = position;	//�ʒu�̐ݒ�
	mRotation = rotation;	//��]�̐ݒ�
	mScale = scale;	//�g�k�̐ݒ�
	CTransform::Update();	//�s��̍X�V
}


CEnemy3::CEnemy3()
	: CCharacter3(1)
	, mCollider(this, &mMatrix, CVector(0.0f, 0.0f, 0.0f), 0.4f)
{
	//���f���������Ƃ��͓ǂݍ���
	if (sModel.Triangles().size() == 0)
	{
		sModel.Load(OBJ, MTL);
	}
	//���f���̃|�C���^�ݒ�
	mpModel = &sModel;
}

//�X�V����
void CEnemy3::Update()
{
	//�v���C���[�̃|�C���^��0�ȊO�̎�
	CPlayer* player = CPlayer::Instance();
	if (player != nullptr)
	{
		//�v���C���[�܂ł̃x�N�g�������߂�
		CVector vp = player->Position() - mPosition;
		//���x�N�g���Ƃ̓��ς����߂�
		float dx = vp.Dot(mMatrixRotate.VectorX());
		//��x�N�g���Ƃ̓��ς����߂�
		float dy = vp.Dot(mMatrixRotate.VectorY());
		float dz = vp.Dot(mMatrixRotate.VectorZ());

		//X���̃Y����2.0�ȉ�
		if (-2.0f < dx && dx < 2.0f)
		{
			//Y���̃Y����2.0�ȉ�
			if (-2.0f < dy && dy < 2.0f)
			{
				if (dz > -2.0f)
				{
					if (dx * dy * dz <= 30)
					{
						//�e�𔭎˂��܂�
						CBullet* bullet = new CBullet();
						bullet->Set(0.1f, 1.5f);
						bullet->Position(
							CVector(0.0f, 0.0f, 10.0f) * mMatrix);
						bullet->Rotation(mRotation);
						bullet->Update();
					}
				}
			}
		}
	}
}

//�Փˏ���
	void CEnemy3::Collision()
	{
		//�R���C�_�̗D��x�ύX
		mCollider.ChangePriority();
		//�Փˏ��������s
		CCollisionManager::Instance()->Collision(&mCollider, COLLISIONRANGE);
	}

//�Փˏ���
//Collision(�R���C�_1, �R���C�_2)
void CEnemy3::Collision(CCollider* m, CCollider* o) {
	//����̃R���C�_�^�C�v�̔���
	switch (o->Type())
	{
	case CCollider::EType::ESPHERE: //���R���C�_�̎�
		//�R���C�_��m��y���Փ˂��Ă��邩����
		if (CCollider::Collision(m, o)) {
			//�G�t�F�N�g����
			new CEffect(o->Parent()->Position(), 1.0f, 1.0f, "exp.tga", 4, 4, 2);
			//�Փ˂��Ă��鎞�͖����ɂ���
			//mEnabled = false;
		}
		break;
	case CCollider::EType::ETRIANGLE: //�O�p�R���C�_�̎�
		CVector adjust; //�����l
		//�O�p�R���C�_�Ƌ��R���C�_�̏Փ˔���
		if (CCollider::CollisionTriangleSphere(o, m, &adjust))
		{	//�Փ˂��Ȃ��ʒu�܂Ŗ߂�
			mPosition = mPosition + adjust;
		}
		break;
	}
}
