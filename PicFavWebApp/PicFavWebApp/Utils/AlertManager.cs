using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XiGp.Server.Web.AdminPortal.Util
{
    public static class AlertManager
    {
        // sets string value into TempData to have opportiunity show it on any view
        public static void PushAlert(this Controller instance ,AlertType type, string message)
        {
            switch (type)
            {
                    case AlertType.Success:
                    instance.TempData["Success"] = message;
                    break;
                    case AlertType.Warning: instance.TempData["Warning"] = message;
                    break;
                    case AlertType.Danger: instance.TempData["Danger"] = message;
                    break;
            }
        }
    }

    public enum AlertType
    {
        Success,
        Warning,
        Danger
    }
}
