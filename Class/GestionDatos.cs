using Newtonsoft.Json;
using ProyectoWeb.Models;



namespace ProyectoWeb.Class
{
    public class GestionDatos
    {
        HttpClient client = new HttpClient();
        
        public async Task<MdUsuarios> UsuarioCrea(MdUsuarios data)
        {
            MdUsuarios crea = new MdUsuarios();
            if (data != null)
            {
                if(data.nombre == null || data.fecha == null || data.sexo == "S")
                {
                    crea = null;
                }
                else
                {
                    crea = data;
                }
            }
            return crea;
        }
        public async Task<string>Guardar(MdUsuarios data)
        {
            string msj = "";
            client.BaseAddress = new Uri("https://localhost:7124/");
            try
            {
                var resp = new HttpResponseMessage();
                if (data.idUsuario == 0)
                {
                     resp = await client.PostAsJsonAsync("api/Usuarios/Creacion", data);
                }
                else
                {
                     resp = await client.PostAsJsonAsync("api/Usuarios/Actualizacion", data);
                }
                
                if(resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync();
                    msj = json.Result;
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return msj;
        }
        public async Task<List<MdUsuarios>>ConsultaUsuarios()
        {
            List<MdUsuarios> usuarios = new List<MdUsuarios>();
            client.BaseAddress = new Uri("https://localhost:7124/");
            try
            {
                //var conte = JsonConvert.SerializeObject(data);
                var resp = await client.GetAsync("api/Usuarios/Consulta");
                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync();
                    usuarios = JsonConvert.DeserializeObject<List<MdUsuarios>>(json.Result);
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return usuarios;
        }
        public async Task<string>EliminarUsuario(int idUsuario)
        {
            string msj = "";
            client.BaseAddress = new Uri("https://localhost:7124/");
            try
            {                
                var resp = await client.GetAsync(@"api/Usuarios/Eliminacion?idUsuario=" + idUsuario );
                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync();
                    msj = json.Result;
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return msj;
        }
        public async Task<MdUsuarios>EditarUsuario(int id)
        {
            MdUsuarios us = new MdUsuarios();
            client.BaseAddress = new Uri("https://localhost:7124/");
            try
            {
                var resp = await client.GetAsync(@"api/Usuarios/consultaUnico?idUsuario=" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync();
                    //msj = json.Result;
                    us = JsonConvert.DeserializeObject<MdUsuarios>(json.Result);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return us;
        }

    }
}
