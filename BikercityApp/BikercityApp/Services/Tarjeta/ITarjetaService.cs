using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikercityApp.Models.Catalogo;
using BikercityApp.Models.Catalogo.ModeloServicio;
using BikercityApp.Models.Common;
using BikercityApp.Models.User;
using BikercityApp.Models.User.TarjetaServicio;

namespace BikercityApp.Services.Tarjeta
{
    public interface ITarjetaService
    {
        Task<bool> agregarTarjeta(TarjetaModel tarjeta);
        Task<List<TarjetaModel>> getListaTarjeta();
		Task<BaseResponse> predeterminarTarjeta(PredeterminarTarjetaInputModel tarjeta);
        Task<TarjetaModel> getTarjetaPredeterminada();

    }
}
