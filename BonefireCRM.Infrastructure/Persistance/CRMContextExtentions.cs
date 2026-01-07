using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Infrastructure.Persistance
{
    public static class CRMContextExtentions
    {
        public static void SeedDefaultData(this CRMContext context)
        {
            SeedPipelines(context);
            SeedLifecycleStages(context);
        }

        private static void SeedPipelines(CRMContext context)
        {
            if (context.Pipelines.Any())
            {
                return;
            }

            var defaultPipeline = new Pipeline
            {
                Name = "Default Sales Pipeline",
                IsDefault = true,
                Stages =
                [
                    new PipelineStage
                    {
                        Name = "Qualified",
                        OrderIndex = 1,
                        Status = null
                    },
                    new PipelineStage
                    {
                        Name = "Proposal",
                        OrderIndex = 2,
                        Status = null
                    },
                    new PipelineStage
                    {
                        Name = "Negotiation",
                        OrderIndex = 3,
                        Status = null
                    },
                    new PipelineStage
                    {
                        Name = "Closed Won",
                        OrderIndex = 4,
                        Status = DealClosureStatus.Won
                    },
                    new PipelineStage
                    {
                        Name = "Closed Lost",
                        OrderIndex = 5,
                        Status = DealClosureStatus.Lost
                    }
                ]
            };

            context.Pipelines.Add(defaultPipeline);
            context.SaveChanges();
        }

        private static void SeedLifecycleStages(CRMContext context)
        {
            if (context.LifecycleStages.Any())
            {
                return;
            }

            context.LifecycleStages.AddRange(
                [
                    new LifecycleStage { Name = "Lead" },
                    new LifecycleStage { Name = "Prospect" },
                    new LifecycleStage { Name = "Customer" },
                    new LifecycleStage { Name = "Evangelist" } // Advocate-type customers
                ]);
            context.SaveChanges();
        }
    }
}
