using AutoOrderSystem.Model;
using RESTClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Common
{
    public class ProductTypeDAL
    {
        private WebRequestSession _reqSession;
        public List<ProductType> TypeList
        {
            get
            {
                return GetProductTypes();
            }

        }
        public ProductTypeDAL(WebRequestSession session)
        {
            _reqSession = session;
            
        }
        private List<ProductType> GetProductTypes()
        {
            List<ProductType> types = new List<ProductType>();

            try
            {
                string sql = "select * from productTypes";
                DataTable dt = RemoteCall.RESTQuery(_reqSession, sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductType type = new ProductType()
                    {
                        TypeId = Convert.ToInt32(dt.Rows[i]["type_id"]),
                        TypeName = dt.Rows[i]["type_name"].ToString().Trim(),
                    };
                    types.Add(type);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return types;
        }
        public int AddProductType()
        {
            return 0;
        }
        public bool Exists(string productName)
        {
            bool b=this.TypeList.Exists(type => type.TypeName == productName);
            return b;
        }
        public int GetIndex(string productName)
        {
            return TypeList.Find(type =>
            {
                if (type.TypeName == productName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            ).TypeId;
        }
    }
}
