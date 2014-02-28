using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XBBS.Core
{
    public class ThemeViewEngine : BuildManagerViewEngine
    {
        public static string ThemeName
        {
            get
            {
                object re = HttpContext.Current.Items["ThemeName"];
                if (re == null)
                {
                    re = "default";
                }
                HttpContext.Current.Items["ThemeName"] = re;
                return re.ToString();
            }
            set { HttpContext.Current.Items["ThemeName"] = value; }
        }

        public ThemeViewEngine()
            : this(null)
        {
        }

        public ThemeViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            //throw new Exception(SkinName);
            base.AreaViewLocationFormats = new string[] { "~/Areas/{2}/View/{1}/{0}.cshtml", "~/Areas/{2}/View/{1}/{0}.vbhtml", "~/Areas/{2}/View/Shared/{0}.cshtml", "~/Areas/{2}/View/Shared/{0}.vbhtml" };
            base.AreaMasterLocationFormats = new string[] { "~/Areas/{2}/View/{1}/{0}.cshtml", "~/Areas/{2}/View/{1}/{0}.vbhtml", "~/Areas/{2}/View/Shared/{0}.cshtml", "~/Areas/{2}/View/Shared/{0}.vbhtml" };
            base.AreaPartialViewLocationFormats = new string[] { "~/Areas/{2}/View/{1}/{0}.cshtml", "~/Areas/{2}/View/{1}/{0}.vbhtml", "~/Areas/{2}/View/Shared/{0}.cshtml", "~/Areas/{2}/View/Shared/{0}.vbhtml" };
            base.ViewLocationFormats = new string[] { "~/theme/" + ThemeName + "/{1}/{0}.cshtml", "~/theme/" + ThemeName + "/{1}/{0}.vbhtml", "~/theme/" + ThemeName + "/Shared/{0}.cshtml", "~/theme/" + ThemeName + "/Shared/{0}.vbhtml" };
            base.MasterLocationFormats = new string[] { "~/theme/" + ThemeName + "/{1}/{0}.cshtml", "~/theme/" + ThemeName + "/{1}/{0}.vbhtml", "~/theme/" + ThemeName + "/Shared/{0}.cshtml", "~/theme/" + ThemeName + "/Shared/{0}.vbhtml"   };
            base.PartialViewLocationFormats = new string[] { "~/theme/" + ThemeName + "/{1}/{0}.cshtml", "~/theme/" + ThemeName + "/{1}/{0}.vbhtml", "~/theme/" + ThemeName + "/Shared/{0}.cshtml", "~/theme/" + ThemeName + "/Shared/{0}.vbhtml"  };
            base.FileExtensions = new string[] { "cshtml", "vbhtml" };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string layoutPath = null;
            bool runViewtartPages = false;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, layoutPath, runViewtartPages, fileExtensions, base.ViewPageActivator);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string layoutPath = masterPath;
            bool runViewtartPages = true;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, layoutPath, runViewtartPages, fileExtensions, base.ViewPageActivator);
        }
    }
}