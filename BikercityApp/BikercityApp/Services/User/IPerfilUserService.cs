using BikercityApp.Models.Servicios;
using BikercityApp.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.User
{
    public interface IPerfilUserService
    {
        Task<DatosCliente> GetByID(int id);
    }
}
