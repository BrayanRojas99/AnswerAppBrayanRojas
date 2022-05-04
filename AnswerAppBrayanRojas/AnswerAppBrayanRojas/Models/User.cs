using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace AnswerAppBrayanRojas.Models
{
    public partial class User
    {
        public RestRequest request { get; set; }

        const string mimetype = "application/json";

        const string contentType = "Content-Type";

        public UserRole MyRole { get; set; }

        public User()
        {
            request = new RestRequest();

            Answers = new HashSet<Answer>();
            Asks = new HashSet<Ask>();
            ChatReceivers = new HashSet<Chat>();
            ChatSenders = new HashSet<Chat>();
            Likes = new HashSet<Like>();

            MyRole = new UserRole();
        }

        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserPassword { get; set; }
        public int StrikeCount { get; set; }
        public string BackUpEmail { get; set; }
        public int UserStatusId { get; set; }
        public string JobDescription { get; set; }
        public int CountryId { get; set; }
        public int UserRoleId { get; set; }

        public virtual Country Country { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Ask> Asks { get; set; }
        public virtual ICollection<Chat> ChatReceivers { get; set; }
        public virtual ICollection<Chat> ChatSenders { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        public async Task<bool> AddNewUser()
        {
            bool R = false;

           

            try
            {
                /*se adjunta la url base la direccion del recurso que queremos consumir ruta final del api configuracion*/
                string FinalApiRoute = CnnToAPI.ProductionRoute + "users";

                RestClient client = new RestClient(FinalApiRoute);


                this.request = new RestRequest(FinalApiRoute, Method.Post);


                //agregar la info de seguridad, en este caso api key
                this.request.AddHeader(CnnToAPI.ApiKeyName, CnnToAPI.ApiKeyValue);
                this.request.AddHeader(contentType,mimetype);

                //serializar esta clase para pasarla al body
                string SerializedClass = JsonConvert.SerializeObject(this);

                this.request.AddBody(SerializedClass, mimetype);

                //esto envia la consulta al api y recibe una respuesta que debemos leer
                RestResponse response = await client.ExecuteAsync(this.request);

                HttpStatusCode statusCode = response.StatusCode;


                if (statusCode == HttpStatusCode.Created)
                {
                    R = true;
                }


            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                throw;
            }

            
            
            return R;
        }


        //funcion para validar el acceso del usuario en el sistema
        public async Task<bool> ValidateUserAccess() 
        {
            bool R = false;

            try
            {
                //como esta ruta es un poco mas compleja de consumir ya que lleva una funcion con nombre y ademas dos parametros
                //lo mas conveniente es formatearle por aparte y luego adjuntarle a base de URL (CnnToAPI.ProductRoute)
                //para obtener la ruta completa
                string routeSuFix = string.Format("users/ValidateUserLogin?pEmail={0}&pPassword={1}", this.UserName,this.UserPassword);

                string FinalApiRoute = CnnToAPI.ProductionRoute + routeSuFix;

                RestClient client = new RestClient(FinalApiRoute);

                this.request = new RestRequest(FinalApiRoute, Method.Get);

                this.request.AddHeader(CnnToAPI.ApiKeyName, CnnToAPI.ApiKeyValue);
                this.request.AddHeader(contentType, mimetype);

                RestResponse response = await client.ExecuteAsync(request);

                HttpStatusCode statusCode = response.StatusCode;

               if (statusCode == HttpStatusCode.OK)
                {
                    R = true;
                }



            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }

            return R;
        }
    }
}
