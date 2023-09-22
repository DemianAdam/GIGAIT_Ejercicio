using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores;

namespace ViewModels.Services
{
    /// <summary>
    /// ParameterNavigationService class. This class represents a parameter navigation service.
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public class ParameterNavigationService<TParameter>
    {
        private readonly NavigationStore navigationStore;
        private readonly Func<TParameter, ViewModelBase> createViewModel;

        public ParameterNavigationService(NavigationStore navigationStore, Func<TParameter, ViewModelBase> createViewModel)
        {
            this.navigationStore = navigationStore;
            this.createViewModel = createViewModel;
        }

        public void Navigate(TParameter parameter)
        {
            navigationStore.CurrentViewModel = createViewModel(parameter);
        }
    }
}
