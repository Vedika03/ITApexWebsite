using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITApexWebsite
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;

        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "ASaE5nRoxgrhMvkHwNftxKOUMJcbhsae3USMrdBjKfeulyX3jqkbm9I1uvL4U-L_OachNuQKgduLYIyp";
            clientSecret = "ELsQe_eh9OgsRRqxdcF2zCqEtAH6YuFKzLYjGjIHCht6hPLZAJE90czc0_4H1JH0EK8wNZy9YNLE-5hu";


        }

        private static Dictionary<string,string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accesstoken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return accesstoken;
        }

        public static APIContext GetAPIContext()
        {
            APIContext aPIContext = new APIContext(GetAccessToken());
            aPIContext.Config = getconfig();
            return aPIContext;
        }
    }
}