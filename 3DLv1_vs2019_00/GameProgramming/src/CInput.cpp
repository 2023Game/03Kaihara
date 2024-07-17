#include "CInput.h"
#include <stdio.h>

GLFWwindow* CInput::spWindow = nullptr;	//�E�B���h�E�̃|�C���^

void CInput::Window(GLFWwindow* pwindow)
{
	spWindow = pwindow;
}

void CInput::GetMousePos(float* px, float* py)
{
	double xpos, ypos;
	glfwGetCursorPos(spWindow, &xpos, &ypos);
	*px = (float)xpos;
	*py = (float)ypos;
	return;
}

CInput::CInput()
{
	
}

bool CInput::Key(char key)
{
	return GetAsyncKeyState(key) < 0;
}
