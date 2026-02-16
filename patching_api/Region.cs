namespace patching_api;

public class Region
{
    public string region_name { get; private set; }
    private List<System> systems;
    
    public Region(string region_name)
    {
        this.region_name = region_name;
        this.systems = new List<System>();
    }
    
    public void addSystem(System system)
    {
        systems.Add(system);
    }
    
    public double calculateAverageUptime()
    {
        var total_uptime = 0.0;
        foreach (System system in systems)
        {
            total_uptime += system.uptime;
        }
        
        var average_uptime = total_uptime / systems.Count;
        
        return average_uptime;
    }
}