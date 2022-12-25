using DotNet7_WebAPI.Model;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNet7_WebAPI.Service
{
    public class scenarioModel
    {
        public int level { get; set; }
        public string file { get; set; }
    }

    public class ScenarioConfig
    {
        public Dictionary<int, string> Scenarios { get; set; }
    }

    public class ScenarioService
    {
        List<scenarioModel> _scenarioList;
        Dictionary<int, string> _scenarioDic;

        public ScenarioService(IConfiguration configuration)
        {
            _scenarioList = configuration.GetSection("ScenarioConfig").Get<List<scenarioModel>>();
            //_dir = scenarioConfig.Value.Dir;
            //_levels = scenarioConfig.Value.Levels;
            // key, 읽은파일내용(json)형태 딕셔너리로 저장.
            _scenarioDic = new Dictionary<int, string>();
            foreach (scenarioModel scenario in _scenarioList)
            {
                using (StreamReader iStreamReader = new StreamReader(new FileStream(scenario.file, FileMode.Open)))
                {
                    _scenarioDic.Add(scenario.level, iStreamReader.ReadToEnd());
                }
                //_scenariosDic.Add(scenario.level, scenario.file);
            }
            return;
        }
        public RtScenarioService GetScenario(int level)
        {
            RtScenarioService rt = new RtScenarioService();
            rt.errorCode = ErrorCode.NoError;
            try
            {
                rt.Scenario = _scenarioDic[level];
            }
            catch (KeyNotFoundException ex)
            {
                rt.errorCode = ErrorCode.WrongLevelReq;
            }
            catch(Exception ex)
            {
                rt.errorCode = ErrorCode.NotDefindedError;
            }
            return rt;
        }

    }
}
