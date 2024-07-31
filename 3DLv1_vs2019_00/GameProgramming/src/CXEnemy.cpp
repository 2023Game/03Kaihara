#include "CXEnemy.h"
#include "CInput.h"
#include "CCollider.h"

//コライダの初期化
CXEnemy::CXEnemy()
    : mColSphereHead(this, nullptr, CVector(0.0f, 1.f, 0.0f), 1.5f)
    , mColSphereSword0(this, nullptr, CVector(0.7f, 3.5f, -0.2f), 0.5f)
    , mColSphereSword1(this, nullptr, CVector(0.5f, 2.5f, -0.2f), 0.5f)
    , mColSphereSword2(this, nullptr, CVector(0.3f, 1.5f, -0.2f), 0.5f)

{
}

void CXEnemy::Init(CModelX* model)
{
    CXCharacter::Init(model);
    //合成行列の設定
    mColSphereBody.Matrix(&mpCombinedMatrix[1]);
    //頭
    mColSphereHead.Matrix(&mpCombinedMatrix[1]);
    //剣
    mColSphereSword0.Matrix(&mpCombinedMatrix[26]);
    mColSphereSword1.Matrix(&mpCombinedMatrix[26]);
    mColSphereSword2.Matrix(&mpCombinedMatrix[26]);

}

void CXEnemy::Collision(CCollider* m, CCollider* o)
{
    if (m->Type() == CCollider::EType::ESPHERE)
    {
        if (o->Type() == CCollider::EType::ESPHERE)
        {
            if (o->Tag() == CCollider::ETag::ESWORD)
            {
                if (m->Tag() == CCollider::ETag::EBODY)
                {
                    if (CCollider::Collision(m, o))
                    {
                        ChangeAnimation(11, false, 30);
                    }
                }
            }
        }
    }
}
