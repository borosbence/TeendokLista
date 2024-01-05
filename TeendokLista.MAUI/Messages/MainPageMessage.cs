using CommunityToolkit.Mvvm.Messaging.Messages;
using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Messages
{
    public enum ListAction
    {
        Add,
        Delete
    }

    public class FeladatModelMessage
    {
        public FeladatModelMessage(FeladatModel item, ListAction action = ListAction.Add)
        {
            Item = item;
            Action = action;
        }
        public FeladatModel Item { get; set; }
        public ListAction Action { get; set; }
    }

    public class MainPageMessage : ValueChangedMessage<FeladatModelMessage>
    {
        public MainPageMessage(FeladatModelMessage value) : base(value)
        {
        }
    }
}
