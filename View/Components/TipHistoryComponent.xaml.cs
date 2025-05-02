using Microsoft.UI.Xaml.Controls;
using src.Model;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace src.View.Components
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TipHistoryComponent : Page
    {
        public Tip tip;

        public TipHistoryComponent()
        {
            this.InitializeComponent();
        }
        public void setTipData(Tip givenTip)
        {
            tip = givenTip;
            TipTextBlock.Text = $"{tip.TipText}";
        }
    }
}
