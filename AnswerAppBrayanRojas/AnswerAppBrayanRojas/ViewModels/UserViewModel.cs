using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnswerAppBrayanRojas.Models;

namespace AnswerAppBrayanRojas.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        //propiedades
        public User MyUser { get; set; }
        public Tools.Crypto MyCrypto { get; set; }

        public UserViewModel()
        {
            MyUser = new User();

            MyCrypto = new Tools.Crypto();

            //todo:Implementar los Command posteriormente

        }

        //todo: cargar los roles

        //agregamos una funcion para agregar el usuario
        public async Task<bool> AddUser(string pUserName,
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

                MyUser.UserName = pUserName;
                MyUser.FirstName = pFirstName;
                MyUser.LastName = pLastName;
                MyUser.PhoneNumber = pPhoneNumber;

                string EncriptedPassword = MyCrypto.EncriptarEnUnSentido(pUserPassword);
                MyUser.UserPassword = EncriptedPassword;

                MyUser.BackUpEmail = pBackUpEmail;
                MyUser.JobDescription = pJobDescription;
                MyUser.UserStatusId = pUserStatusId;
                MyUser.CountryId = pCountryId;
                MyUser.UserRoleId = pUserRolId;

                bool R = await MyUser.AddNewUser();
                return R;


            }
            catch (Exception)
            {

                return false;
            }
            finally 
            { IsBusy = false; }     
            
        }


        //funcion para validar el permiso de acceso del usuario
        public async Task<bool> ValidateUserAccess(string pEmail, string pPassword)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                string EncriptedPassword = MyCrypto.EncriptarEnUnSentido(pPassword);

                MyUser.UserName = pEmail;
                MyUser.UserPassword = EncriptedPassword;

                bool R = await MyUser.ValidateUserAccess();
                return R;
            }
            catch (Exception)
            {

                return false;
            }
            finally
            { IsBusy = false; }
        }

        //cargar roles
        public async Task<List<UserRole>> LoadRoles()
        {
            if (IsBusy) return null;
            IsBusy = true;

            try
            {
                //realizar la limpieza
                MyUser = new User();

                List<UserRole> RoleList = new List<UserRole>();

                RoleList = await MyUser.MyRole.LoadRoles();

                if (RoleList != null)
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
            finally
            { IsBusy = false; }
        }


    }
}
