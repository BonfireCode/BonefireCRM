using BonefireCRM.SourceGenerator.Tests.Common;

namespace BonefireCRM.SourceGenerator.Tests
{
    public class FilterGeneratorTests
    {
        private readonly string _attributeMarker;

        public FilterGeneratorTests()
        {
            _attributeMarker = """
            namespace BonefireCRM.SourceGenerator
            {
                [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
                public class ApplyFilteringAttribute : Attribute
                {
                    public Type EntityType { get; set; }

                    public ApplyFilteringAttribute(Type entityType)
                    {
                        EntityType = entityType;
                    }
                }
            }
            """;
        }

        [Fact]
        public Task GeneratesClassCorrectly()
        {
            var generator = new FilterGenerator();
            var source = $$"""
                using System;
                using System.Collections.Generic;
                using BonefireCRM.SourceGenerator;
                using BonefireCRM.Targets;

                {{_attributeMarker}}

                namespace BonefireCRM.Definitions
                {
                    [ApplyFiltering(typeof(MyEntity))]
                    public class TestClass
                    {
                        public Guid? Id { get; set; }
                        public string? FirstName { get; set; }
                        public string? LastName { get; set; }
                        public DateTime? Age { get; set; }
                        public int? Count { get; set; }

                        public string SortBy { get; set; }
                        public string SortDirection { get; set; } = "ASC";                   
                    }
                }

                namespace BonefireCRM.Targets
                {
                    public class MyEntity
                    {
                        public Guid? Id { get; set; }
                        public string? FirstName { get; set; }
                        public string? LastName { get; set; }
                        public DateTime? Age { get; set; }
                        public int? Count { get; set; }
                        public int[] Counters { get; set; }
                        public List<int> Counters2 { get; set; }
                    }
                }
                """;

            var result = TestHelper.Verify(source, generator);

            return Verify(result).UseDirectory("Snapshots");
        }
    }
}
