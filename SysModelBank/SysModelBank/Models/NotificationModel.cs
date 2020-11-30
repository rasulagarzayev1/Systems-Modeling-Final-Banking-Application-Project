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

        public NotificationModel WithType(NotificationType type)
        {
            this.Type = type;
            return this;
        }        
        
        public NotificationModel WithMessage(string message)
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
