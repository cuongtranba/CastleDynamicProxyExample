using System;
using NUnit.Framework;

namespace CastleDynamicProxy.Test
{
    [TestFixture]
    public class ObjectLoggingTest
    {
        [Test]
        public void ShouldTrackingWhenAssigningValuesProperties()
        {
            var Person = LoggingProxy.Wrap(new Person());
            Person.Name = "Cuong";
            StringAssert.AreEqualIgnoringCase("method call: set_Name - parameter: Cuong",LoggingProxy.ChangeInfor);
        }
        [Test]
        public void ShouldNotTrackingWhenCallGetMethod()
        {
            var person = LoggingProxy.Wrap(new Person());
            var name = person.Name;
            StringAssert.AreEqualIgnoringCase(null, LoggingProxy.ChangeInfor);
        }
    }
}