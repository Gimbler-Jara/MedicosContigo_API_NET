using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Emun;
using NET_MedicosContigo_API.Models;

namespace NET_MedicosContigo_API.Reposotorio.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> ListarUsuarios();
        Usuario ObtenerUsuario(int id);
        Usuario CrearUsuario(Usuario usuario);
        Usuario ActualizarUsuario(int id, Usuario usuario);
        void EliminarUsuario(int id);

        (Usuario? usuario, LoginResultado resultado) BuscarPorEmail(LoginDTO dto);

        bool CambiarEstadoUsuario(int idUsuario);

    }
}
