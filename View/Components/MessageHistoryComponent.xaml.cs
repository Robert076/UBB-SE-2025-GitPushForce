using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using src.Model;

namespace src.View.Components
{
    public sealed partial class MessageHistoryComponent : Page
    {
        public Message message;

        public MessageHistoryComponent()
        {
            this.InitializeComponent();
        }

        public void setMessageData(Message givenMessage)
        {
            message = givenMessage;
            MessageTypeTextBlock.Text = $"Type: {message.Type}";
            MessageTextBlock.Text = $"{message.MessageText}";
        }
    }
}
