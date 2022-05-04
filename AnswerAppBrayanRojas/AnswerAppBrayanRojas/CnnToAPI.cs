using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerAppBrayanRojas
{
    public static class CnnToAPI
    {
        // en esta clase estatica vamos a almacenar la info de configuracionde consumo del API
        
        //En pruebas normalmente se usa una ruta distinta que en produccion.
        
        public static string ProductionRoute = "http://192.168.1.13:45455/api/";
        public static string TestingRoute = "http://192.168.1.13:45455/api/";


        //todo: Agregar la funcionalidad para JWT

        //El api key aca es util ya que el usuario antes de registrar podria usarlo, y ya una vez registrado puede usar JWT.


        public static string ApiKeyName = "ApiKey";
        public static string ApiKeyValue = "123456";

    }
}
