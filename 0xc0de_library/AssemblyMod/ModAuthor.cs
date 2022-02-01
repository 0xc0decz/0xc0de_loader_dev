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
    public sealed class ModAuthorAttribute : Attribute
    {
        private string m_version;

        [__DynamicallyInvokable]
        public ModAuthorAttribute(string name) => this.m_version = name;

        [__DynamicallyInvokable]
        public string Name
        {
            [__DynamicallyInvokable]
            get => this.m_version;
        }
    }
}

