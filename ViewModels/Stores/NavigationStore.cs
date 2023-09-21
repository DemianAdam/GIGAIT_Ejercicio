﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores
{
    public class NavigationStore
    {
		private ViewModelBase currentViewModel;

		public ViewModelBase CurrentViewModel
		{
			get => currentViewModel;
			set
			{ 
				currentViewModel = value; 
				OnCurrentViewModelChanged();
			}
		}

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;

	}
}