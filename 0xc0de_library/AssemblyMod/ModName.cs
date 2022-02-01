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
    public sealed class ModNameAttribute : Attribute
    {
        private string m_title;

        [__DynamicallyInvokable]
        public ModNameAttribute(string title) => this.m_title = title;

        [__DynamicallyInvokable]
        public string Title
        {
            [__DynamicallyInvokable]
            get => this.m_title;
        }
    }


}
