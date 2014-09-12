using System;
using NUnit.Framework;
using System.Linq;

namespace CallPlanKata
{
    public class AgentAssigningStep :IStep
	{
        private AgentsService _agentsService;

        public AgentAssigningStep(AgentsService agentsService)
        {
            _agentsService = agentsService;
        }
        public void Execute(Interaction interaction)
        {
            var agents = _agentsService.GetAvailableAgentsFromGroupForInteraction(interaction);
            var assignedAgent = agents.FirstOrDefault();

            if (assignedAgent != null)
            {
                if (interaction.Type == InteractionType.call)
                {
                    assignedAgent.IsHandlingCall = true;
                }
                else if(interaction.Type == InteractionType.email)
                {
                    assignedAgent.NumberOfAssignedEmails++;
                }

                interaction.Summary += string.Format("and Agent \"{0}\"\"", assignedAgent.Id); 
            }
            else
            {
                interaction.Summary += "all agents are busy";
            }
        }
	}
}

