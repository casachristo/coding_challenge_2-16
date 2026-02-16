using System.Text.Json;

namespace patching_api;

public class SystemDataManager
{
    public Dictionary<string, Region> systems_by_region { get; private set; }
    private bool rebuild_regional_data;
    public Dictionary<string, Dictionary<string, Object>> full_data { get; private set;}
    public SystemDataManager()
    {
        var systems = loadData();
        buildRegions(systems);
        this.rebuild_regional_data = true;
    }

    private List<System> loadData()
    {
        string json = File.ReadAllText("systems.json");
        List<System>? systems_list = JsonSerializer.Deserialize<List<System>>(json);
        return systems_list;
    }
    
    private void buildRegions(List<System> systems)
    {
        this.systems_by_region = new Dictionary<string, Region>();

        foreach (System system in systems)
        {
            if (!this.systems_by_region.ContainsKey(system.region))
            {
                this.systems_by_region.Add(system.region, new Region(system.region));
            }
            
            this.systems_by_region[system.region].addSystem(system);
        }
    }

    public void buildFullData()
    {
        if (!this.rebuild_regional_data) return;
        this.full_data = new Dictionary<string, Dictionary<string, Object>>();
        
        var avg_uptime = buildAvgUptime();
        this.full_data.Add("avg_uptime", avg_uptime);

        var regional_status = setRegionalStatus(avg_uptime);
        this.full_data.Add("regional_status", regional_status);
        
        this.rebuild_regional_data = false;
    }

    private Dictionary<string, Object> buildAvgUptime()
    {
        var avg_uptime_dict = new Dictionary<string, Object>();
        foreach (Region region in systems_by_region.Values)
        {
            var avg_uptime = region.calculateAverageUptime();
            avg_uptime_dict.Add(region.region_name, avg_uptime);
        }
        
        return avg_uptime_dict;
    }

    private Dictionary<string, Object> setRegionalStatus(Dictionary<string, Object> avg_uptime)
    {
        var status_dict = new Dictionary<string, Object>();

        foreach (KeyValuePair<string, Object> region in avg_uptime)
        {
            var uptime = (double)region.Value;
            var status = "degraded";

            if (uptime > 95)
            {
                status = "healthy";
            }
            else if (uptime < 90)
            {
                status = "critical";
            }

            status_dict.Add(region.Key, status);
        }
        
        return status_dict;
    }
}