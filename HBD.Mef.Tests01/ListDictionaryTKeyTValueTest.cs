using Microsoft.ExtendedReflection.DataAccess;
using System;
using HBD.Mef.Core;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Core.Tests
{
    /// <summary>This class contains parameterized unit tests for ListDictionary`2</summary>
    [TestClassAttribute]
    [PexClass(typeof(ListDictionary<, >))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ListDictionaryTKeyTValueTest
    {

        /// <summary>Test stub for Add(!0)</summary>
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(TermDestructionException))]
        public void AddTest<TKey, TValue>([PexAssumeUnderTest]ListDictionary<TKey, TValue> target, TKey key)
        {
            target.Add(key);
            // TODO: add assertions to method ListDictionaryTKeyTValueTest.AddTest(ListDictionary`2<!!0,!!1>, !!0)
        }

        /// <summary>Test stub for Add(!0, !1)</summary>
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        public void AddTest<TKey, TValue>(
            [PexAssumeUnderTest]ListDictionary<TKey, TValue> target,
            TKey key,
            TValue value
        )
        {
            target.Add(key, value);
            // TODO: add assertions to method ListDictionaryTKeyTValueTest.AddTest(ListDictionary`2<!!0,!!1>, !!0, !!1)
        }
    }
}
