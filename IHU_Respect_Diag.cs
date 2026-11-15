using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using USILogicLayer.Protocol;
using Extensions.ByteArrayExtensions;
using DataAccessLayer;
using System.Threading;
using System.IO;
using Extensions.Loggers;
using USILogicLayer.Algorithms.Algorithm_Forms;
using System.Diagnostics;
using USILogicLayer.Algorithms;


namespace USILogicLayer.Algorithms
{
    public class IHU_Respect_Diag : Algorithm
    {
        public IHU_Respect_Diag(RayanDataModelDataSet.ECURow ECU, LogicManager logicManager)
                                                                            : base(ECU, logicManager)
        {
            Messages["IHU_Respect_Diag_AreYouSure", Language.English] = "Are you sure about performing the operation?";
            Messages["IHU_Respect_Diag_AreYouSure", Language.Farsi] = "آیا از انجام عملیات اطمینان دارید؟";
                         
            Messages["IHU_Respect_Diag_Canceled", Language.English] = "Operation canceled by user.";
            Messages["IHU_Respect_Diag_Canceled", Language.Farsi] = "عملیات توسط کاربر لغو شد";
                         
            Messages["IHU_Respect_Diag_Digit", Language.English] = " digit ";
            Messages["IHU_Respect_Diag_Digit", Language.Farsi] = " رقم ";
                         
            Messages["IHU_Respect_Diag_lessAllowedLimit", Language.English] = "The entered values are less than the allowed limit.";
            Messages["IHU_Respect_Diag_lessAllowedLimit", Language.Farsi] = "مقادیر وارد شده کمتر از حد مجاز است.";
                         
            Messages["IHU_Respect_Diag_EnterDate", Language.English] = " EnterDate ";
            Messages["IHU_Respect_Diag_EnterDate", Language.Farsi] = " تاریخ را وارد کنید ";
                         
            Messages["IHU_Respect_Diag_done", Language.English] = " Operation done ";
            Messages["IHU_Respect_Diag_done", Language.Farsi] = " مقادیر وارد شده به درستی ثبت شد ";

            Messages["IHU_Respect_Diag_Info", Language.English] = "Any action that causes a change in the crankshaft sensor position requires this operation.";
            Messages["IHU_Respect_Diag_Info", Language.Farsi] = "هر اقدامی که باعث تغییر موقعیت سنسور میل‌لنگ شود نیاز به انجام این عملیات دارد.";

            Messages["IHU_Respect_Diag_Warning", Language.English] = "This operation is required after: Camshaft sensor replacement, Engine speed sensor replacement, Crankshaft or camshaft replacement, Timing belt replacement, Timing adjustment, ECU replacement.";
            Messages["IHU_Respect_Diag_Warning", Language.Farsi] = "انجام این عملیات پس از موارد زیر الزامی است: تعویض سنسور میل بادامک، تعویض سنسور دور موتور، تعویض میل لنگ یا میل بادامک، تعویض تسمه تایم، تنظیم زمانبندی، تعویض ایسیو.";

            Messages["IHU_Respect_Diag_Condition", Language.English] = "VVT position setup conditions: First warm up the engine, turn the ignition off, then turn the ignition on.";
            Messages["IHU_Respect_Diag_Condition", Language.Farsi] = "شرایط تعریف موقعیت شیر VVT: ابتدا موتور را گرم کرده سوییچ را بسته و سپس سوییچ باز باشد.";

            Messages["IHU_Respect_Diag_Failed", Language.English] = "Operation failed";
            Messages["IHU_Respect_Diag_Failed", Language.Farsi] = "عملیات انجام نشد";

            Messages["IHU_Respect_Diag_Condition1", Language.English] = " After installing the new VGT,turn thr ignition ON.The engine must be stationary and vehicle speed must be 0.";
            Messages["IHU_Respect_Diag_Condition1", Language.Farsi] = " بعد از تعویض VGT موتوردر حالت ایستا باشد و سرعت صفر باشد .";

            Messages["IHU_Respect_Diag_Confirm", Language.English] = "Turn the ignition OFF for 5 seconds, then turn it ON. Press ENTER to continue.";
            Messages["IHU_Respect_Diag_Confirm", Language.Farsi] = "سوییچ را 5 ثانیه ببندید و سوییچ را باز کنید و برای ادامه اینتر را بزنید.";

            Messages["IHU_Respect_Diag_Success", Language.English] = "Definition completed successfully. Turn OFF the VGT, wait 1 minute, then turn it ON again.";
            Messages["IHU_Respect_Diag_Success", Language.Farsi] = "تعریف با موفقیت انجام شد. VGT را خاموش کرده و یک دقیقه صبر کنید و مجدد روشن کنید.";

            Messages["IHU_Respect_Diag_InputRange", Language.English] = "Enter a value in the range of 0 to 99.";
            Messages["IHU_Respect_Diag_InputRange", Language.Farsi] = "مقدار را در بازه 0 تا 99 وارد کنید.";

            Messages["IHU_Respect_Diag_InputRange1", Language.English] = "Enter a value in the range of 0 to 106.";
            Messages["IHU_Respect_Diag_InputRange1", Language.Farsi] = "مقدار را در بازه 0 تا 106 وارد کنید.";

            Messages["IHU_Respect_Diag_InputRange2", Language.English] = "Enter a value in the range of 0 to 2550.";
            Messages["IHU_Respect_Diag_InputRange2", Language.Farsi] = "مقدار را در بازه 0 تا 2550 وارد کنید.";

            Messages["IHU_Respect_Diag_Successful", Language.English] = "Operation completed successfully.";
            Messages["IHU_Respect_Diag_Successful", Language.Farsi] = "عملیات با موفقیت انجام شد.";

            Messages["IHU_Respect_Diag_Condition2", Language.English] = "Turn the switch on, start the engine, and ensure the speed is 0 in stable condition.";
            Messages["IHU_Respect_Diag_Condition2", Language.Farsi] = "سوئیچ را باز کرده و موتور روشن باشد و در حالت ثابت سرعت 0 باشد.";


        }
    }
}