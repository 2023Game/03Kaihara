#include "CXPlayer.h"
#include "CInput.h"
#include "CCollider.h"

//�R���C�_�̏�����
CXPlayer::CXPlayer()
    : mColSphereBody(this, nullptr, 
        CVector(0.0f ,0.0f, 0.0f), 0.5f)
    , mColSphereHead(this, nullptr,
        CVector(0.0f, 5.0f, -3.0f), 0.5f)
    , mColSphereSword(this, nullptr,
        CVector(-10.0f, 10.0f, 50.0f), 0.3f, CCollider::ETag::ESWORD)
{
}

void CXPlayer::Init(CModelX* model)
{
    CXCharacter::Init(model);
    //�����s��̐ݒ�
    mColSphereBody.Matrix(&mpCombinedMatrix[8]);
    //��
    mColSphereHead.Matrix(&mpCombinedMatrix[11]);
    //��
    mColSphereSword.Matrix(&mpCombinedMatrix[21]);
}

void CXPlayer::Update()
{
    if (!(mAnimationIndex == 3 || mAnimationIndex == 4))
    {
        if (mInput.Key('A'))
        {
            mRotation.Y(mRotation.Y() + 2.0f);
        }
        if (mInput.Key('D'))
        {
            mRotation.Y(mRotation.Y() - 2.0f);
        }
        if (mInput.Key('W'))
        {
            ChangeAnimation(1, true, 60);
            Position(Position() + mMatrixRotate.VectorZ() * 0.1);
        }
        else
        {
            ChangeAnimation(0, true, 60);
        }
    }
    if (mInput.Key(' '))
    {
        ChangeAnimation(3, false, 30);
    }
    if (mAnimationIndex == 3 && mAnimationFrame >= 30)
    {
        ChangeAnimation(4, false, 30);
        if (mAnimationFrame >= 30)  ChangeAnimation(0, true, 60);
    }
    CXCharacter::Update();
}