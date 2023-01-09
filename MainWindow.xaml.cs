using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Drawing;
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
using System.Collections;
using System.Windows.Media.Animation;
using System.Data.Odbc;
using System.Windows.Threading;
using System.Reflection.Emit;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        DispatcherTimer Datum = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            Datum.Tick += DatumTijd;
            Datum.Interval = new TimeSpan(0, 0, 1);
            Datum.Start();
            LblTime.Content = DateTime.Now.ToString();
            Title += DateTime.Now.ToString("dd/MM/yyyy");

            backgroundMusic.MediaEnded += new EventHandler(Media_Ended);
            backgroundMusic.Open(new System.Uri(@"..\..\music\backGround.wav", UriKind.RelativeOrAbsolute));
            backgroundMusic.Play();
            playerPlusMin.Load();
            playerKnoppen.Load();

            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            BtnRestart.IsEnabled = false;
            BtnNieuwSpel.IsEnabled = false;

            deck.Source = new BitmapImage(new Uri(@"..\..\img\deck.png", UriKind.Relative));
            WPanelDeck.Children.Add(deck);            

            LblStart.Content = "Zet in om te starten";
        }        

        MediaPlayer backgroundMusic = new MediaPlayer();
        SoundPlayer playerPlusMin = new SoundPlayer(@"..\..\music\chipSound.wav");
        SoundPlayer playerKnoppen = new SoundPlayer(@"..\..\music\buttonClick.wav");


        ArrayList cards = new ArrayList(){"2_of_clubs.png","3_of_clubs.png","4_of_clubs.png","5_of_clubs.png","6_of_clubs.png","7_of_clubs.png","8_of_clubs.png","9_of_clubs.png","10_of_clubs.png",
                          "1_ace_of_clubs.png","10_jack_of_clubs2.png","10_king_of_clubs2.png","10_queen_of_clubs2.png",
                          "2_of_diamonds.png","3_of_diamonds.png","4_of_diamonds.png","5_of_diamonds.png","6_of_diamonds.png","7_of_diamonds.png","8_of_diamonds.png","9_of_diamonds.png",
                          "10_of_diamonds.png","1_ace_of_diamonds.png","10_jack_of_diamonds2.png","10_queen_of_diamonds2.png","10_king_of_diamonds2.png",
                          "2_of_hearts.png","3_of_hearts.png","4_of_hearts.png","5_of_hearts.png","6_of_hearts.png","7_of_hearts.png","8_of_hearts.png","9_of_hearts.png","10_of_hearts.png",
                          "1_ace_of_hearts.png","10_jack_of_hearts2.png","10_king_of_hearts2.png","10_queen_of_hearts2.png",
                          "2_of_spades.png","3_of_spades.png","4_of_spades.png","5_of_spades.png","6_of_spades.png","7_of_spades.png","8_of_spades.png","9_of_spades.png","10_of_spades.png",
                          "1_ace_of_spades2.png","10_jack_of_spades2.png","10_king_of_spades2.png","10_queen_of_spades2.png"};

        Image deck = new Image();

        Image spFoto1 = new Image();
        Image spFoto2 = new Image();
        Image spFoto3 = new Image();
        Image spFoto4 = new Image();
        Image spFoto5 = new Image();

        Image comFoto1 = new Image();
        Image comFoto2 = new Image();
        Image comFoto3 = new Image();
        Image comFoto4 = new Image();
        Image comFoto5 = new Image();

        string spCard1;
        string spCard2;
        string spCard3;
        string spCard4;
        string spCard5;

        string comCard1;
        string comCard2;
        string comCard3;
        string comCard4;
        string comCard5;

        int spCard1Num;
        int spCard2Num;
        int spCard3Num;
        int spCard4Num;
        int spCard5Num;

        int comCard1Num;
        int comCard2Num;
        int comCard3Num;
        int comCard4Num;
        int comCard5Num;

        int spCounter = 0;
        int comCounter = 0;
        
        int index;        

        int spelerTotaal = 0;
        int computerTotaal = 0;

        Random random = new Random();

        int kapitaal = 100;
        int inzet= 0;

        int cardTotal = 52;
        
        Window1 window1 = new Window1();

        private void Media_Ended(object sender, EventArgs e)
        {
            backgroundMusic.Position = TimeSpan.Zero;
            backgroundMusic.Play();
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {          

            playerPlusMin.Play();

            LblStart.Visibility= Visibility.Collapsed;

            BtnDeel.IsEnabled = true;

            if (!(kapitaal <= 0))
            {
                inzet += 10;
                TxtInzet.Text = inzet.ToString();
                kapitaal -= 10;
                TxtKapitaal.Text = kapitaal.ToString();
            }           
            
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {           

            playerPlusMin.Play();

            if (!(kapitaal <= 0))
            inzet -= 10;
            TxtInzet.Text = inzet.ToString();
            kapitaal += 10;
            TxtKapitaal.Text = kapitaal.ToString();

        }

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {

            playerKnoppen.Play();

            BtnPlus.IsEnabled = false;
            BtnMin.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            

            int index = random.Next(cards.Count);
            spCard1 = (string)cards[index];
            spFoto1.Source = new BitmapImage(new Uri(@"..\..\img\" + spCard1, UriKind.Relative));
            SpelerWrap.Children.Add(spFoto1); 
            spCard1Num = int.Parse(spCard1.Split('_')[0]);

            cards.Remove(spCard1);
            cardTotal -= 1;
            ShowDeckSize();


            index = random.Next(cards.Count);
            spCard2 = (string)cards[index];
            spFoto2.Source = new BitmapImage(new Uri(@"..\..\img\" + spCard2, UriKind.Relative));
            SpelerWrap.Children.Add(spFoto2);
            spCard2Num = int.Parse(spCard2.Split('_')[0]);

            cards.Remove(spCard2);
            cardTotal -= 1;
            ShowDeckSize();


            comFoto1.Source = new BitmapImage(new Uri(@"..\..\img\ccard.png", UriKind.Relative));
            ComputerWrap.Children.Add(comFoto1);

            index = random.Next(cards.Count);
            comCard2 = (string)cards[index];
            comFoto2.Source = new BitmapImage(new Uri(@"..\..\img\" + comCard2, UriKind.Relative));
            ComputerWrap.Children.Add(comFoto2);
            comCard2Num = int.Parse(comCard2.Split('_')[0]);

            cards.Remove(comCard2);
            cardTotal -= 1;
            ShowDeckSize();

            spelerTotaal = spCard1Num + spCard2Num;
            computerTotaal = comCard2Num;

            ShowTotaal();

            if (spelerTotaal == 21)
            {
                ShowWin();

                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

            spCounter = 2;
            comCounter = 2;

            LblDeck.Content = (cards.Count) - 1;

        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            playerKnoppen.Play();

            spCounter++;

            if (spCounter == 3)
            {
                int index = random.Next(cards.Count);
                spCard3 = (string)cards[index];
                spFoto3.Source = new BitmapImage(new Uri(@"..\..\img\" + spCard3, UriKind.Relative));
                SpelerWrap.Children.Add(spFoto3);
                spCard3Num = int.Parse(spCard3.Split('_')[0]);

                cards.Remove(spCard3);
                cardTotal -= 2;
                ShowDeckSize();


                spelerTotaal += spCard3Num;

                if (spelerTotaal == 21)
                {
                    ShowWin();

                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                }

                if (spelerTotaal > 21)
                {
                    ShowDefeat();

                    BtnHit.IsEnabled = false;
                }

            }

            if (spCounter == 4)
            {
                index = random.Next(cards.Count); // nieuwe kaart voor speler
                spCard4 = (string)cards[index];
                spFoto4.Source = new BitmapImage(new Uri(@"..\..\img\" + spCard4, UriKind.Relative));
                SpelerWrap.Children.Add(spFoto4);
                spCard4Num = int.Parse(spCard4.Split('_')[0]);

                cards.Remove(spCard4);
                cardTotal -= 1;
                ShowDeckSize();


                spelerTotaal += spCard4Num;

                if (spelerTotaal == 21)
                {
                    ShowWin();

                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                }

                if (spelerTotaal > 21)
                {
                    ShowDefeat();

                    BtnHit.IsEnabled = false;
                }
            }

            if (spCounter == 5)
            {
                index = random.Next(cards.Count); // nieuwe kaart voor speler
                spCard5 = (string)cards[index];
                spFoto5.Source = new BitmapImage(new Uri(@"..\..\img\" + spCard5, UriKind.Relative));
                SpelerWrap.Children.Add(spFoto5);
                spCard5Num = int.Parse(spCard5.Split('_')[0]);

                cards.Remove(spCard5);
                cardTotal -= 1;
                ShowDeckSize();


                spelerTotaal += spCard5Num;

                if (spelerTotaal == 21)
                {
                    ShowWin();

                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                }

                if (spelerTotaal > 21)
                {
                    ShowDefeat();

                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                }
            }

            ShowTotaal();

            if (cards.Count < 4)
            {
                BtnDeel.IsEnabled = false;
                BtnNieuwSpel.IsEnabled = false;
                BtnRestart.IsEnabled = true;


                if (MessageBox.Show("Er zijn niet genoeg kaarten. Klik op YES voor een nieuwe spel.", "ATTENTIE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();

                }
                else
                {
                    this.Close();
                }
            }
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            playerKnoppen.Play();

            BtnNieuwSpel.IsEnabled = true;

            index = random.Next(cards.Count);
            comCard1 = (string)cards[index];
            comFoto1.Source = new BitmapImage(new Uri(@"..\..\img\" + comCard1, UriKind.Relative));
            comCard1Num = int.Parse(comCard1.Split('_')[0]);

            cards.Remove(comCard1);

            if(spCounter < 3)
            {
                cardTotal -= 1;
            }
            ShowDeckSize();


            computerTotaal += comCard1Num;

            comCounter++;

            if (computerTotaal <= 16)
            {
                int index = random.Next(cards.Count);
                comCard3 = (string)cards[index];
                comFoto3.Source = new BitmapImage(new Uri(@"..\..\img\" + comCard3, UriKind.Relative));
                ComputerWrap.Children.Add(comFoto3);
                comCard3Num = int.Parse(comCard3.Split('_')[0]);

                cards.Remove(comCard3);
                cardTotal -= 1;
                ShowDeckSize();


                computerTotaal += comCard3Num;

            }

            if (computerTotaal <= 16)
            {
                int index = random.Next(cards.Count);
                comCard4 = (string)cards[index];
                comFoto4.Source = new BitmapImage(new Uri(@"..\..\img\" + comCard4, UriKind.Relative));
                ComputerWrap.Children.Add(comFoto4);
                comCard4Num = int.Parse(comCard4.Split('_')[0]);

                cards.Remove(comCard4);
                cardTotal -= 1;
                ShowDeckSize();


                computerTotaal += comCard4Num;

            }

            if (computerTotaal <= 16)
            {
                int index = random.Next(cards.Count);
                comCard5 = (string)cards[index];
                comFoto5.Source = new BitmapImage(new Uri(@"..\..\img\" + comCard5, UriKind.Relative));
                ComputerWrap.Children.Add(comFoto5);
                comCard5Num = int.Parse(comCard5.Split('_')[0]);

                cards.Remove(comCard5);
                cardTotal -= 1;
                ShowDeckSize();


                computerTotaal += comCard5Num;

            }
            CheckResult();
            ShowLastResult();

        }

        private void BtnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {

            playerKnoppen.Play();

            BtnHit.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnPlus.IsEnabled = true;
            BtnMin.IsEnabled = true;            

            ComputerWrap.Children.Clear();
            SpelerWrap.Children.Clear();


            if (kapitaal == 0)
            {
                BtnDeel.IsEnabled = false;
                BtnNieuwSpel.IsEnabled = false;
                BtnRestart.IsEnabled = true;
                

                if (MessageBox.Show("Uw kapitaal is op. Klik op YES voor een nieuwe spel.", "ATTENTIE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();

                }
                else
                {
                    this.Close();
                }
            }

            if (cards.Count < 4)
            {
                BtnDeel.IsEnabled = false;
                BtnNieuwSpel.IsEnabled = false;
                BtnRestart.IsEnabled = true;


                if (MessageBox.Show("Er zijn niet genoeg kaarten. Klik op YES voor een nieuwe spel.", "ATTENTIE", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();

                }
                else
                {
                    this.Close();
                }
            }


            TxtSpelerTotaal.Text = "0";
            TxtComputerTotaal.Text = "0";
            LblVerloren.Content = "";
            kapitaal = 100;
            inzet = 0;

        }

        private void ShowTotaal()
        {
            TxtSpelerTotaal.Text = Convert.ToString(spelerTotaal);
            TxtComputerTotaal.Text = Convert.ToString(computerTotaal);

        } // Toon totaal punten

        private void ShowWin()
        {
            LblVerloren.Content = "GEWONNEN";

            int inzetGeld = int.Parse(TxtInzet.Text) * 2;
            kapitaal += inzetGeld;
            TxtKapitaal.Text = Convert.ToString(kapitaal);
            TxtInzet.Text = 0.ToString();

            BtnNieuwSpel.IsEnabled = true;

        }

        private void ShowDefeat()
        {
            LblVerloren.Content = "VERLOREN";

          
            TxtInzet.Text = 0.ToString();

            BtnStand.IsEnabled = false;
        }

        private void ShowDraw()
        {
            LblVerloren.Content = "GELIJKSPEL";

            int inzetGeld = int.Parse(TxtInzet.Text);
            kapitaal += inzetGeld;
            TxtKapitaal.Text = Convert.ToString(kapitaal);
            TxtInzet.Text = 0.ToString();
        }

        private void CheckResult()
        {
            if (computerTotaal == 21)
            {
                ShowDefeat();

                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

            if (computerTotaal > 21)
            {
                ShowWin();

                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

            if (computerTotaal > spelerTotaal && computerTotaal < 21)
            {
                ShowDefeat();

                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

            if (spelerTotaal > computerTotaal && spelerTotaal <= 21)
            {
                ShowWin();

                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

            if (spelerTotaal == computerTotaal && spelerTotaal <= 21 && computerTotaal <= 21)
            {
                ShowDraw();

                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
            }

            ShowTotaal();
        } // Resultaat 

        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void ShowDeckSize() // Overgeblijven kaarten
        {
            
            LblDeck.Content = cardTotal.ToString();
        }
        
        private void DatumTijd(object sender, EventArgs e)
        {
            LblTime.Content= DateTime.Now.ToString();
        }

        private async void Blink()
        {
            while (true)
            {
                await Task.Delay(500);
                LblStart.Background = LblStart.Background == Brushes.Black ? Brushes.White : Brushes.Black;
                LblStart.Foreground = LblStart.Foreground == Brushes.Black ? Brushes.Black : Brushes.Black;
            }
        } // Label "Zet in om te starten"
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Blink();
        } // Label "Zet in om te starten"

        private void ShowLastResult()
        {
            if(LblVerloren.Content == "GEWONNEN")
            {
                LblLastResult.Content = "Speler: " + TxtSpelerTotaal.Text + " Computer: " + TxtComputerTotaal.Text + " GEWONNEN";
                window1.Lbl1.Content += "Speler: " + TxtSpelerTotaal.Text + " Computer: " + TxtComputerTotaal.Text + " GEWONNEN \n"; 

            }
            if(LblVerloren.Content == "VERLOREN")
            {
                LblLastResult.Content = "Speler: " + TxtSpelerTotaal.Text + " Computer: " + TxtComputerTotaal.Text + " VERLOREN";
                window1.Lbl1.Content += "Speler: " + TxtSpelerTotaal.Text + " Computer: " + TxtComputerTotaal.Text + " VERLOREN \n";
            }
            if(LblVerloren.Content == "GELIJKSPEL")
            {
                LblLastResult.Content = "Speler: " + TxtSpelerTotaal.Text + " Computer: " + TxtComputerTotaal.Text + " GELIJKSPEL";
                window1.Lbl1.Content += "Speler: " + TxtSpelerTotaal.Text + " Computer: " + TxtComputerTotaal.Text + " GELIJKSPEL \n";

            }
            
        }

        private void SButton_Click(object sender, RoutedEventArgs e) // Window statistieken
        {           
            window1.Show();
            window1.Height = 500;
            window1.Width = 400;
        }
  
    }


}
