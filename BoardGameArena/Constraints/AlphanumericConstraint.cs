using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoardGameArena.Constraints
{
    public class AlphanumericConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string patern = @"\d+";
            Match Match =  Regex.Match(values["id"].ToString(), patern);
            if (Match.Success)
            {
                return true;
            }

            return false;
        }
    }
}
