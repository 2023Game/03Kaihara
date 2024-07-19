#include "CXCharacter.h"
#include "CCollider.h"

class CXEnemy : public CXCharacter{
public:
	void CXEnemy::Init(CModelX* model);
	//デフォルトコンストラクタ
	CXEnemy();
	CCollider mColSphereHead;	//頭
	CCollider mColSphereSword;	//剣
	CXCharacter mXCharacter;
private:
	//コライダの宣言
	CCollider mColSphereBody;	//体
};