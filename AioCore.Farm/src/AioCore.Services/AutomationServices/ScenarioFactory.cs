using AioCore.Services.AutomationServices.FacebookScenarios;
using AioCore.Services.AutomationServices.TiktokScenarios;

namespace AioCore.Services.AutomationServices;

public class ScenarioFactory
{
    //Facebook
    public class FacebookScenarioFactory
    {
        private readonly Dictionary<FacebookScenarios.StepType, IFacebookScenario> _processors;

        public FacebookScenarioFactory(IEnumerable<IFacebookScenario> processors)
        {
            _processors = processors.ToDictionary(t => t.StepType, t => t);
        }

        public IFacebookScenario? GetProcessor(FacebookScenarios.StepType type)
        {
            var tryGet = _processors.TryGetValue(type, out var value);
            return tryGet ? value : default!;
        }
    }
    ///Tiktok
    public class TiktokScenarioFactory
    {
        private readonly Dictionary<TiktokScenarios.StepType, ITiktokScenario> _processors;
        public TiktokScenarioFactory(IEnumerable<ITiktokScenario> processors)
        {
            _processors = processors.ToDictionary(t => t.StepType, t => t);
        }

        public ITiktokScenario? GetProcessor(TiktokScenarios.StepType type)
        {
            var tryGet = _processors.TryGetValue(type, out var value);
            return tryGet ? value : default!;
        }
    }
}