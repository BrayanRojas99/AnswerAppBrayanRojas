using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace AnswerAppBrayanRojas.Models
{
    public class UserRole
    {
        public RestRequest request { get; set; }

        const string mimeType = "application/json";
        const string contentType = "Content-Type";

        public UserRole()
        {
            Users = new HashSet<User>();
            request = new RestRequest();
        }

        public int UserRoleId { get; set; }
        public string UserRole1 { get; set; }

        public virtual ICollection<User> Users { get; set; }


        //cargar los roles del usuario
        public async Task<List<UserRole>> LoadRoles()
        {
            try
            {
                //configurar primero el cliente
                //ruta final
                string FinalRoute = CnnToAPI.ProductionRoute + "UserRoles";

                //cliente
                RestClient cliente = new RestClient(FinalRoute);

                //configurar la solicitud
                this.request = new RestRequest(FinalRoute, Method.Get);

                //asignar valores de la llave

                this.request.AddHeader(CnnToAPI.ApiKeyName,CnnToAPI.ApiKeyValue);
                this.request.AddHeader(contentType,mimeType);

                RestResponse response = await cliente.ExecuteAsync(request);

                //convertir a list la respuesta
                var RoleList = JsonConvert.DeserializeObject<List<UserRole>>(response.Content);

                HttpStatusCode Status = response.StatusCode;

                if (Status == HttpStatusCode.OK)
                {
                    return RoleList;
                }
                else 
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }

    }
}
