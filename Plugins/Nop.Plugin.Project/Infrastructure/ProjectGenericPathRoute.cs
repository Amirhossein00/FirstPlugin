using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Nop.Core.Data;
using Nop.Core.Domain.Localization;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Seo;

namespace Nop.Plugin.Projects.Infrastructure
{
    public class ProjectGenericPathRoute : LocalizedRoute
    {
        private readonly IRouter _targe;

        public ProjectGenericPathRoute(IRouter target, string routeName,string routeTemplate,RouteValueDictionary defaults,
            IDictionary<string,object> constraints,RouteValueDictionary dataTokens,IInlineConstraintResolver inlineConstraintResolver)
            :base (target,routeName,routeTemplate,defaults,constraints,dataTokens,inlineConstraintResolver)
        {
            _targe = target;
        }

        #region Utilities
        protected RouteValueDictionary GetRouteValues(RouteContext context)
        {
            //remove language code from the path if it's localized URL
            var path = context.HttpContext.Request.Path.Value;
            if (SeoFriendlyUrlsForLanguagesEnabled && path.IsLocalizedUrl(context.HttpContext.Request.PathBase, false, out Language _))
                path = path.RemoveLanguageSeoCodeFromUrl(context.HttpContext.Request.PathBase, false);

            //parse route data
            var routeValues = new RouteValueDictionary(ParsedTemplate.Parameters
                .Where(parameter => parameter.DefaultValue != null)
                .ToDictionary(parameter => parameter.Name, parameter => parameter.DefaultValue));
            var matcher = new TemplateMatcher(ParsedTemplate, routeValues);
            matcher.TryMatch(path, routeValues);

            return routeValues;
        }
        #endregion

        
        public override Task RouteAsync(RouteContext context)
        {
           
            if (!DataSettingsManager.DatabaseIsInstalled)
                return Task.CompletedTask;

            var routeValues = GetRouteValues(context);

            if (!routeValues.TryGetValue("GenericSeName", out object slugValue) || string.IsNullOrEmpty(slugValue as string))
                return Task.CompletedTask;

            var slug = slugValue as string;

            var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();

            var urlRecord = urlRecordService.GetBySlug(slug);

            var currentRouteData = new RouteData(context.RouteData);

            if (urlRecord.EntityName.ToLowerInvariant()=="project")
            {
                currentRouteData.Values[NopPathRouteDefaults.ControllerFieldKey] = "Project";
                currentRouteData.Values[NopPathRouteDefaults.ActionFieldKey] = "Detail";
                currentRouteData.Values[ProjectPathRouteDefault.ProjectFieldKey] = urlRecord.EntityId;
                currentRouteData.Values[NopPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
            }
            context.RouteData = currentRouteData;
            
            return _targe.RouteAsync(context);
        }
    }
}
