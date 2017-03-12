using HBD.Mef.Core;
// <copyright file="ListDictionaryFactory.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using Microsoft.Pex.Framework;

namespace HBD.Mef.Core
{
    /// <summary>A factory for HBD.Mef.Core.ListDictionary`2[System.Int32,System.Int32] instances</summary>
    public static partial class ListDictionaryFactory
    {
        /// <summary>A factory for HBD.Mef.Core.ListDictionary`2[System.Int32,System.Int32] instances</summary>
        [PexFactoryMethod(typeof(ListDictionary<int, int>))]
        public static ListDictionary<int, int> Create()
        {
            ListDictionary<int, int> listDictionary = new ListDictionary<int, int>();
            return listDictionary;

            // TODO: Edit factory method of ListDictionary`2<Int32,Int32>
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
