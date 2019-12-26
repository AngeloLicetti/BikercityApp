using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikercityApp.Models.Catalogo
{
	public class DetalleCarritoModel : ExtendedBindableObject
	{
		public int idDetalleProducto { get; set; }
		public string descripcion { get; set; }
		public string descripcionProducto { get; set; }
		public string titulo { get; set; }
		public int stockWeb { get; set; }
		public int garantia { get; set; }
		public string estado { get; set; }
		public DateTime fechaRegistro { get; set; }
		public int numeroInventario { get; set; }
		public string sku { get; set; }
		public float descuento { get; set; }
		public string foto { get; set; }
		public DateTime fechaDescuento { get; set; }
		public string estadoProducto { get; set; }
		public int idEstadoProducto { get; set; }
		public int idInventario { get; set; }
		public int idTipoProducto { get; set; }
		public DateTime registroProd { get; set; }
		public float precioVenta { get; set; }
		public string tipoProducto { get; set; }
		public List<string> listaFotos { get; set; }
		public string caracteristicas { get; set; }
		public DateTime registroInve { get; set; }
		public int stockInventario { get; set; }

		public string fotoPrincipal { get { return listaFotos.FirstOrDefault(); } }
		public List<CaracteristicaProductoModel> listaCaracteristicas { get; set; }
		private int _cantidad;
		public int Cantidad
		{
			get { return _cantidad; }
			set
			{
				_cantidad = value;
				if (value != default(int))
				{
					SubTotal = precioVenta * Cantidad;
				}
				RaisePropertyChanged(() => Cantidad);
			}
		}

		private double _subTotal;
		public double SubTotal
		{
			get { return _subTotal; }
			set { _subTotal = value; RaisePropertyChanged(() => SubTotal); }
		}

	}
}
