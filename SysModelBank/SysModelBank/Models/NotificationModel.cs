using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysModelBank.Models
{
    public enum NotificationType
    {
        danger,
        success,
        info
    }

    public class NotificationModel
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }

        public NotificationModel withType(NotificationType type)
        {
            this.Type = type;
            return this;
        }        
        
        public NotificationModel withMessage(string message)
        {
            this.Message = message;
            return this;
        }
        public NotificationModel(NotificationType type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
