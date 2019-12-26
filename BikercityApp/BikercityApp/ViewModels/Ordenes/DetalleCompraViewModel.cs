using BikercityApp.Models.Orden;
using BikercityApp.Extensions;
using BikercityApp.Services.Ordenes;
using BikercityApp.ViewModels.Base;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.ViewModels.Ordenes
{
    public class DetalleCompraViewModel : ViewModelBase
    {
        private IOrdenService _ordenService;

        public DetalleCompraViewModel(
                                IOrdenService ordenService
                             )
        {
            _ordenService = ordenService;
        }
		public override async Task InitializeAsync(object navigationData)
		{
			try
			{
				OrdenModel model = navigationData as OrdenModel;
				List<DetalleOrdenModel> detalle = await _ordenService.GetDetalleCompraByID(model.idOrden);

				Orden = model;
				//int contador = 0;
				//double auxprecio = 0;
				//double auxdesc = 0;
				foreach (var item in detalle)
				{
					//auxdesc += item.descuento;
					//auxprecio += item.precio;
					//contador += item.cantidad;
					if (item.foto == null) item.foto = "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png";
				}

                Orden.cantidad = detalle.Sum(l => l.cantidad);
                Orden.preciototal = detalle.Sum(l => l.precio*l.cantidad);
				Orden.descuentotal = detalle.Sum(l=>l.descuento);
                Orden.precio = Orden.preciototal - Orden.descuentotal;


                DetalleOrdenList = detalle.ToObservableCollection();
			}
			catch (Exception e)
			{
				return;
			}
		}
        private ObservableCollection<DetalleOrdenModel> _detalleOrden;

        public ObservableCollection<DetalleOrdenModel> DetalleOrdenList
        {
            get { return _detalleOrden; }
            set { _detalleOrden = value; RaisePropertyChanged(() => DetalleOrdenList); }
        }
		private OrdenModel _orden;

		public OrdenModel Orden
		{
			get { return _orden; }
			set { _orden = value; RaisePropertyChanged(() => Orden); }
		}
	}
}
