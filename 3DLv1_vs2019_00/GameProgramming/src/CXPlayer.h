#include "CXCharacter.h"
#include "CInput.h"
#include "CCollider.h"

class CXPlayer : public CXCharacter
{
public:
	void CXPlayer::Init(CModelX* model);
	//デフォルトコンストラクタ
	CXPlayer();
	void Update();
	CCollider mColSphereHead;	//頭
	CCollider mColSphereSword;	//剣
	CInput mInput;
	CXCharacter mXCharacter;
private:
	//コライダの宣言
	CCollider mColSphereBody;	//体
};