#ifndef __IHU_RESPECT_DIAG_H
#define __IHU_RESPECT_DIAG_H

#include "rayanmessage.h"
#include "Algorithm.h"

class IHU_Respect_Diag : public BaseAlgorithm
{
public:
	virtual bool InternalSwitchCase(CommandAdorner* Command);
	const static u16 Class_ID;
	IHU_Respect_Diag(void);
	~IHU_Respect_Diag(void);

	bool IHUDTC(CommandAdorner* Command);
	bool Keyboard_Actuator(CommandAdorner* Command);
	bool Delay_Actuator(CommandAdorner* Command);
	bool Error_Handlear();
private:
	bool delay_exitsuported(u32 delay, char* msg, bool show_timer);
	
};
#endif //__IHU_Respect_Diag_H
