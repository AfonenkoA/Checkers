using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views
{
    public interface IProfileView:IView
    {
        void SetUserInfo(string nickname, string socialCredit,string lastactivity);
    }
}
