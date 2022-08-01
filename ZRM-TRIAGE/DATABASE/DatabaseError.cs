using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public class DatabaseError
    {
        public static void Show(string e)
        {
           Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new DatabaseErrorPage(e));
        }
    }
}
