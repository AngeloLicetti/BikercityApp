using BikercityApp.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikercityApp.Services.User
{
	public interface IUserService
	{
		Task<List<UserModel>> GetUsuariosLista();
	}
}
