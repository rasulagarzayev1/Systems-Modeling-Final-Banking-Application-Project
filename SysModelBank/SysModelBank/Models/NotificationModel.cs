namespace SysModelBank.Models
{
    public class NotificationModel
    {
        public string Type { get; private set; }
        public string Message { get; set; }

        public NotificationModel asError()
        {
            this.Type = "danger";
            return this;
        }

        public NotificationModel asSuccess()
        {
            this.Type = "success";
            return this;
        }

        public NotificationModel asInfo()
        {
            this.Type = "info";
            return this;
        }

        public NotificationModel WithMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public NotificationModel(string message)
        {
            Message = message;
        }
    }
}
