using NUnit.Framework;
using ServiceLayer.ServiceLayer;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void SinglePropertyInteger()
        {
            var entity = new Entity<MockEntity>();
            var result = string.Empty;

            result = entity.Where(c => c.IntProperty1 > 50);
            Assert.AreEqual("IntProperty1 gt 50", result);

            result = entity.Where(c => c.IntProperty1 < 50);
            Assert.AreEqual("IntProperty1 lt 50", result);

            result = entity.Where(c => c.IntProperty1 >= 50);
            Assert.AreEqual("IntProperty1 ge 50", result);

            result = entity.Where(c => c.IntProperty1 <= 50);
            Assert.AreEqual("IntProperty1 le 50", result);

            result = entity.Where(c => c.IntProperty1 == 50);
            Assert.AreEqual("IntProperty1 eq 50", result);

            result = entity.Where(c => c.IntProperty1 != 50);
            Assert.AreEqual("IntProperty1 ne 50", result);

            result = entity.Where(c => !(c.IntProperty1 > 50));
            Assert.AreEqual("not (IntProperty1 gt 50)", result);

            result = entity.Where(c => !(c.IntProperty1 < 50));
            Assert.AreEqual("not (IntProperty1 lt 50)", result);

            result = entity.Where(c => !(c.IntProperty1 >= 50));
            Assert.AreEqual("not (IntProperty1 ge 50)", result);

            result = entity.Where(c => !(c.IntProperty1 <= 50));
            Assert.AreEqual("not (IntProperty1 le 50)", result);

            result = entity.Where(c => !(c.IntProperty1 == 50));
            Assert.AreEqual("not (IntProperty1 eq 50)", result);

            result = entity.Where(c => !(c.IntProperty1 != 50));
            Assert.AreEqual("not (IntProperty1 ne 50)", result);
        }

        [Test]
        public void TwoPropertyInteger()
        {
            var entity = new Entity<MockEntity>(); 
            var result = string.Empty;

            result = entity.Where(c => c.IntProperty1 > 50 && c.IntProperty2 > 100);
            Assert.AreEqual("(IntProperty1 gt 50) and (IntProperty2 gt 100)", result);

            result = entity.Where(c => c.IntProperty1 > 50 || c.IntProperty2 > 100);
            Assert.AreEqual("IntProperty1 gt 50 or IntProperty2 gt 100", result);

            result = entity.Where(c => (c.IntProperty1 > 50 || c.IntProperty1 < 20) && c.IntProperty2 > 100);
            Assert.AreEqual("(IntProperty1 gt 50 or IntProperty1 lt 20) and (IntProperty2 gt 100)", result);

            result = entity.Where(c => !(c.IntProperty1 > 50 || c.IntProperty1 < 20) && c.IntProperty2 > 100);
            Assert.AreEqual("(not (IntProperty1 gt 50 or IntProperty1 lt 20)) and (IntProperty2 gt 100)", result);
        }

        [Test]
        public void SinglePropertyString()
        {
            var entity = new Entity<MockEntity>();
            var result = string.Empty;

            result = entity.Where(c => c.StringProperty1 == "TESTE");
            Assert.AreEqual("StringProperty1 eq 'TESTE'", result);

            result = entity.Where(c => c.StringProperty1.Equals("TESTE"));
            Assert.AreEqual("StringProperty1 eq 'TESTE'", result);

            result = entity.Where(c => c.StringProperty1 != "TESTE");
            Assert.AreEqual("StringProperty1 ne 'TESTE'", result);

            result = entity.Where(c => c.StringProperty1.Contains("TESTE"));
            Assert.AreEqual("contains(StringProperty1, 'TESTE')", result);

            result = entity.Where(c => c.StringProperty1.EndsWith("TESTE"));
            Assert.AreEqual("endswith(StringProperty1, 'TESTE')", result);

            result = entity.Where(c => c.StringProperty1.StartsWith("TESTE"));
            Assert.AreEqual("startswith(StringProperty1, 'TESTE')", result);

            result = entity.Where(c => !(c.StringProperty1 == "TESTE"));
            Assert.AreEqual("not (StringProperty1 eq 'TESTE')", result);

            result = entity.Where(c => !(c.StringProperty1.Equals("TESTE")));
            Assert.AreEqual("not (StringProperty1 eq 'TESTE')", result);

            result = entity.Where(c => !(c.StringProperty1 != "TESTE"));
            Assert.AreEqual("not (StringProperty1 ne 'TESTE')", result);

            result = entity.Where(c => !(c.StringProperty1.Contains("TESTE")));
            Assert.AreEqual("not (contains(StringProperty1, 'TESTE'))", result);

            result = entity.Where(c => !(c.StringProperty1.EndsWith("TESTE")));
            Assert.AreEqual("not (endswith(StringProperty1, 'TESTE'))", result);

            result = entity.Where(c => !(c.StringProperty1.StartsWith("TESTE")));
            Assert.AreEqual("not (startswith(StringProperty1, 'TESTE'))", result);

            // result = entity.Where(c => c.StringProperty1.Length > 7); //TODO
        }

        [Test]
        public void TwoPropertyString()
        {
            var entity = new Entity<MockEntity>();
            var result = string.Empty;

            result = entity.Where(c => c.StringProperty1 == "TESTE" && c.StringProperty2 == "TESTE");
            Assert.AreEqual("(StringProperty1 eq 'TESTE') and (StringProperty2 eq 'TESTE')", result);

            result = entity.Where(c => c.StringProperty1 == "TESTE" || c.StringProperty2 == "TESTE");
            Assert.AreEqual("StringProperty1 eq 'TESTE' or StringProperty2 eq 'TESTE'", result);

            result = entity.Where(c => (c.StringProperty1 == "TESTE" || c.StringProperty1 == "TESTE2") && c.StringProperty2 == "TESTE2");
            Assert.AreEqual("(StringProperty1 eq 'TESTE' or StringProperty1 eq 'TESTE2') and (StringProperty2 eq 'TESTE2')", result);

            result = entity.Where(c => !(c.StringProperty1 == "TESTE" || c.StringProperty1 == "TESTE2") && c.StringProperty2 == "TESTE2");
            Assert.AreEqual("(not (StringProperty1 eq 'TESTE' or StringProperty1 eq 'TESTE2')) and (StringProperty2 eq 'TESTE2')", result);
        }
    }
}