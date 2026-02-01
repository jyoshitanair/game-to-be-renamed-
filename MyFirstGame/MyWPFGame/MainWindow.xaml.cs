using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWPFGame{
public partial class MainWindow : Window
{
    private Image player;
    private Image enemy;
    private double PlayerX = 100; 
    private double PlayerY = 100;
    private double EnemyX = 300;
    private double EnemyY = 100;
    private const double PlayerSpeed = 100;
    private const double EnemySpeed = 10;
    private double betweenX;
    private double betweenY;
    private double distanceFormula;
    private DateTime lastTime;
    private double secondTime;  
    public MainWindow()
    {
        InitializeComponent();
            player = new Image
            {
                Width = 700,
                Height = 700,
                Source = new BitmapImage(new Uri("pack://application:,,,/Image/player.png", UriKind.Absolute))
            };
            enemy = new Image
            {
                Width = 700,
                Height = 700,
                Source = new BitmapImage(new Uri("pack://application:,,,/Image/enemy.png", UriKind.Absolute))
            };
        GameCanvas.Children.Add(player);
        GameCanvas.Children.Add(enemy);
        lastTime = DateTime.Now; 
        Positions(); 
        CompositionTarget.Rendering += GameLoop;
        this.KeyDown += OnKeyDown;
    }
    private void Positions()
    {
        Canvas.SetLeft(enemy, EnemyX);
        Canvas.SetTop(enemy, EnemyY);

        Canvas.SetLeft(player, PlayerX);
        Canvas.SetTop(player, PlayerY);
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        secondTime = time();
        switch(e.Key){
            case Key.Up:
                PlayerY -= PlayerSpeed * secondTime;
                break;
            case Key.Down:
                PlayerY += PlayerSpeed * secondTime;
                break;
            case Key.Left:
                PlayerX -= PlayerSpeed * secondTime;
                break;
            case Key.Right:
                PlayerX += PlayerSpeed * secondTime;
                break;
        }
        Positions(); 
    }
    private void GameLoop(object sender, EventArgs e){
       secondTime = time();
       betweenX = PlayerX - EnemyX;
       betweenY = PlayerY - EnemyY;
       distanceFormula = Math.Sqrt(betweenX * betweenX + betweenY * betweenY);
       if (distanceFormula >= EnemySpeed)
        {
            EnemyX += (betweenX / distanceFormula) * EnemySpeed * secondTime;
            EnemyY += (betweenY / distanceFormula) * EnemySpeed * secondTime;
        }
        Positions();
        if(distanceFormula < 20)
        {
            CompositionTarget.Rendering -= GameLoop;
            MessageBox.Show("Game Over!");
        } 
    }
    private double time(){
        DateTime now = DateTime.Now;
        double time = (now - lastTime).TotalSeconds;
        lastTime = now;
        return time;
    }  
}
}