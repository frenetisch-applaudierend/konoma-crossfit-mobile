﻿using Konoma.CrossFit;
using TemperatureConverter.Core.Application;
using TemperatureConverter.Core.Services;
using TemperatureConverter.Native.iOS.Application.Converter;
using TemperatureConverter.Native.iOS.Application.Login;
using TemperatureConverter.Native.iOS.Services;

namespace TemperatureConverter.Native.iOS.Application
{
    public class AppDelegate : CrossFitAppDelegate<TemperatureConverterCoordinator>
    {
        protected override void RegisterPlatformServices(IServiceRegistration services)
        {
            services.AddSingleton<IPreferencesService, PreferencesService>();
        }

        protected override void ConnectNavigationPoints(TemperatureConverterCoordinator coordinator)
        {
            coordinator.ShowLogin.Connect(
                Navigation
                    .Root(Window!, () => new LoginViewController())
                    .InNavigationController());
            coordinator.ShowHome.Connect(
                Navigation
                    .Root(Window!, () => new ConverterViewController())
                    .InNavigationController());
        }
    }
}