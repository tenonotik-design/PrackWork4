using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLibrary
{
    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        System
    }

    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public NotificationType Type { get; set; } // Info, Warning, Error, System
        public DateTime CreatedAt { get; set; }
        public int Priority { get; set; } // 0 (низкий) до 5 (высокий)
        public bool IsRead { get; set; }
    }
}
