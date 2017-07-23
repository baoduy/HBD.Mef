using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;

namespace HBD.ServiceLocators.StTests
{
    [TestClass]
    public class ServiceLocatorTests
    {
        [TestMethod]
        public void Access_The_Current_Without_Initilize()
        {
            Action a = () => ServiceLocator.Current.GetInstance(null);
            a.ShouldThrow<InvalidOperationException>();
            ServiceLocator.IsServiceLocatorSet.Should().BeFalse();
        }

        [TestMethod]
        public void Initilize_Null_Instace()
        {
            Action a = () => ServiceLocator.SetServiceLocator((IServiceLocator)null);
            a.ShouldThrow<ArgumentNullException>();

            Action b = () => ServiceLocator.SetServiceLocator((Func<IServiceLocator>)null);
            b.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Lazy_Initialize_Service()
        {
            var count = 0;
            var mock = new Mock<ServiceLocatorBase> { CallBase = true };

            ServiceLocator.SetServiceLocator(() =>
            {
                count++;

                if (count <= 1)
                    return null;

                mock.Protected().Setup<IEnumerable<object>>("DoGetAllInstances", typeof(object))
                .Returns(new List<string>()).Verifiable();
                mock.Protected().Setup<object>("DoGetInstance", typeof(object), "Test")
                .Returns(new object()).Verifiable();

                return mock.Object;
            });

            ServiceLocator.IsServiceLocatorSet.Should().BeTrue();

            Action a = () => ServiceLocator.Current.GetAllInstances(null);
            a.ShouldThrow<Exception>();

            Action b = () => ServiceLocator.Current.GetAllInstances(typeof(object));
            b.ShouldNotThrow();

            Action c = () => ServiceLocator.Current.GetInstance(typeof(object),"Test");
            c.ShouldNotThrow();

            mock.VerifyAll();
        }
    }
}
