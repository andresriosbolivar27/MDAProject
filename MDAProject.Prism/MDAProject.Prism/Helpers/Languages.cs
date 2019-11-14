using MDAProject.Prism.Interfaces;
using MDAProject.Prism.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MDAProject.Prism.Helpers
{
    public class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Password => Resource.Password;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Rememberme => Resource.Rememberme;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string Login => Resource.Login;

        public static string Register => Resource.Register;

        public static string Loading => Resource.Loading;
    }
}
