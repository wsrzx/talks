using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TalkToMeAlexa.FuncTown
{
    public static class SkillHttpTrigger
    {
        [FunctionName("skill")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            var skillRequest = await Skill.ParseSkillRequestFromJson(req);
            //var requestType = skillRequest.GetRequestType();
            SkillResponse skillResponse = null;

            if (skillRequest.Request is IntentRequest intentRequest)
                switch (intentRequest.Intent.Name)
                {
                    case "alugar carro":
                        skillResponse = Skill.BuildRentcarsResponse();
                        break;
                    case "AMAZON.StopIntent":
                    case "AMAZON.CancelIntent":
                        skillResponse = Skill.BuildGoodbyeResponse();
                        break;
                    case "AMAZON.HelpIntent":
                        skillResponse = Skill.BuildHelpResponse();
                        break;
                    default:
                        skillResponse = ResponseBuilder.Empty();
                        skillResponse.Response.ShouldEndSession = false;
                        break;
                }

            return new OkObjectResult(skillResponse);
        }

        public static class Skill
        {
            public static SkillResponse BuildRentcarsResponse()
            {
                var text = "Para alugar um carro você precisa dizer a data de inicio e fim!";
                var responseBuilder = ResponseBuilder.Tell(text);
                responseBuilder.Response.ShouldEndSession = false;
                return responseBuilder;
            }

            public static SkillResponse BuildGoodbyeResponse()
            {
                return ResponseBuilder.Tell("Até mais mano, da um salve para os trutas, é nóis!");
            }

            public static SkillResponse BuildHelpResponse()
            {
                var help = "Presta atenção! Você precisa dizer o que quer, para que eu te ajude!";
                var responseBuilder = ResponseBuilder.Tell(help);
                responseBuilder.Response.ShouldEndSession = false;
                return responseBuilder;
            }

            public static async Task<SkillRequest> ParseSkillRequestFromJson(HttpRequest req)
            {
                return JsonConvert.DeserializeObject<SkillRequest>(await req.ReadAsStringAsync());
            }
        }
    }
}