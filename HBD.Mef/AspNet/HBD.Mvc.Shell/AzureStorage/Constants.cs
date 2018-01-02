#region using

using System;
using System.Configuration;

#endregion

namespace AzureStorage
{
    internal static class Constants
    {
        public static string AzureNodeUri
            => ConfigurationManager.AppSettings["AzureNode_Uri"]
               ?? Environment.GetEnvironmentVariable("AzureNode_Uri");

        public static string AzureNodeKey
            => ConfigurationManager.AppSettings["AzureNode_Key"]
               ?? Environment.GetEnvironmentVariable("AzureNode_Key");

        public static string AzureNodeDbId
            => ConfigurationManager.AppSettings["AzureNode_DbId"]
               ?? Environment.GetEnvironmentVariable("AzureNode_DbId");

        public static string AzureNodeCollectionId
            => ConfigurationManager.AppSettings["AzureNode_CollectionId"]
               ?? Environment.GetEnvironmentVariable("AzureNode_CollectionId");
    }
}