#include "CXCharacter.h"
#include "CInput.h"
#include "CCollider.h"

class CXPlayer : public CXCharacter
{
public:
	void CXPlayer::Init(CModelX* model);
	//�f�t�H���g�R���X�g���N�^
	CXPlayer();
	void Update();
	CCollider mColSphereHead;	//��
	CCollider mColSphereSword;	//��
	CInput mInput;
	CXCharacter mXCharacter;
private:
	//�R���C�_�̐錾
	CCollider mColSphereBody;	//��
};