#include "IHU_Respect_Diag.h" //delshadi
#include "MSG_IHU_Respect_Diag.h"
#include "ArrayTools.h"
#include "OtherHeader\IHU_Respect_Diag_OH.h"
IHU_Respect_Diag::IHU_Respect_Diag(void)
{
}
IHU_Respect_Diag::~IHU_Respect_Diag(void)
{
}
bool IHU_Respect_Diag::delay_exitsuported(u32 delay, char* msg, bool show_timer)
{
	u16 cunt = 10;
	char msg_2[255] = "";
	

	MSG_Redraw redraw = MSG_Redraw_true;
	while (delay && !Manager.BreakFlag)
	{
		Sleep(100);
		if ((msg || show_timer) && cunt == 10)
		{
			if (msg) sprintf(msg_2, "%s\n%s: [%02d : %02d]", (msg == null ? "" : msg), "Remaining Time", (delay / 60000), (delay % 60000) / 1000);
			else sprintf(msg_2, "%s: [%02d : %02d]", "Remaining Time", (delay / 60000), (delay % 60000) / 1000);
			if(!OnShowMessage(msg_2, NoneWithoutWait, false, redraw))
				return false;
			cunt = 0;
		}
		redraw = MSG_Redraw_false;
		cunt++;
		delay -= 100;
	}
	if (Manager.BreakFlag)
	{
		Manager.BreakFlag = false;
		GetLastKey();
		return false;
	}
	Sleep(delay % 100);
	return true;
}



bool IHU_Respect_Diag::Keyboard_Actuator(CommandAdorner *Command)
{
	byte response[255] = {};
	OBDii.Security_Pass(Command, 1);
	char MSG[20] = "";
	sprintf(MSG, "0 - %d", (u8)(Command->DB->FaultCodeLength));
	u8 Output = 0;
	if ((Command->DB->FaultCodeLength) == 31)
	{
		
		Output = (u8)(((GetDecimalDataDialog(MSG, 2, 31) > 31 ? 31 : GetDecimalDataDialog(MSG, 2,31)) ));
	}
	
	CommandAdorner::SendingFrame_ByteArrayFrame_structure *sendFrame = Command_SendingFrames(Command, 2);
	byte send_buff[5] = {sendFrame->ByteFrame[0], sendFrame->ByteFrame[1], sendFrame->ByteFrame[2], sendFrame->ByteFrame[3], Output};

	if ((OBDii.Send_Receive(sizeof(send_buff), send_buff, response, sizeof(response)) == null) || (response[0] == 0x7F))
	{
		Manager.BreakFlag = true;
		Command->State.IsFaulty = true;
		return true;
	}

	delay_exitsuported(5000, null, true);
	
	
	return true;
}
bool IHU_Respect_Diag::Error_Handlear(void)
{
	OnShowMessage_Messages(IHU_Respect_Diag_done,Yes_No, true); 
	Sleep(1000);
	return false;
}
bool IHU_Respect_Diag::IHUDTC(CommandAdorner* Command)
{
    byte res[1424];
	  OBDii.Security_Pass(Command, 1);
    int res_len = 0;
    memset(res, 0, sizeof(res));
    u32 code = 0;
    byte send_buff[3] = { 0x19, 0x02, 0xFF };
    FaultCodeAdorner _FaultCodeAdorner;

    res_len =  (int)OBDii.Send_Receive(sizeof(send_buff), send_buff, res, sizeof(res));

    if (res_len < 7 || res[0] == 0x7F)
    {
        Manager.BreakFlag = true;
        Command->State.IsFaulty = true;
        return true;
    }

    for (int i = 3; (i + 3) < res_len; i += 4)
    {
      code = 0;
			code |= (res[i] << 16);
			code |= (res[i + 1] << 8);
			code |= res[i + 2];

        if (code == 0xFFFF || code == 0x0000)
            continue;

        byte state = res[i + 3];

        if (_FaultCodeAdorner.CheckFault(code, state, Command))
        {
           if (((state & 0x09) == 0x09))
            {
                _FaultCodeAdorner.AddFault(code,null,GetDTCCategory(code, Command->DB->FaultCodeLength),GetDTCinCategoryMode(code,Command->DB->FaultCodeLength));
            }
        }
    }

    _FaultCodeAdorner.ShowFaults(Command, true);
    Manager.BreakFlag = true;
    return true;
}
