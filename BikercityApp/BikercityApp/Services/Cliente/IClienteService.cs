using BikercityApp.Models.User;
using BikercityApp.Models.User.ClienteServicio;
using BikercityApp.Models.User.ModeloServicio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikercityApp.Services.Cliente
{
    public interface IClienteService
    {
        Task<LoginClienteResponse> LoginCliente(LoginClienteInput input);
        Task<bool> agregarCliente(AgregarClienteInputModel clienteInputModel);
    }
}
