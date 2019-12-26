using BikercityApp.ViewModels.Catalogo;
using BikercityApp.ViewModels.Menu;
using BikercityApp.ViewModels.Servicios;
using BikercityApp.ViewModels.User;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Base
{
	public class ViewModelTab : ViewModelBase
	{
		protected Color p_selectedColor;
		private OptionModel _homeOption;

		public OptionModel HomeOption
		{
			get { return _homeOption; }
			set
			{
				_homeOption = value;
				RaisePropertyChanged(() => HomeOption);
			}
		}

		private OptionModel _catalogOption;

		public OptionModel CatalogOption
		{
			get { return _catalogOption; }
			set
			{
				_catalogOption = value;
				RaisePropertyChanged(() => CatalogOption);
			}
		}

		private OptionModel _paymentPlacesOption;

		public OptionModel PaymentPlacesOption
		{
			get { return _paymentPlacesOption; }
			set
			{
				_paymentPlacesOption = value;
				RaisePropertyChanged(() => PaymentPlacesOption);
			}
		}

		private OptionModel _updateDataOption;

		public OptionModel UpdateDataOption
		{
			get { return _updateDataOption; }
			set
			{
				_updateDataOption = value;
				RaisePropertyChanged(() => UpdateDataOption);
			}
		}

		#region Color

		private Color _homeColor;

		public Color HomeColor
		{
			get { return _homeColor; }
			set
			{
				_homeColor = value;
				RaisePropertyChanged(() => HomeColor);
			}
		}

		private Color _cartColor;

		public Color CartColor
		{
			get { return _cartColor; }
			set
			{
				_cartColor = value;
				RaisePropertyChanged(() => CartColor);
			}
		}

		private Color _profileColor;

		public Color ProfileColor
		{
			get { return _profileColor; }
			set
			{
				_profileColor = value;
				RaisePropertyChanged(() => ProfileColor);
			}
		}

		private Color _catalogColor;

		public Color CatalogColor
		{
			get { return _catalogColor; }
			set
			{
				_catalogColor = value;
				RaisePropertyChanged(() => CatalogColor);
			}
		}

        private Color _serviceColor;

        public Color ServiceColor
        {
            get { return _serviceColor; }
            set
            {
                _serviceColor = value;
                RaisePropertyChanged(() => ServiceColor);
            }
        }

        #endregion Color

        #region Icons

        private string _homeIcon;

		public string HomeIcon
		{
			get { return _homeIcon; }
			set
			{
				_homeIcon = value;
				RaisePropertyChanged(() => HomeIcon);
			}
		}

		private string _paymentPlacesIcon;

		public string PaymentPlacesIcon
		{
			get { return _paymentPlacesIcon; }
			set
			{
				_paymentPlacesIcon = value;
				RaisePropertyChanged(() => PaymentPlacesIcon);
			}
		}

		private string _updateDataIcon;

		public string UpdateDataIcon
		{
			get { return _updateDataIcon; }
			set
			{
				_updateDataIcon = value;
				RaisePropertyChanged(() => UpdateDataIcon);
			}
		}

		private string _catalogIcon;

		public string CatalogIcon
		{
			get { return _catalogIcon; }
			set
			{
				_catalogIcon = value;
				RaisePropertyChanged(() => CatalogIcon);
			}
		}

		#endregion Icons

		//Commands

		public ICommand GoToCarritoViewCommand => new Command(async () => await GoToCarritoView());

		private async Task GoToCarritoView()
		{
			await NavigationService.NavigateToAsync<CarritoViewModel>();
		}

		public ICommand GoToPerfilViewCommand => new Command(async () => await GoToPerfilView());

		private async Task GoToPerfilView()
		{
			await NavigationService.NavigateToAsync<PerfilViewModel>();
		}
		public ICommand GoToCatalogoViewCommand => new Command(async () => await GoToCatalogoView());

		private async Task GoToCatalogoView()
		{
			await NavigationService.NavigateToAsync<CatalogoViewModel>();
		}

        public ICommand GoToServicioViewCommand => new Command(async () => await GoToServicioView());

        private async Task GoToServicioView()
        {
            await NavigationService.NavigateToAsync<ServicioViewModel>();
        }

        private async Task SelectHomenAsync(OptionModel MenuOption)
		{
			if (MenuOption == null)
			{
				return;
			}
			if (MenuOption.Target != null)
			{
				await NavigationService.NavigateToAsync(MenuOption.Target,null).ConfigureAwait(false);
				await NavigationService.RemoveBackStackAsync();
			}
		}

		private async Task SelectOptionAsync(OptionModel MenuOption, object navigationData)
		{

			await NavigationService.NavigateToAsync(MenuOption.Target, navigationData).ConfigureAwait(false);
		}

		private bool _isTabVisible = true;

		public bool IsTabVisible
		{
			get { return _isTabVisible; }
			set
			{
				_isTabVisible = value;
				RaisePropertyChanged(() => IsTabVisible);
			}
		}
		public override async Task InitializeAsync(object navigationData)
		{
			await base.InitializeAsync(navigationData);
			// var filteredList = list.Where(s => s.Enabled).ToObservableCollection();

			//HomeOption = list.FirstOrDefault(s => s.Id == Constants.Options.QuickHome.Item1);
			//UpdateDataOption = list.FirstOrDefault(s => s.Id == Constants.Options.QuickUpdateData.Item1);
			//PaymentPlacesOption = list.FirstOrDefault(s => s.Id == Constants.Options.QuickPaymentPlace.Item1);
			//CatalogOption = list.FirstOrDefault(s => s.Id == Constants.Options.QuickCatalogMain.Item1);

			HomeColor = Color.FromHex("#495057");
			CartColor = Color.FromHex("#495057");
			ProfileColor = Color.FromHex("#495057");
			p_selectedColor = Color.FromHex("#4286f4");
            ServiceColor = Color.FromHex("#495057");
            //HomeIcon = HomeOption.Icon;
            //PaymentPlacesIcon = PaymentPlacesOption.Icon;
            //UpdateDataIcon = UpdateDataOption.Icon;
            //CatalogIcon = CatalogOption.Icon;

            IsTabVisible = false;
		}
	}
}
