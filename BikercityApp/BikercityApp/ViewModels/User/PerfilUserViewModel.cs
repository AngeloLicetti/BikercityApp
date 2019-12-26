using BikercityApp.Helpers;
using BikercityApp.Models.User;
using BikercityApp.Services.User;
using BikercityApp.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace BikercityApp.ViewModels.User
{
    public class PerfilUserViewModel : ViewModelBase
    {
        private IPerfilUserService _PerfilUserService;
 
        public PerfilUserViewModel(
                                IPerfilUserService PerfilUserService
                             )
        {
            _PerfilUserService = PerfilUserService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            try
            {
                DatosCliente datoscliente = await _PerfilUserService.GetByID(Settings.idCliente);
                datosCliente = datoscliente;
            }
            catch (Exception ex)
            {

            }

        }
        private DatosCliente _datoscliente;
        public DatosCliente datosCliente
        {
            get { return _datoscliente; }
            set
            {
                _datoscliente = value;
                RaisePropertyChanged(() => datosCliente);
            }
        }
    }
}
