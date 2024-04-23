#include "CMatrix.h"
#include "CVector.h"
//標準入出力関数のインクルード
#include <stdio.h>
//円周率M_PIを有効にする
#define _USE_MATH_DEFINES
//数学関数のインクルード
#include <math.h>

//*演算子のオーバーロード
//CMatrix * CMatrix の演算結果を返す
const CMatrix CMatrix::operator*(const CMatrix& m) const
{
	CMatrix t;
	for(int a = 0;a <= 3; a++ )
	{
			t.mM[a][0] = mM[a][0] * m.mM[0][0] + mM[a][1] * m.mM[1][0] + mM[a][2] * m.mM[2][0] + mM[a][3] * m.mM[3][0];
			t.mM[a][1] = mM[a][0] * m.mM[0][1] + mM[a][1] * m.mM[1][1] + mM[a][2] * m.mM[2][1] + mM[a][3] * m.mM[3][1];
			t.mM[a][2] = mM[a][0] * m.mM[0][2] + mM[a][1] * m.mM[1][2] + mM[a][2] * m.mM[2][2] + mM[a][3] * m.mM[3][2];
			t.mM[a][3] = mM[a][0] * m.mM[0][3] + mM[a][1] * m.mM[1][3] + mM[a][2] * m.mM[2][3] + mM[a][3] * m.mM[3][3];
	}
	
	return t;
}


//移動行列の作成
//Translate(移動量X, 移動量Y, 移動量Z)
CMatrix CMatrix::Translate(float mx, float my, float mz) {
	//単位行列にする
	Identity();
	mM[3][0] = mx; mM[3][1] = my; mM[3][2] = mz;
	//この行列を返す
	return *this;
}

void CMatrix::M(int row, int col, float value)
{
	mM[row][col] = value;
}

//回転行列（X軸）の作成
//RotateX(角度)
CMatrix CMatrix::RotateX(float degree) {
	//角度からラジアンを求める
	float rad = degree / 180.0f * M_PI;
	//単位行列にする
	Identity();
	//X軸で回転する行列の設定
	mM[1][1] = mM[2][2] = cos(rad);
	mM[2][1] = -sinf(rad);
	mM[1][2] = -mM[2][1];
	//行列を返す
	return *this;
}

//回転行列（Y軸）の作成
//RotateY(角度)
CMatrix CMatrix::RotateY(float degree) {
	//角度からラジアンを求める
	float rad = degree / 180.0f * M_PI;
	//単位行列にする
	Identity();
	//Y軸で回転する行列の設定
	mM[0][0] = mM[2][2] = cosf(rad);
	mM[2][0] = sinf(rad);
	mM[0][2] = -mM[2][0];
	//行列を返す
	return *this;
}

//回転行列（Z軸）の作成
//RotateZ(角度)
CMatrix CMatrix::RotateZ(float degree) {
	//角度からラジアンを求める
	float rad = degree / 180.0f * M_PI;
	//単位行列にする
	Identity();
	//Z軸で回転する行列の設定
	mM[0][0] = mM[1][1] = cosf(rad);
	mM[0][1] = -sinf(rad);
	mM[1][0] = -mM[0][1];
	//行列を返す
	return *this;
}

void CMatrix::Print() {
	printf("%10f %10f %10f %10f\n",
		mM[0][0], mM[0][1], mM[0][2], mM[0][3]);
	printf("%10f %10f %10f %10f\n",
		mM[1][0], mM[1][1], mM[1][2], mM[1][3]);
	printf("%10f %10f %10f %10f\n",
		mM[2][0], mM[2][1], mM[2][2], mM[2][3]);
	printf("%10f %10f %10f %10f\n",
		mM[3][0], mM[3][1], mM[3][2], mM[3][3]);
}
//デフォルトコンストラクタ
CMatrix::CMatrix() {
	Identity();
}
//単位行列の作成
CMatrix CMatrix::Identity() {
		mM[0][0] = 1; mM[0][1] = 0; mM[0][2] = 0; mM[0][3] = 0;
		mM[1][0] = 0; mM[1][1] = 1; mM[1][2] = 0; mM[1][3] = 0;
		mM[2][0] = 0; mM[2][1] = 0; mM[2][2] = 1; mM[2][3] = 0;
		mM[3][0] = 0; mM[3][1] = 0; mM[3][2] = 0; mM[3][3] = 1;
	//この行列を返す
	return *this;
}

//拡大縮小行列の作成
//Scale(倍率X, 倍率Y, 倍率Z)
CMatrix CMatrix::Scale(float sx, float sy, float sz) {
	//単位行列にする
	Identity();
	//倍率設定
	mM[0][0] = sx; mM[1][1] = sy; mM[2][2] = sz;
	//この行列を返す
	return *this;
}

float CMatrix::M(int r, int c) const{
	return mM[r][c];
}

float* CMatrix::M() const
{
	return (float*)mM[0];
}

CMatrix CMatrix::Transpose() const
{
	CMatrix tmp;
	tmp.mM[0][0] = mM[0][0];
	tmp.mM[0][1] = mM[1][0];
	tmp.mM[0][2] = mM[2][0];
	tmp.mM[0][3] = mM[3][0];
	tmp.mM[1][0] = mM[0][1];
	tmp.mM[1][1] = mM[1][1];
	tmp.mM[1][2] = mM[2][1];
	tmp.mM[1][3] = mM[3][1];
	tmp.mM[2][0] = mM[0][2];
	tmp.mM[2][1] = mM[1][2];
	tmp.mM[2][2] = mM[2][2];
	tmp.mM[2][3] = mM[3][2];
	tmp.mM[3][0] = mM[0][3];
	tmp.mM[3][1] = mM[1][3];
	tmp.mM[3][2] = mM[2][3];
	tmp.mM[3][3] = mM[3][3];
	return tmp;
}

CVector CMatrix::VectorZ() const
{
	return CVector(mM[2][0], mM[2][1], mM[2][2]);
}
CVector CMatrix::VectorX() const
{
	return CVector(mM[0][0], mM[0][1], mM[0][2]);
}
CVector CMatrix::VectorY() const
{
	return CVector(mM[1][0], mM[1][1], mM[1][2]);
}

int CMatrix::Size()
{
	return sizeof(mM) / sizeof(float);
}