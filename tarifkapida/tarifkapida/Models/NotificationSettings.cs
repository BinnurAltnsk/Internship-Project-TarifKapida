using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models
{
    public class NotificationSettings
    {
        [Key]
        public int NotificationSettingsId { get; set; }
        public bool EmailNotifications { get; set; }
        public bool PushNotifications { get; set; }
        public bool SMSNotifications { get; set; }
        public bool NewsletterSubscription { get; set; }
        public bool PromotionalOffers { get; set; }
        public NotificationSettings()
        {
            EmailNotifications = true;
            PushNotifications = true;
            SMSNotifications = false;
            NewsletterSubscription = true;
            PromotionalOffers = false;
        }
    }
}
