using EPiServer.Core;
using EPiServer.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Controllers
{
    public class BasePageController<T> : PageController<T> where T : PageData
    {
    }
}