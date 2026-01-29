using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWPFGame;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
            Image player = new Image
            {
                Width = 100,
                Height = 100,
                Source = new BitmapImage(new Uri("pack://application:,,,/Image/player.png", UriKind.Absolute))
            };
        GameCanvas.Children.Add(player);
        Canvas.SetLeft(player, 100);
        Canvas.SetTop(player, 100);

    }
}