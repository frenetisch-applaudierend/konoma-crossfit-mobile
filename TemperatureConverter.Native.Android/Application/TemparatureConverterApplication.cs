using System;
using Android.App;
using Android.Runtime;
using Konoma.CrossFit;
using TemperatureConverter.Android.Services;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Services;

namespace TemperatureConverter.Android.Application
{
    [Application]
    public class TemparatureConverterApplication : CrossFitApplication<TemperatureConverterCoordinator>
    {
        protected TemparatureConverterApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public TemparatureConverterApplication()
        {
        }

        protected override void RegisterPlatformServices(IServiceRegistration services)
        {
            services.AddSingleton<IPreferencesService, PreferencesService>();
        }
    }
}
