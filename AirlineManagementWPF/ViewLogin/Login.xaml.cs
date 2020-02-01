using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AirlineManagement;

namespace AirlineManagementWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private FlyingCenterSystem FCS;
        private BorderBackGround myBorderBackGround;
        private BorderBackGround myBorderBackGroundRed;
        private BorderBackGround myBorderBackGroundGreen;
        private FlightsList flightList;
        private BuyTicket buyTicket;
        private DBUpdate dbUpdate;

        private ILoginToken loginToken =null;

        public LoginView()
        {

            InitializeComponent();
        
              FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            myBorderBackGround = new BorderBackGround { ColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("White")) };
            myBorderBackGroundRed = new BorderBackGround { ColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("Red")) };
            myBorderBackGroundGreen = new BorderBackGround { ColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("Green")) };
            
            this.Border.DataContext = myBorderBackGround;
            useNameTxt.Text = "admin";
            passwordTxt.Text = "9999";
        }

     
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginToken = FCS.Login(useNameTxt.Text, passwordTxt.Text);
            if (loginToken == null)
            {
                Border.DataContext = myBorderBackGroundRed;
            }
            else
            {
               
                LoginToken<AirlineCompany> airlineCompanyToken = loginToken as LoginToken<AirlineCompany>;
                if(airlineCompanyToken != null)
                {
                    Border.DataContext = myBorderBackGroundGreen;
                    flightList = new FlightsList(loginToken);
                    flightList.Show();

                    // Hide the MainWindow until later
                    //this.Hide();
                    this.Close();
                }
               
                LoginToken<Customer> customerToken = loginToken as LoginToken<Customer>;
                if (customerToken != null)
                {
                    buyTicket = new BuyTicket(loginToken);
                   
                    buyTicket.Show();
                }
                LoginToken<Administrator> adminToken = loginToken as LoginToken<Administrator>;
                if (adminToken != null)
                {
                    dbUpdate = new DBUpdate(loginToken);

                    dbUpdate.Show();
                }
            }
            
        }
        public class BorderBackGround
        {
            public SolidColorBrush ColorBrush { get; set; }

            
        }
    }
}
