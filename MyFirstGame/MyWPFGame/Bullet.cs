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
namespace MyWPFGame
{
    public class Bullet
    {
        public Image? Sprite { get; set; }
        /* private double _speed; 
        public double Speed 
        { 
            get { return _speed; } 
            set { _speed = value; } 
        } 
        getter setters, encryption.
        12 is the default speed of the bullet */
        public double Speed { get; set; } = 12;
        public double X { get; set; } = 0; 
        public double Y { get; set; } = 0;

    }
}