using BikercityApp.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace BikercityApp.Services.Navigation
{
	public interface INavigationService
	{
		ViewModelBase PreviousPageViewModel { get; }

		Task InitializeAsync();

		Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

		Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

		Task NavigateToAsync(Type ViewModelType, object parameter);

		Task RemoveLastFromBackStackAsync();

		Task RemoveBackStackAsync();

		Task PopToRoot(bool Inactive = false);

		Task NavigateBackAsync();
	}
}
