#include "CXCharacter.h"
#include "CCollider.h"

class CXEnemy : public CXCharacter{
public:
	//�Փˏ���
	void Collision(CCollider* m, CCollider* o);
	void CXEnemy::Init(CModelX* model);
	//�f�t�H���g�R���X�g���N�^
	CXEnemy();
	CCollider mColSphereHead;	//��
	CCollider mColSphereSword;	//��
	CXCharacter mXCharacter;
private:
	//�R���C�_�̐錾
	CCollider mColSphereBody;	//��
};