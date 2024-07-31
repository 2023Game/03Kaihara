#include "CXCharacter.h"
#include "CCollider.h"

class CXEnemy : public CXCharacter{
public:
	//衝突処理
	void Collision(CCollider* m, CCollider* o);
	void CXEnemy::Init(CModelX* model);
	//デフォルトコンストラクタ
	CXEnemy();
	CCollider mColSphereHead;	//頭
	CCollider mColSphereSword0;	//剣
	CCollider mColSphereSword1;	//剣
	CCollider mColSphereSword2;	//剣
	CXCharacter mXCharacter;
private:
	//コライダの宣言
	CCollider mColSphereBody;	//体
};