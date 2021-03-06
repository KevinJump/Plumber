﻿using System;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Constants = Workflow.Helpers.Constants;

namespace Workflow
{
    public class RouteHandler : UmbracoVirtualNodeRouteHandler
    {
        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            if (null == requestContext) return null;

            string path = requestContext.HttpContext.Request.Url.GetAbsolutePathDecoded();

            if (!path.StartsWith(Constants.PreviewRouteBase)) return null;

            string[] segments = path.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries);

            // domain // workflow-preview // node // user // task // guid
            if (segments.Length != 5)
            {
                return null;
            }

            IPublishedContent node = umbracoContext.ContentCache.GetById(int.Parse(segments[1]));
            return node;
        }
    }
}
