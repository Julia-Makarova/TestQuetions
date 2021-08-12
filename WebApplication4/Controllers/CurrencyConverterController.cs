using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication4.models;
using WebApplication4.App_Data;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyConverterController : ControllerBase
    {


        [HttpPost()]
        public object Create(List<Product> products)
        {
           
            try
            {
                foreach (Product item in products)
                {
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        throw new Exception("Отсутствует название продукта в позиции - " + products.IndexOf(item));
                    }

                    if (item.PriceUSD < 0)
                    {
                        throw new Exception("Отрицательное значение PriceUSD в позиции  - " + products.IndexOf(item));
                    }
                }

                List<string> productIds = new List<string>();

                foreach (Product item in products)
                { 
                    DataBase.products.Add(item.Id, item);
                    productIds.Add(item.Id);
                }

                Ok();
                return productIds;
            }
            catch (Exception ex)
            {
                Problem(ex.StackTrace, "App", 500, ex.Message);
                return ex.Message;
            }
        }

        [HttpGet]
        public object GetAll()
        {
            return DataBase.products.Values;
        }

        [HttpGet("{id}")]
        public object GetProduct(string id)
        {
            try
            {
                if (!DataBase.products.ContainsKey(id))
                {
                    throw new Exception("Отутствует запись с идентификатором " + id);
                }

                Ok();

                return DataBase.products[id];
            } 
            catch (Exception ex)
            {
                Problem(ex.StackTrace, "App", 500, ex.Message);
                return ex.Message;
            }
        }
        [HttpDelete]
        public object Delete(string id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>() 
            { 
                ["code"] = 0,
                ["message"] = "",
                ["data"] = null
            };

            try
            {
                if (!DataBase.products.ContainsKey(id))
                {
                    throw new Exception("Отутствует запись с идентификатором " + id);
                }
                DataBase.products.Remove(id);
                Ok();
                return result;
            }
            catch (Exception ex)
            {
                Problem(ex.StackTrace, "App", 500, ex.Message);
                return ex.Message;
            }
        }

    }
}
