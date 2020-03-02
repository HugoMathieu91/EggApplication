using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows; 
using System.Threading;
using System.Windows.Threading; 
using System.Threading.Tasks;

namespace WpfApp1
{

    public class Recipe
    {

        #region Variable Privées
        private string _title;
        private string _content;
        private int _timer;
   

        public event EventHandler<CustomEventArgs> RaiseCustomEvent;
   

        #endregion

        #region asccesseurs
        public string Title
        {

            get { return _title; }
            set
            {
                _title = value;

            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;

            }
        }

        public int Timer
        {
            get { return _timer; }
            set
            {
                _timer = value;

            }
        }
        #endregion

        #region Constructeurs

        public Recipe(string title, string content, int timer)
        {
            _title = title;
            _timer = timer;
            _content = content;
        }

        #endregion



        #region Methodes
        //Faire un méthode OnProgress qui crée un thread, ce thread envoie un evènement quand une seconde s'écoule
        public void Onprogress()
        {
            Thread threadName = new Thread(new ThreadStart(LaunchTimer));
            
            threadName.Start();
        }

        public void resetTimer()
        {
          //A faire
        }
        public void LaunchTimer()
        {

            //
            /*_time1 = TimeSpan.FromSeconds(20);
                  _timer1 = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                  {
                      
                      if (_time1 == TimeSpan.Zero) _timer1.Stop();
                      {
                          _time1 = _time1.Add(TimeSpan.FromSeconds(-1));
                          OnRaiseCustomEvent(new CustomEventArgs("test"));
                      }
                  }, Application.Current.Dispatcher);
                  _timer1.Start();*/

            int timer = _timer;
            string title = _title;
          
            do
            {
                Thread.Sleep(1000);
                timer--;
                OnRaiseCustomEvent(new CustomEventArgs(title,timer));

            } while (timer > 0);

            if (timer == 0)
            {
                //Ne marche pas, et lève une exception System.Threading.ThreadAbortException dans mscorlib.dll
                Thread.CurrentThread.Abort(); 
            }

        }

     

        public void OnRaiseCustomEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> handler = RaiseCustomEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

       
        
        #endregion
    }

    //Classe pour garder l'info de l'event
    public class CustomEventArgs : EventArgs
    {
        public string message;
        public int time;
       
        
        public CustomEventArgs(string title,int cookTime)
        {
            message = title;
            time = cookTime;  
           
          
        }

      
        

    }
}
