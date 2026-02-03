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
using System.Windows.Automation.Text;

namespace MyWPFGame{
public partial class MainWindow : Window
{
    private Image player;
    private Image enemy;
    private Image? carrot; 
    private double PlayerDirX = 0;
    private double PlayerDirY = 0;
    private double PlayerX = 100; 
    private double PlayerY = 100;
    private double EnemyX = 300;
    private double EnemyY = 100;
    private const double PlayerSpeed = 100;
    private const double EnemySpeed = 50;
    private double betweenX;
    private double betweenY;
    private double distanceFormula;
    private DateTime lastTime;
    private double secondTime;  
    List<Bullet> bullets; 
    public MainWindow()
    {
        InitializeComponent();
        bullets = new List<Bullet>();
            player = new Image
            {
                Width = 100,
                Height = 100,
                Source = new BitmapImage(new Uri("pack://application:,,,/Image/player.png", UriKind.Absolute))
            };
            enemy = new Image
            {
                Width = 50,
                Height = 50,
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
        double rotationAngle = 0;
        switch(e.Key){
            case Key.Up:
                PlayerY -= PlayerSpeed ;
                rotationAngle = 0;
                PlayerDirX = 0;
                PlayerDirY = -1;
                break;
            case Key.Down:
                PlayerY += PlayerSpeed;
                rotationAngle = 180;
                PlayerDirX = 0;
                PlayerDirY = 1;
                break;
            case Key.Left:
                PlayerX -= PlayerSpeed;
                rotationAngle = 270;
                PlayerDirX = -1;
                PlayerDirY = 0;
                break;
            case Key.Right:
                PlayerX += PlayerSpeed;
                rotationAngle = 90;
                PlayerDirX = 1;
                PlayerDirY = 0;
                break;
            case Key.Space:
                Shoot();
                break;
        }
        Positions(); 
        RotatePlayer(rotationAngle);
    }
    private void GameLoop(object? sender, EventArgs? e)
    {
    // Enemy Movement
       secondTime = time();
       betweenX = PlayerX - EnemyX;
       betweenY = PlayerY - EnemyY;
       distanceFormula = Math.Sqrt(betweenX * betweenX + betweenY * betweenY);
       if (distanceFormula > 20)
        {
            EnemyX += (betweenX / distanceFormula) * EnemySpeed * secondTime;
            EnemyY += (betweenY / distanceFormula) * EnemySpeed * secondTime;
        }
        Positions();
        if(distanceFormula < 20)
        {
            CompositionTarget.Rendering -= GameLoop;
            MessageBox.Show("Game Over!");
            return; 
        } 
    // Bullet Movement
        for(int i = bullets.Count - 1; i>= 0; i--){
            Bullet b = bullets[i];
            double curTop = Canvas.GetTop(b.Sprite);
            double curLeft = Canvas.GetLeft(b.Sprite);
           Canvas.SetLeft(b.Sprite, curLeft +(b.X * b.Speed * secondTime)); // x axis position + (direction x speed x time)
           Canvas.SetTop(b.Sprite, curTop + (b.Y * b.Speed * secondTime)); // y axis position + (direction x speed x time)
        }
    }
    private double time(){
        DateTime now = DateTime.Now;
        double time = (now - lastTime).TotalSeconds;
        lastTime = now;
        return time;
    }  
    private void Shoot(){
        carrot = new Image{
            Width = 10,
            Height = 10,
            Source = new BitmapImage(new Uri("pack://application:,,,/Image/carrot.png", UriKind.Absolute))
        };
        GameCanvas.Children.Add(carrot);
        Canvas.SetLeft(carrot, PlayerX +45);
        Canvas.SetTop(carrot, PlayerY +45);
        bullets.Add(new Bullet {
            Sprite = carrot,
            Speed = 400,
            X = PlayerDirX,
            Y = PlayerDirY
        });
    }
    private void RotatePlayer (double angle){
        RotateTransform rotateTransform = new RotateTransform(angle);
        player.RenderTransform = rotateTransform;
    }
}
}