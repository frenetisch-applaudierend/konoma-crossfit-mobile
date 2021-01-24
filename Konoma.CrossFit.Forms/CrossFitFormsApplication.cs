using System;

namespace Konoma.CrossFit.Forms
{
    public abstract class CrossFitFormsApplication<TApp, TMainPage> : Xamarin.Forms.Application
        where TApp : CrossFitApplication, new()
        where TMainPage : CrossFitMainPage, new()
    {
        protected void StartApplication()
        {
            MainPage = new TMainPage();
        }
    }

    public abstract class CrossFitFormsApplication<TApp> : CrossFitFormsApplication<TApp, CrossFitMainPage>
        where TApp : CrossFitApplication, new()
    {
    }
}
