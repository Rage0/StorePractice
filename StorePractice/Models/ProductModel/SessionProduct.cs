using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StorePractice.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class SessionProduct : ProductInteraction
    {
        public static ProductInteraction GetSessionProduct(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            SessionProduct sessionProduct = session.GetJson<SessionProduct>("SessionProduct")
                ?? new SessionProduct();

            sessionProduct.Session = session;
            return sessionProduct;
        }

        [JsonIgnore]

        ISession Session;

        public override void AddProduct(Product product)
        {
            base.AddProduct(product);
            Session.SetJson("SessionProduct", this);
        }

        public void RemoveProduct()
        {
            Session.Remove("SessionProduct");
        }

        public override void AddCategoryToProduct(Category category)
        {
            base.AddCategoryToProduct(category);
            Session.SetJson("SessionProduct", this);
        }

        public override void RemoveCategoryToProduct(Category category)
        {
            base.RemoveCategoryToProduct(category);
            Session.SetJson("SessionProduct", this);
        }

        public override void ClearCategoriesToProduct()
        {
            base.ClearCategoriesToProduct();
            Session.SetJson("SessionProduct", this);
        }
    }
}
