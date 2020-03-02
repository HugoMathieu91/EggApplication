using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
//pr utiliser les threads
using System.Threading;


namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour home.xaml
    /// </summary>
    public partial class home : Page
    {
        Recipe oeufDur = new Recipe("Oeuf dur", "Facile à réaliser", 5);
        Recipe oeufCoque = new Recipe("Oeuf à la coque", "Délicieux et rapide", 3);
        Recipe oeufMollet = new Recipe("Oeuf mollet", "Difficile à faire", 9);
        Recipe oeufAutruche = new Recipe("Oeuf d'autruche", "Pour ceux qui ont faim", 45);
       
        RecipeCollection EggRecipes = new RecipeCollection();

       /*DispatcherTimer _timer1;
        DispatcherTimer _timer2;
        DispatcherTimer _timer3;
        DispatcherTimer _timer4;
        TimeSpan _time1;
        TimeSpan _time2;
        TimeSpan _time3;
        TimeSpan _time4;*/

       // string threadMsg; 

        int i;
        

        public home()
        {
            //Pour le timer 
            InitializeComponent();

            EggRecipes.StartCollection();
            //On ajoute les recettes à la collection
            EggRecipes.AddMadeRecipe(oeufDur);
            EggRecipes.AddMadeRecipe(oeufAutruche);
            EggRecipes.AddMadeRecipe(oeufMollet);
            EggRecipes.AddMadeRecipe(oeufCoque);
           
           
            //On rempli la listBox avec notre collection
            for(i = 0; i < EggRecipes.Recipes.Count; i++)
            {
                recipesListBox.Items.Add(EggRecipes.Recipes[i].Title);  
            }
           
        }

        void block1Write(int time, string title)
        {
            timerBlock1.Text = time.ToString() + " - " + title;
        }

        void block2Write(int time, string title)
        {
            timerBlock2.Text = time.ToString() + " - " + title ;  
        }

        void block3Write(int time, string title)
        {
            timerBlock3.Text = time.ToString() + " - " + title;
        }

        void block4Write(int time, string title)
        {
            timerBlock4.Text = time.ToString() + " - " + title ;
        }



        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            
            // Retourne erreur "ne peut pas accéder car thread propriétaire"
            //timerBlock1.Text = e.Message; 
            //Même erreur 
            //WriteDelegate writeTimerBlock = block1Write;
            //writeTimerBlock.Invoke(e.Message);  

        
            timerBlock1.Dispatcher.Invoke(() => block1Write(e.time, e.message));
        }

        void HandleCustomEvent2(object sender, CustomEventArgs e)
        {
            timerBlock2.Dispatcher.Invoke(() => block2Write(e.time, e.message)); 
        }

        void HandleCustomEvent3(object sender, CustomEventArgs e )
        {
            timerBlock3.Dispatcher.Invoke(() => block3Write(e.time, e.message));  
        }

        void HandleCustomEvent4(object sender, CustomEventArgs e )
        {
            timerBlock4.Dispatcher.Invoke(() => block4Write(e.time, e.message)); 
        }
        //Lancer le chrono
        public void LaunchTimer(object sender, RoutedEventArgs e)
        {
            //Si aucune recette selectionnée
            if (recipesListBox.SelectedIndex <0)
            {
                warningBlock.Background = Brushes.AliceBlue; 
                warningBlock.Text = "Selectionnez une recette";
                return;
            }
            warningBlock.Background = Brushes.Transparent; 
            warningBlock.Text = ""; 
            
            string recipeTile = EggRecipes.Recipes[recipesListBox.SelectedIndex].Title;
            

            if (string.IsNullOrWhiteSpace(timerBlock1.Text) )
            {
                //Lancer le thread
                EggRecipes.Recipes[recipesListBox.SelectedIndex].Onprogress();

                //S'abonner a l'evenement 
               
                    EggRecipes.Recipes[recipesListBox.SelectedIndex].RaiseCustomEvent += HandleCustomEvent;
                

                return; 
             


                /* _time1 = TimeSpan.FromSeconds(EggRecipes.Recipes[recipesListBox.SelectedIndex].Timer * 60);
                  _timer1 = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                  {
                      timerBlock1.Text = _time1.ToString("c") + ": " + recipeTile;
                      if (_time1 == TimeSpan.Zero) _timer1.Stop();
                      _time1 = _time1.Add(TimeSpan.FromSeconds(-1));
                  }, Application.Current.Dispatcher);
                  _timer1.Start();
                  return; */
            }

            if (string.IsNullOrWhiteSpace(timerBlock2.Text))
            {
                EggRecipes.Recipes[recipesListBox.SelectedIndex].Onprogress();
                EggRecipes.Recipes[recipesListBox.SelectedIndex].RaiseCustomEvent += HandleCustomEvent2;
               
                return;
            }

            if (string.IsNullOrWhiteSpace(timerBlock3.Text))
            {
                EggRecipes.Recipes[recipesListBox.SelectedIndex].Onprogress();
                EggRecipes.Recipes[recipesListBox.SelectedIndex].RaiseCustomEvent += HandleCustomEvent3;

                return;
            }

            if (string.IsNullOrWhiteSpace(timerBlock4.Text))
            {
                EggRecipes.Recipes[recipesListBox.SelectedIndex].Onprogress();
                EggRecipes.Recipes[recipesListBox.SelectedIndex].RaiseCustomEvent += HandleCustomEvent4;

                return;           
            }
        }

        //Afficher infos sur les recettes dans la textBox
        private void GetInformations(object sender, MouseButtonEventArgs e)
        {
            if (recipesListBox.SelectedIndex < 0)
            {
                descriptionTextBox.Text = "Selectionnez une recette";
            }

            descriptionTextBox.Text = EggRecipes.Recipes[recipesListBox.SelectedIndex].Content + "\n Temps de préparation: " + EggRecipes.Recipes[recipesListBox.SelectedIndex].Timer + " minutes"; 
        }

        //Reset le chrono
        private void ResetTimer1(object sender, RoutedEventArgs e)
        {


                timerBlock1.Text = " "; 
        }

        private void ResetTimer2(object sender, RoutedEventArgs e)
        {
                timerBlock2.Text = " ";
        }

        private void ResetTimer3(object sender, RoutedEventArgs e)
        {   
                
                timerBlock3.Text = " "; 
        }

        private void ResetTimer4(object sender, RoutedEventArgs e)
        {
                timerBlock4.Text = " ";
        }        
    }
}

            
 
        

      
    




