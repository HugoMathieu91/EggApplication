using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading; 
using System.Threading.Tasks;
using System.Windows.Threading;


namespace WpfApp1
{
    class RecipeCollection
    {

        #region Properties
        public ObservableCollection<Recipe> Recipes { get; set; }



        #endregion

        public void StartCollection()
        {
            InitializeCollection();
        }

        private void InitializeCollection()
        {
            Recipes = new ObservableCollection<Recipe>();
        }
        public void AddRecipe(string title, string description, int time)
        {
            Recipes.Add(new Recipe(title, description, time));
        }

        public void AddMadeRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }
    }
       
}
