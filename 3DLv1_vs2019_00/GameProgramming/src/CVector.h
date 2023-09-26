#ifndef CVECTOR_H
#define CVECTOR_H
/*
�x�N�g���N���X
�x�N�g���f�[�^�������܂�
*/
class CVector {
public:
	//+���Z�q�̃I�[�o�[���[�h
	//CVector + CVector �̉��Z���ʂ�Ԃ�
	CVector operator+(const CVector& v) const;
	//-���Z�q�̃I�[�o�[���[�h
	//CVector - CVector �̉��Z���ʂ�Ԃ�
	CVector operator-(const CVector& v) const;
	//�f�t�H���g�R���g���X�^
	CVector();
	// �R���g���X�^
	// CVector(X���W,Y���W,Z���W)
	//�e���ł̒l�̐ݒ�
	CVector(float x, float y, float z);
	//Set(X���W, Y���W, Z���W)
	void Set(float x, float y, float z);
	//X�̒l�𓾂�
	float X() const;
	//Y�̒l�𓾂�
	float Y() const;
	//Z�̒l�𓾂�
	float Z() const;
private:
	//3D�e���ł̒l��ݒ�
	float mX, mY, mZ;
};
#endif
