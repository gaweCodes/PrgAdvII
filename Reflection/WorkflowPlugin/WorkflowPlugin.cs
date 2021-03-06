﻿using System;
using Reflection.PluginInterface;

namespace Reflection.WorkflowPlugin 
{
    public class Plugin : IPlugin 
    {
        public string Name { get; } = "WorkflowPlugin";
        public bool Execute() 
        {
            Console.WriteLine($"Execute {this.Name}.");
            return true;
        }
    }
}
