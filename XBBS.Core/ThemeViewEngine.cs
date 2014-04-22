using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XBBS.Core
{
    public class ThemeViewEngine : BuildManagerViewEngine
    {
        private static Dictionary<string, string> dicViewPath = new Dictionary<string, string>();
        public static string ThemeName
        {
            get
            {
                object obj = HttpContext.Current.Items["ThemeName"];
                if (obj == null)
                {
                    obj = "default";
                }
                HttpContext.Current.Items["ThemeName"] = obj;
                return obj.ToString();
            }
            set
            {
                HttpContext.Current.Items["ThemeName"] = value;
            }
        }
        public ThemeViewEngine()
            : this(null)
        {
        }
        public ThemeViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            base.AreaViewLocationFormats = new string[]
			{
				"~/Areas/{2}/View/{1}/{0}.cshtml", 
				"~/Areas/{2}/View/Shared/{0}.cshtml" 
			};
            base.AreaMasterLocationFormats = new string[]
			{
				"~/Areas/{2}/View/{1}/{0}.cshtml", 
				"~/Areas/{2}/View/Shared/{0}.cshtml" 
			};
            base.AreaPartialViewLocationFormats = new string[]
			{
				"~/Areas/{2}/View/{1}/{0}.cshtml", 
				"~/Areas/{2}/View/Shared/{0}.cshtml" 
			};
            base.ViewLocationFormats = new string[]
			{
				"~/theme/" + ThemeViewEngine.ThemeName + "/{1}/{0}.cshtml", 
				"~/theme/" + ThemeViewEngine.ThemeName + "/Shared/{0}.cshtml" 
			};
            base.MasterLocationFormats = new string[]
			{
				"~/theme/" + ThemeViewEngine.ThemeName + "/{1}/{0}.cshtml", 
				"~/theme/" + ThemeViewEngine.ThemeName + "/Shared/{0}.cshtml" 
			};
            base.PartialViewLocationFormats = new string[]
			{
				"~/theme/" + ThemeViewEngine.ThemeName + "/{1}/{0}.cshtml", 
				"~/theme/" + ThemeViewEngine.ThemeName + "/Shared/{0}.cshtml" 
			};
            base.FileExtensions = new string[]
			{
				"cshtml" 
			};
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string layoutPath = null;
            bool runViewStartPages = false;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, layoutPath, runViewStartPages, fileExtensions, base.ViewPageActivator);
        }
        private string PascalName(string name)
        {
            name = name.ToLower();
            char[] array = name.ToArray<char>();
            array[0] = char.ToUpper(array[0]);
            return new string(array);
        }
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            bool runViewStartPages = true;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, masterPath, runViewStartPages, fileExtensions, base.ViewPageActivator);
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("viewNameu不能为空");
            }
            string requiredString = controllerContext.RouteData.GetRequiredString("controller");
            string text = string.Concat(new string[]
			{
				"~/theme/",
				ThemeViewEngine.ThemeName,
				"/",
				this.PascalName(requiredString),
				"/",
				viewName.ToLower(),
				".cshtml"
			});
            if (!File.Exists(controllerContext.HttpContext.Server.MapPath(text)))
            {
                throw new FileNotFoundException(text);
            }
            return new ViewEngineResult(this.CreateView(controllerContext, text, ""), this);
        }
    }
}