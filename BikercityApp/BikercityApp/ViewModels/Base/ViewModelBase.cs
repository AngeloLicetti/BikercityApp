using BikercityApp.Helpers;
using BikercityApp.Services.Navigation;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Base
{
	public abstract class ViewModelBase : ExtendedBindableObject
	{
		#region Protected Fields

		//protected readonly IDialogService DialogService;
		protected readonly INavigationService NavigationService;

		private bool _isModalAlert = false;

		#endregion Protected Fields

		#region Private Fields

		private bool _isEmpty = false;
		private bool _isBusy;
		private bool _isEnabled;
		private bool _isLoadingMore;
		private bool _isConnected;
		private bool _isModalVisible = false;
        private string _modalTitle;
        private bool _isConsulting = false;
		private object _navigationData;

		#endregion Private Fields

		#region Public Constructors

		protected ViewModelBase()
		{
			//DialogService = ViewModelLocator.Resolve<IDialogService>();
			NavigationService = ViewModelLocator.Resolve<INavigationService>();
			IsConnected = true;
			_isConnected = true;
		}

		public ICommand OpenModalCommand => new Command(async () => await OpenModal());
		public ICommand CloseModalCommand => new Command(async () => await CloseModal());

		#endregion Public Constructors

		#region Public Properties

		public bool IsLoaded;

		public bool IsEmpty
		{
			get
			{
				return _isEmpty;
			}

			set
			{
				_isEmpty = value;
				RaisePropertyChanged(() => IsEmpty);
			}
		}
		public bool IsEnabled
		{
			get
			{
				return _isEnabled;
			}

			set
			{
				_isEnabled = value;
				RaisePropertyChanged(() => IsEnabled);
			}
		}
		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}

			set
			{
				_isBusy = value;
				RaisePropertyChanged(() => IsBusy);
			}
		}
		public bool IsLoadingMore
		{
			get
			{
				return _isLoadingMore;
			}

			set
			{
				_isLoadingMore = value;
				RaisePropertyChanged(() => IsLoadingMore);
			}
		}
		public bool IsConnected
		{
			get { return _isConnected; }
			set
			{
				_isConnected = value;
				RaisePropertyChanged(() => IsConnected);
			}
		}
        public string ModalTitle
        {
            get { return _modalTitle; }
            set
            {
                _modalTitle = value;
                RaisePropertyChanged(() => ModalTitle);
            }
        }

        public bool IsModalVisible
        {
            get
            {
                return _isModalVisible;
            }

            set
            {
                _isModalVisible = value;
                RaisePropertyChanged(() => IsModalVisible);
            }
        }

        #endregion Public Properties

        #region Public Methods

        public virtual async Task InitializeAsync(object navigationData)
		{
			IsConnected = true;
			_navigationData = navigationData;
			Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
			{
				await TryConnectAsync();
			};

			IsConnected = true;
			IsConnected = await ConnectivityHelper.CheckConnectivityAsync();
		}

		public bool IsModalAlert
		{
			get { return _isModalAlert; }
			set
			{
				_isModalAlert = value;
				RaisePropertyChanged(() => IsModalAlert);
			}
		}

		protected virtual async Task CloseModal()
		{
			IsModalAlert = false;
            IsModalVisible = false;
		}
		public ICommand CloseInfoCommand => new Command(async () => await CloseInfo());
		private async Task CloseInfo()
		{
			IsModalVisible = false;
		}


		#endregion Public Methods

		#region Private Methods

		protected void IsEmptyList(IList list)
		{
			IsEmpty = list.Count == 0;
		}

		private async Task OpenModal()
		{
			IsModalAlert = true;
		}

		private async Task TryConnectAsync()
		{
			if (_isConsulting) { return; }

			try
			{
				_isConsulting = true;
				IsConnected = await ConnectivityHelper.CheckConnectivityAsync();
				if (IsConnected)
				{
					if (!IsLoaded)
					{
						await InitializeAsync(_navigationData);
					}
				}
			}
			catch (Exception)
			{
				//Do nothing
			}
			finally
			{
				_isConsulting = false;
			}
		}

		

		#endregion Private Methods
	}
}
