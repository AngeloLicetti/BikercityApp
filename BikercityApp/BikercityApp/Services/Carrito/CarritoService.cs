using BikercityApp.Helpers;
using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Catalogo.CarritoServicio;
using BikercityApp.Models.Common;
using BikercityApp.Services.RequestProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikercityApp.Services.Carrito
{
	public class CarritoService : ICarritoService
	{
		private IRequestProvider _requestProvider { get; set; }
		public CarritoService(IRequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}

		public async Task<List<DetalleProductoModel>> getListaCarrito()
		{
			if (Settings.loggedIn)
			{
				UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
				builder.Path = string.Format("api/Carrito/GetAllCarritoByCliente");
				builder.Query = string.Format("idCliente={0}", Settings.idCliente);

				string uri = builder.ToString();

				var body = await _requestProvider.GetAsync<List<DetalleProductoModel>>(uri).ConfigureAwait(false);
				return body;
			}
			else
			{
				return Settings.ShoppingCartList;
			}
		}
		public async Task<bool> agregarCarrito(DetalleProductoModel detalleProducto)
		{
			if (Settings.loggedIn)
			{
				UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
				builder.Path = string.Format("api/Carrito/AddCarritoFromClient");
				string uri = builder.ToString();

				AgregarCarritoInputModel inputModel = new AgregarCarritoInputModel()
				{
					cantidad = 1,
					idDetalleProducto = detalleProducto.idDetalleProducto,
					idCliente = Settings.idCliente
				};

				var body = await _requestProvider.PostAsync<bool, AgregarCarritoInputModel>(uri, inputModel).ConfigureAwait(false);
				return body;
			}
			else
			{
				var carritoList = Settings.ShoppingCartList;

				if (carritoList.Any(d => d.idDetalleProducto == detalleProducto.idDetalleProducto))
				{
					foreach (var item in carritoList)
					{
						if (item.idDetalleProducto == detalleProducto.idDetalleProducto)
							item.Cantidad++;
					}
				}
				else
				{
					detalleProducto.Cantidad = 1;
					carritoList.Add(detalleProducto);
				}

				Settings.ShoppingCartList = carritoList;
				return true;
			}
		}

		public async Task<bool> eliminarCarrito(DetalleProductoModel detalleProducto)
		{
			if (Settings.loggedIn)
			{
				UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
				builder.Path = string.Format("api/Carrito/DeleteDetalleCarrito");
				string uri = builder.ToString();

				EliminarCarritoInputModel inputModel = new EliminarCarritoInputModel()
				{
					idDetalleProducto = detalleProducto.idDetalleProducto,
					idCliente = Settings.idCliente
				};

				var body = await _requestProvider.PostAsync<bool, EliminarCarritoInputModel>(uri, inputModel).ConfigureAwait(false);
				return body;
			}
			else
			{
				var carritoList = Settings.ShoppingCartList;
				foreach (var item in carritoList)
				{
					if (item.idDetalleProducto == detalleProducto.idDetalleProducto)
					{
						carritoList.Remove(item);
						break;
					}
				}

				Settings.ShoppingCartList = carritoList;
				return true;
			}
		}

		public async Task<BaseResponse> actualizarCantidadCarrito(DetalleProductoModel detalleProducto, int cantidad)
		{
			if (Settings.loggedIn)
			{
				UriBuilder builder = new UriBuilder(GlobalSettings.Instance.DefaultEndpoint);
				builder.Path = string.Format("api/Carrito/AddCantidadCarritoApp");
				string uri = builder.ToString();

				AgregarCarritoInputModel inputModel = new AgregarCarritoInputModel()
				{
					cantidad = cantidad,
					idDetalleProducto = detalleProducto.idDetalleProducto,
					idCliente = Settings.idCliente
				};
				var body = await _requestProvider.PostAsync<BaseResponse, AgregarCarritoInputModel>(uri, inputModel).ConfigureAwait(false);
				return body;
			}
			else
			{
				var carritoList = Settings.ShoppingCartList;

				if (cantidad > 0)
				{
					foreach (var item in carritoList)
					{
						if (item.idDetalleProducto == detalleProducto.idDetalleProducto)
						{
							item.Cantidad = cantidad;
							break;
						}
					}
				}
				else
				{
					foreach (var item in carritoList)
					{
						if (item.idDetalleProducto == detalleProducto.idDetalleProducto)
						{
							carritoList.Remove(item);
							break;
						}
					}
				}

				Settings.ShoppingCartList = carritoList;
				return new BaseResponse { result = true };
			}
		}
	}
}

