﻿using Microsoft.AspNet.SignalR.Client;
using System;

namespace DesktopApp.Services
{
    public interface IConnectionService
    {
        bool HasConnection { get; }
        string ServerURL { get; set; }

        event Action<bool> HasConnectionChanged;
        event Action<string> ServerURLChanged;

        IHubProxy CreateHubProxy(string hubName);
        void Open();
        void Close();
    }
}