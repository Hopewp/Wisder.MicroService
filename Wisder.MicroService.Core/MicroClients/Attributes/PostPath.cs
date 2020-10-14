﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Core.MicroClients.Attributes
{
    /// <summary>
    /// Post请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PostPath : Attribute
    {
        // 请求路径
        public string Path { get; }
        public PostPath(string Path)
        {
            this.Path = Path;
        }
    }
}
