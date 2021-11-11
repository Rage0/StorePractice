using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StorePractice.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class SessionCategory : LineCategories
    {
        public static LineCategories GetCategories(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            SessionCategory sessionCategory = session?.GetJson<SessionCategory>("Categories")
                ?? new SessionCategory();

            sessionCategory.Session = session;

            return sessionCategory;
        }

        [JsonIgnore]
        ISession Session;
        public override void AddCategory(Category category)
        {
            base.AddCategory(category);
            Session.SetJson("Categories", this);
        }

        public override void RemoveCategory(Category category)
        {
            base.RemoveCategory(category);
            Session.SetJson("Categories", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Categories");
        }
    }
}
