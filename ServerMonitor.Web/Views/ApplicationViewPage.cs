using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerMonitor.Web
{
    public abstract class ApplicationViewPage : WebViewPage
    {
    }

    public abstract class ApplicationViewPage<T> : WebViewPage<T>
    {

        protected override void InitializePage()
        {
            SetViewBagDefaultProperties();
            base.InitializePage();
        }

        private void SetViewBagDefaultProperties()
        {

            ViewBag.VersionString = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

    }
}