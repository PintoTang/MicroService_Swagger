using System;
using System.Collections.Generic;
using System.Text;

namespace Swagger.Lib.Model
{
    public class ProjectModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 路由前缀
        /// </summary>
        public string RouteKey { set; get; }
    }
}
