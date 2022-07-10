using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Redis.Contexts;
using Inter.Infrastructure.Redis.Mappers;
using Melberg.Infrastructure.Redis.Repository;

namespace Inter.Infrastructure.Redis.Repositories;
public class PlaneCacheRepository : RedisRepository<PlaneCacheContext>, IPlaneCacheRepository
{
    private TimeSpan FrameLifespan => new System.TimeSpan(0,0,45);
    private TimeSpan FinalPlaneRecordLifespan => TimeSpan.FromSeconds(60);
    public PlaneCacheRepository(PlaneCacheContext context) : base(context) { }

    public async Task<PlaneFrame> GetPlaneFrameAsync(long timestamp)
    {
        var key = $"plane_congregated_{timestamp}";
        var payload = await DB.StringGetAsync(key);
        var sourceDefinition = new PlaneSourceDefintion()
        {
            Node = "aggregate",
            Antenna = "aggregate"
        };
        var planes = payload.ToDomain(sourceDefinition);
        return planes; 
    }


    public async Task InsertCongregatedPlaneFrameAsync(PlaneFrame frame) => await DB.StringSetAsync(ToCongregatedKey(frame),frame.ToModel().ToPayload(),new TimeSpan(0,0,45));
    public async Task InsertPlaneFrameAsync(PlaneFrame frame) => await DB.StringSetAsync(ToKey(frame),frame.ToModel().ToPayload(),new TimeSpan(0,0,45));

    public async Task InsertPreCongregatedPlaneFrameAsync(PlaneFrame planeFrame)
    {
        var sourceDefinition = new PlaneSourceDefintion()
        {
            Node = planeFrame.Source,
            Antenna = planeFrame.Antenna
        };
        await DB.StringSetAsync(ToPreAggregateKey(sourceDefinition,planeFrame.Now),planeFrame.ToModel().ToPayload(),TimeSpan.FromSeconds(50));
    }


    public async IAsyncEnumerable<PlaneFrame> GetPreCongregatedPlaneFramesAsync(long timestamp)
    {
        await foreach(var key in Server.KeysAsync(pattern:$"plane_preaggregate_*_*_{timestamp}"))
        {
            var keySections = key.ToString().Split("_");
            var sourceDefinition = new PlaneSourceDefintion()
            {
                Node = keySections[2],
                Antenna = keySections[3]
            };
            yield return (await DB.StringGetAsync(key)).ToDomain(sourceDefinition);
        }
        yield break;
    }


    public async Task<PlaneFrame> GetPreCongregatedPlaneFrameAsync(PlaneSourceDefintion source, long timestamp) =>
        (await DB.StringGetAsync(ToPreAggregateKey(source,timestamp))).ToDomain(source);

    public async Task<PlaneFrame> GetPlaneSourceState(PlaneSourceDefintion source) => 
        (await DB.StringGetAsync(ToStateKey(source))).ToDomain(source);
    public async Task SetPlaneSourceState(PlaneSourceDefintion source, PlaneFrame frame) =>
        await DB.StringSetAsync(ToStateKey(source), frame.ToModel().ToPayload(),TimeSpan.FromSeconds(60));

    public async Task UpdatePlaneRecordAsync(Plane plane)
    {
        var model = ((TimeAnotatedPlane)plane).ToModel();
        await DB.StringSetAsync(ToPlaneRecordKey(model.hexValue),model.ToPayload(), FinalPlaneRecordLifespan);
    }

    public async IAsyncEnumerable<TimeAnotatedPlane> GetPlaneRecordAsync()
    {
        var filter = ToPlaneRecordKey("*");
        await foreach(var key in Server.KeysAsync(pattern:filter))
        {
            var keysections = key.ToString().Split("_");
            yield return (await DB.StringGetAsync(ToPlaneRecordKey(keysections[2]))).ToModel().ToDomain();
        }
    }
    public string ToKey( PlaneFrame frame) => $"plane_{frame.Now}";
    public string ToCongregatedKey( PlaneFrame frame) => $"plane_congregated_{frame.Now}";

    public string ToStateKey(PlaneSourceDefintion source) => $"plane_{source.Node}_{source.Antenna}_state";

    public string ToPreAggregateKey(PlaneSourceDefintion source, long timestamp) => $"plane_preaggregate_{source.Node}_{source.Antenna}_{timestamp}";

    public string ToPlaneRecordKey(string hex) => $"plane_memo_{hex}";
}