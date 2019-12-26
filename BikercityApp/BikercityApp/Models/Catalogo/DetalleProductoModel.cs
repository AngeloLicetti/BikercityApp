using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace BikercityApp.Models.Catalogo
{
	public class DetalleProductoModel : ExtendedBindableObject
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
		public List<ProductImageModel> Fotos {
			get
			{
				List<ProductImageModel> imagenes = new List<ProductImageModel>();
				foreach (var foto in listaFotos)
				{
					imagenes.Add(new ProductImageModel { Foto = foto });
				}
				return imagenes;
			}
		}
		public string caracteristicas { get; set; }
		public DateTime registroInve { get; set; }
		public int stockInventario { get; set; }
		public string estadoEtiqueta {
			get
			{
				string estadoDisplay = String.Empty;
				if (stockWeb < 10)
					estadoDisplay = "Quedan "+stockWeb+"!";
				if (stockWeb <= 0)
					estadoDisplay = "Agotado";

				
				return estadoDisplay;
			}
		}
		public string colorEtiqueta { get; set; }
		public bool boolEtiqueta {
			get {
				if (stockWeb < 10)
					return true;
				if (stockWeb <= 0)
					return true;
				return false;
			} }


		public string fotoPrincipal { get { return listaFotos.FirstOrDefault(); } }
		public List<CaracteristicaProductoModel> listaCaracteristicas { get; set; }
		//Carrito
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
			get
			{
				_subTotal = precioVenta * Cantidad;
				return _subTotal;
			}
			set { _subTotal = value; RaisePropertyChanged(() => SubTotal); }
		}

		public ICommand EliminarCarritoCommand { get; set; }
		public ICommand AumentarCarritoCommand { get; set; }
		public ICommand DisminuirCarritoCommand { get; set; }

	}
	public class CaracteristicaProductoModel
	{
		public string nombre { get; set; }
		public string valor { get; set; }
	}
	public class ProductImageModel
	{
		public string Foto { get; set; }
	}
}
