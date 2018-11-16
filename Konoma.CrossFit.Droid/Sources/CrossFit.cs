﻿
using System;

namespace Konoma.CrossFit
{
    public static class CrossFitExtensions
    {
        public static void Initialize(this CrossFit.Context context, Action<PlatformServiceRegistry> registrations)
        {
            var registry = context.ServiceRegistry;

            registry.RegisterService<IDeviceInfo>(() => new DeviceInfo());
            registry.RegisterService<IThreading>(() => new Threading());

            registrations?.Invoke(registry);

            context.Initialized = true;
        }
    }
}