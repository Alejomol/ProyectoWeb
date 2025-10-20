using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.Class;
using ProyectoWeb.Models;
using System.Threading.Tasks;


namespace ProyectoWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly GestionDatos proc;        
        public UsuariosController()
        {
            proc = new GestionDatos();
        }
        public IActionResult Usuario()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Usuario(MdUsuarios producto)
        {
            MdUsuarios ser = await proc.UsuarioCrea(producto);
            if (ser != null)
            {
                var set = proc.Guardar(ser);
            }
            return RedirectToAction("Usuario");
        }
        public async Task<IActionResult> GestionUsuarios()
        {
            List<MdUsuarios> usu = await proc.ConsultaUsuarios();
            return View(usu);
        }
        public async Task<IActionResult> Editar(int id)
        {
            MdUsuarios md = new MdUsuarios();
            md = await proc.EditarUsuario(id);
            return View(md);
        }
        [HttpPost]
        public async Task<ActionResult> Editar(MdUsuarios producto)
        {
            MdUsuarios ser = await proc.UsuarioCrea(producto);
            if (ser != null)
            {
                var set = proc.Guardar(ser);
            }

            return RedirectToAction("GestionUsuarios");
        }
        [HttpGet]
        public async Task<ActionResult> Eliminar(int id)
        {
            string ser = await proc.EliminarUsuario(id);
            
           
            return RedirectToAction("GestionUsuarios");
        }
    }
}
