#include "CXPlayer.h"
#include "CInput.h"

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