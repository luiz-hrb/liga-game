using UnityEngine;

namespace LigaGame.UI
{
    public class NotificationHolder : MonoBehaviour
    {
        public void Message(string message)
        {
            Notification.Instance.Message(message);
        }
    }
}
