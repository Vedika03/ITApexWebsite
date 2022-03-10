using ITApexWebsite.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITApexWebsite.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        DbITApexEntities context = new DbITApexEntities();

        public IPagedList<tblProduct> ListOfProducts { get; set; }
        public IPagedList<securityProduct> ListOfsecurityPro { get; set; }

        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search",search??(object)DBNull.Value)
            };
            IPagedList<tblProduct>data = context.Database.SqlQuery<tblProduct>("GetBySearch @search", param).ToList().ToPagedList(page??1,pageSize);
            return new HomeIndexViewModel()
            {
                ListOfProducts = data  /*_unitOfWork.GetRepositoryInstance<tblProduct>().GetAllRecords()*/
            };
        }


    }
}