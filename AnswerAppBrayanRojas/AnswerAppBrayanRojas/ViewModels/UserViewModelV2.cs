using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnswerAppBrayanRojas.Models;
using Xamarin.Forms;
using AnswerAppBrayanRojas.Tools;

namespace AnswerAppBrayanRojas.ViewModels
{
    public class UserViewModelV2 : BaseViewModel
    {
        //esta es la version mas immplementada en ambitos laborales
        //a diferencia del primer vm para el usuario, este sera adecuado para
        //realizar el binding a cada control en la vista

        //con respecto a las propiedades debemos usar el formato full prop
        //ya que debemos implementar OnPropertyChange (cuya funcionalidad se hereda
        //de baseviewmodel)

        //El binding entre los controles de la vista y las propiedades del viewmodel
        //ocurren en "tiempo real" al modificar los datos de la vista
        //para que esto ocurra debemos de implementar Onpropchange en el get; o en el set;
        //de la propiedad

        private string nombreDeUsuario;
        private string primerNombre;
        private string apellido;
        private string numeroTelefono;
        private string usuarioClave;
        private int intentosPerdidos;
        private string emailRespaldo;
        private int usuarioEstadoId;
        private string trabajoDescripcion;
        private int idPais;
        private int idUsuarioRol;

        public User MiUsuario { get; set; }

        public int IdUsuarioRol
        {
            get { return idUsuarioRol; }
            set 
            {
                if (idUsuarioRol == value)
                {
                    return;
                }

                idUsuarioRol = value;

                OnPropertyChanged(nameof(IdUsuarioRol));
            }
        }


        public int IdPais
        {
            get { return idPais; }
            set 
            {
                if (idPais == value)
                {
                    return;
                }

                idPais = value;

                OnPropertyChanged(nameof(IdPais));
            }
        }


        public string TrabajoDescripcion
        {
            get { return trabajoDescripcion; }
            set 
            {
                if (trabajoDescripcion == value)
                {
                    return;
                }

                trabajoDescripcion = value;

                OnPropertyChanged(nameof(TrabajoDescripcion));
            }
        }


        public int UsuarioEstadoId
        {
            get { return usuarioEstadoId; }
            set 
            {
                if (usuarioEstadoId == value)
                {
                    return;
                }

                usuarioEstadoId = value;

                OnPropertyChanged(nameof(UsuarioEstadoId));
            }
        }


        public string EmailRespaldo
        {
            get { return emailRespaldo; }
            set 
            {
                if (emailRespaldo == value)
                {
                    return;
                }

                emailRespaldo = value;

                OnPropertyChanged(nameof(EmailRespaldo));
            }
        }


        public int IntentosPerdidos
        {
            get { return intentosPerdidos; }
            set 
            {
                if (intentosPerdidos == value)
                {
                    return;
                }

                intentosPerdidos = value;

                OnPropertyChanged(nameof(IntentosPerdidos));
            }
        }


        public string UsuarioClave
        {
            get { return usuarioClave; }
            set 
            {
                if (usuarioClave == value)
                {
                    return;
                }

                usuarioClave = value;

                OnPropertyChanged(nameof(UsuarioClave));
            }
        }


        public string NumeroTelefono
        {
            get { return numeroTelefono; }
            set 
            {
                if (numeroTelefono == value)
                {
                    return;
                }

                numeroTelefono = value;

                OnPropertyChanged(nameof(NumeroTelefono));
            }
        }


        public string Apellido
        {
            get { return apellido; }
            set 
            {
                if (apellido == value)
                {
                    return;
                }

                apellido = value;

                OnPropertyChanged(nameof(Apellido));
            }
        }


        public string PrimerNombre
        {
            get { return primerNombre; }
            set
            {
                if (primerNombre == value)
                {
                    return;
                }

                primerNombre = value;

                OnPropertyChanged(nameof(PrimerNombre));
            }
        }


        public string NombreDeUsuario
        {
            get { return nombreDeUsuario; }
            set 
            {
                if (nombreDeUsuario == value)
                {
                    return;
                }

                nombreDeUsuario = value;

                OnPropertyChanged(nameof(NombreDeUsuario));
            }
        }

        Crypto MiEncriptador { get; set; }

        public Command AgregarUsuarioCommand { get; }
        public UserViewModelV2()
        {
            MiUsuario = new User();
            MiEncriptador = new Crypto();

            //implementacion de los command
            AgregarUsuarioCommand = new Command(async () => await AgregarUsuario(NombreDeUsuario,PrimerNombre,Apellido,NumeroTelefono,UsuarioClave,EmailRespaldo,TrabajoDescripcion,1,1,1));
        }

        //A diferencia del vm original, acá definimos las acciones por medio de Comand(s)
        //si la funcionalidad es muy compleja y se requiere de un manejo más detallado, igual se puede usar como lo
        //vimos en el vm original

        //escribimos las funciones de forma similar sino identica a la version original del vm
        //agregamos una funcion para agregar usuario
        public async Task<bool> AgregarUsuario(string pUserName,
                                         string pFirstName,
                                         string pLastName,
                                         string pPhoneNumber,
                                         string pUserPassword,
                                         string pBackUpEmail,
                                         string pJobDescription,
                                         int pUserStatusId = 1,
                                         int pCountryId = 1,
                                         int pUserRolId = 1)
        {
            if (IsBusy) return false;

            IsBusy = true;

            try
            {

                //todo:encriptar el password ya sea aca o en el modelo

                MiUsuario.UserName = pUserName;
                MiUsuario.FirstName = pFirstName;
                MiUsuario.LastName = pLastName;
                MiUsuario.PhoneNumber = pPhoneNumber;

                string EncriptedPassword = MiEncriptador.EncriptarEnUnSentido(pUserPassword);
                MiUsuario.UserPassword = EncriptedPassword;

                MiUsuario.BackUpEmail = pBackUpEmail;
                MiUsuario.JobDescription = pJobDescription;
                MiUsuario.UserStatusId = pUserStatusId;
                MiUsuario.CountryId = pCountryId;
                MiUsuario.UserRoleId = pUserRolId;

                MiUsuario.UserRole.UserRoleId = pUserRolId;
                MiUsuario.UserRole.UserRole1 = "Role";

                bool R = await MiUsuario.AddNewUser();
                return R;


            }
            catch (Exception)
            {

                return false;
            }
            finally
            { IsBusy = false; }

        }



    }
}
