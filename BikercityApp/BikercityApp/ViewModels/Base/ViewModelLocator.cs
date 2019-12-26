using Autofac;
using Xamarin.Forms;
using System;
using System.Globalization;
using System.Reflection;
using BikercityApp.Services.Navigation;
using BikercityApp.ViewModels.User;
using BikercityApp.Services.RequestProvider;
using BikercityApp.Services.User;
using BikercityApp.ViewModels.Catalogo;
using BikercityApp.Services.Catalogo;
using BikercityApp.Services.Carrito;
using BikercityApp.Services.Cliente;
using BikercityApp.ViewModels.Ordenes;
using BikercityApp.Services.Ordenes;
using BikercityApp.Services.Direccion;
using BikercityApp.ViewModels.Servicios;
using BikercityApp.Services.Tarjeta;
using BikercityApp.Services.Notification;

namespace BikercityApp.ViewModels.Base
{
    public static class ViewModelLocator
    {
		public static readonly BindableProperty AutoWireViewModelProperty =
		   BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        private static IContainer _container;

		public static bool GetAutoWireViewModel(BindableObject bindable)
		{
			return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
		}

		public static bool UseMockService { get; set; }

		public static void RegisterDependencies(bool useMockServices)
		{
			var builder = new ContainerBuilder();
			//builder.RegisterType<LoginViewModel>();

			//builder.RegisterType<DialogService>().As<IDialogService>();
			#region viewModels
			builder.RegisterType<LoginViewModel>();
			builder.RegisterType<CatalogoViewModel>();
            builder.RegisterType<CarritoViewModel>();
            builder.RegisterType<ConfirmarPedidoViewModel>();
            builder.RegisterType<PerfilViewModel>();
            builder.RegisterType<DetalleProductoViewModel>();
            builder.RegisterType<HistorialCompraViewModel>();
            builder.RegisterType<HistorialServicioViewModel>();
            builder.RegisterType<HistorialServicioDetalleViewModel>();
            builder.RegisterType<DetalleCompraViewModel>();
            builder.RegisterType<AgregarDireccionViewModel>();
            builder.RegisterType<AgregarTarjetaViewModel>();
            builder.RegisterType<GestionarDireccionesViewModel>();
            builder.RegisterType<ValoracionesViewModel>();
            builder.RegisterType<ServicioViewModel>();
            builder.RegisterType<DetalleServicioViewModel>();
            builder.RegisterType<SolicitarServicioViewModel>();
            builder.RegisterType<GestionarMetodoPagoViewModel>();
            builder.RegisterType<HistorialServicioDetallePdfViewModel>();
            builder.RegisterType<HistorialCalendarViewModel>();
            builder.RegisterType<RegistrarUsuarioViewModel>();
            builder.RegisterType<PerfilUserViewModel>();

            #endregion

            #region Servicios
            builder.RegisterType<RequestProvider>().As<IRequestProvider>();
			builder.RegisterType<NavigationService>().As<INavigationService>();
			builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<CatalogoService>().As<ICatalogoService>();
            builder.RegisterType<CarritoService>().As<ICarritoService>();
            builder.RegisterType<ClienteService>().As<IClienteService>();
            builder.RegisterType<OrdenService>().As<IOrdenService>();
            builder.RegisterType<DireccionService>().As<IDireccionService>();
            builder.RegisterType<ServicioService>().As<IServicioService>();
            builder.RegisterType<TarjetaService>().As<ITarjetaService>();
            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<PerfilUserService>().As<IPerfilUserService>();

            #endregion

            if (useMockServices)
			{
				UseMockService = true;
			}
			else
			{
				UseMockService = false;
			}

			if (_container != null)
			{
				_container.Dispose();
			}
			_container = builder.Build();
		}

		public static T Resolve<T>()
		{
			return _container.Resolve<T>();
		}

		public static void SetAutoWireViewModel(BindableObject bindable, bool value)
		{
			bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
		}


		private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var view = bindable as Element;
			if (view == null)
			{
				return;
			}

			var viewType = view.GetType();
			var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
			var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
			var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

			var viewModelType = Type.GetType(viewModelName);
			if (viewModelType == null)
			{
				return;
			}
			var viewModel = _container.Resolve(viewModelType);
			view.BindingContext = viewModel;
		}
	}
}
