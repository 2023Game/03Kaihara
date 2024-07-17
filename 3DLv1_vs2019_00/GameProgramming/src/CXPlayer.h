#include "CXCharacter.h"
#include "CInput.h"

class CXPlayer : public CXCharacter
{
public:
	void Update();
	CInput mInput;
	CXCharacter mXCharacter;
};
