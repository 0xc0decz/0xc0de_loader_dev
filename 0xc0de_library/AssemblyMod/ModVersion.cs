using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;

namespace _0xc0de_library.AssemblyMod
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public sealed class ModVersionAttribute : Attribute
    {
        private string m_version;

        [__DynamicallyInvokable]
        public ModVersionAttribute(string version) => this.m_version = version;

        [__DynamicallyInvokable]
        public string Version
        {
            [__DynamicallyInvokable]
            get => this.m_version;
        }
    }
}

