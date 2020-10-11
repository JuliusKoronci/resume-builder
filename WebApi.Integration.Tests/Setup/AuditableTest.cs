using System;
using System.Linq;
using Domain.Common;
using Xunit;

namespace WebApi.Integration.Tests.Setup
{
    public static class AuditableTest
    {
        public static void EnsureNotModifiedAuditableEntity(AuditableEntity entity)
        {
            Assert.NotEmpty(entity.CreatedBy);
            Assert.IsType<DateTime>(entity.Created);
            Assert.Null(entity.LastModifiedBy);
            Assert.Null(entity.LastModified);
        }

        public static void EnsureModifiedAuditableEntity(AuditableEntity entity)
        {
            Assert.NotEmpty(entity.CreatedBy);
            Assert.IsType<DateTime>(entity.Created);
            Assert.NotEmpty(entity.LastModifiedBy);
            Assert.IsType<DateTime>(entity.LastModified);
        }

        public static void EqualsIgnoreAuditableProps<TBaseType>(TBaseType entity1, TBaseType entity2)
            where TBaseType : AuditableEntity
        {
            var auditableProps = typeof(AuditableEntity).GetProperties();
            var entity1Props = entity1.GetType().GetProperties();
            var entity2Props = entity1.GetType().GetProperties();

            Assert.Equal(entity1Props.Length, entity2Props.Length);

            foreach (var prop in entity1Props)
            {
                var name = prop.Name;
                if (auditableProps.FirstOrDefault(a => a.Name == name) != null) continue;

                var value1 = entity1.GetType().GetProperty(name)?.GetValue(entity1, null);
                var value2 = entity2.GetType().GetProperty(name)?.GetValue(entity2, null);

                Assert.Equal(value1, value2);
            }
        }
    }
}
