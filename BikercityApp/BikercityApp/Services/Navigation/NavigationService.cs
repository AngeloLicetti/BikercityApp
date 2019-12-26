using BikercityApp.ViewModels.Base;
using BikercityApp.ViewModels.Catalogo;
using BikercityApp.ViewModels.User;
using BikercityApp.Views;
using BikercityApp.Views.User;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikercityApp.Services.Navigation
{
	public class NavigationService : INavigationService
	{
		public ViewModelBase PreviousPageViewModel
		{
			get
			{
				var mainPage = Application.Current.MainPage as CustomNavigationView;
				var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
				return viewModel as ViewModelBase;
			}
		}

		public Task InitializeAsync()
		{
			return NavigateToAsync<CatalogoViewModel>();
			//return NavigateToAsync<ViewModel>();
		}

		public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
		{
			return InternalNavigateToAsync(typeof(TViewModel), null);
		}

		public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
		{
			return InternalNavigateToAsync(typeof(TViewModel), parameter);
		}
		public Task NavigateToAsync(Type ViewModelType, object parameter)
		{
			return InternalNavigateToAsync(ViewModelType, parameter);
		}

		public Task RemoveLastFromBackStackAsync()
		{
			var mainPage = Application.Current.MainPage as CustomNavigationView;

			if (mainPage != null)
			{
				mainPage.Navigation.RemovePage(
					mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
			}

			return Task.FromResult(true);
		}

		public async Task PopToRoot(bool Inactive = false)
		{
			if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
			{
				Page page = CreatePage(typeof(LoginView));
				Application.Current.MainPage = new CustomNavigationView(page);
				await (page.BindingContext as ViewModelBase).InitializeAsync(Inactive);
			}
			else
			{
				await RemoveBackStackAsync();
				await NavigateToAsync<LoginViewModel>(Inactive);
			}
		}

		public async Task RemoveBackStackAsync()
		{
			var mainPage = Application.Current.MainPage as CustomNavigationView;

			if (mainPage != null)
			{
				for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
				{
					var page = mainPage.Navigation.NavigationStack[i];
					mainPage.Navigation.RemovePage(page);
				}
			}
		}

		public async Task NavigateBackAsync()
		{
			var navigationPage = Application.Current.MainPage as CustomNavigationView;
			await navigationPage.PopAsync().ConfigureAwait(false);
		}

		private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
		{
			Page page = CreatePage(viewModelType);
			var navigationPage = Application.Current.MainPage as CustomNavigationView;
			if (navigationPage != null)
			{
				var currentPageType = navigationPage.CurrentPage.BindingContext.GetType();
				if (currentPageType == viewModelType)
					return;
				for (int i = 0; i < navigationPage.Navigation.NavigationStack.Count; i++)
				{
					Page item = navigationPage.Navigation.NavigationStack[i];
					if (viewModelType == item.BindingContext.GetType())
					{
						navigationPage.Navigation.RemovePage(item);
						break;
					}
				}
				await navigationPage.PushAsync(page);
				
			}
			else
			{
				Application.Current.MainPage = new CustomNavigationView(page);
			}
			await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
		}

		private Type GetPageTypeForViewModel(Type viewModelType)
		{
			var viewName = viewModelType.FullName.Replace("Model", string.Empty);
			var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
			var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
			var viewType = Type.GetType(viewAssemblyName);
			return viewType;
		}

		private Page CreatePage(Type viewModelType)
		{
			Type pageType = GetPageTypeForViewModel(viewModelType);
			Page page = Activator.CreateInstance(pageType) as Page;
			return page;
		}
	}
}
