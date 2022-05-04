using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace AnswerAppBrayanRojas.Models
{
    public class Ask
    {
        public RestRequest request { get; set; }

        const string mimetype = "application/json";

        const string contentType = "Content-Type";
        public Ask()
        {
            request = new RestRequest();
            Answers = new HashSet<Answer>();
        }

        public long AskId { get; set; }
        public DateTime Date { get; set; }
        public string Ask1 { get; set; }
        public int UserId { get; set; }
        public int AskStatusId { get; set; }
        public bool IsStrike { get; set; }
        public string ImageUrl { get; set; }
        public string AskDetail { get; set; }
        public virtual AskStatus AskStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }


        //esta funcion llama a la ruta GetQuestionsListByUserID(pUserId) que retorna un json
        //que tenemos que convertir a clase (modelo) Ask.

        public async Task<ObservableCollection<Ask>> GetQuestionsListByUser()
        {
            try
            {
                //como esta ruta es un poco mas compleja de consumir ya que lleva una funcion con nombre y ademas dos parametros
                //lo mas conveniente es formatearle por aparte y luego adjuntarle a base de URL (CnnToAPI.ProductRoute)
                //para obtener la ruta completa
                string routeSuFix = string.Format("Asks/GetQuestionsListByUserID?pUserID={0}", this.UserId);

                string FinalApiRoute = CnnToAPI.ProductionRoute + routeSuFix;

                RestClient client = new RestClient(FinalApiRoute);

                this.request = new RestRequest(FinalApiRoute, Method.Get);

                this.request.AddHeader(CnnToAPI.ApiKeyName, CnnToAPI.ApiKeyValue);
                this.request.AddHeader(contentType, mimetype);

                var response = await client.ExecuteAsync(request);

                HttpStatusCode statusCode = response.StatusCode;

                var Qlist = JsonConvert.DeserializeObject< ObservableCollection<Ask>>(response.Content);


                if (statusCode == HttpStatusCode.OK)
                {
                    return Qlist;
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
