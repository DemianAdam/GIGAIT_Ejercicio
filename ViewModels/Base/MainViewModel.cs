using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;

        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
